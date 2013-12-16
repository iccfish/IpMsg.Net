using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using FSLib.IPMessager.Entity;


namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 消息实体-网络消息的解析和翻译类
	/// </summary>
	public class MessageTranslator
	{

		IPEndPoint broadcastEndPoint;

		internal MessageTranslator(UDPThread udpClient, Config config, OnlineHost hostManager)
		{
			this.Client = udpClient;
			this.Config = config;
			this.LivedHost = hostManager;
			this.broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, Config.Port);

			receivedQueue = new Queue<ulong>();
			udpClient.PackageReceived += udpClient_PackageReceived;
		}

		#region 属性

		/// <summary>
		/// 用来发送和接收消息的对象
		/// </summary>
		public UDPThread Client { get; set; }

		/// <summary>
		/// 设置对象
		/// </summary>
		public Config Config { get; set; }

		/// <summary>
		/// 网络在线主机状态
		/// </summary>
		public OnlineHost LivedHost { get; set; }

		#endregion

		#region 消息处理部分

		/// <summary>
		/// 尝试将收到的消息解析为实体对象
		/// </summary>
		/// <param name="buffer">封包消息</param>
		/// <param name="remoteEndPoint">远程主机的端点位置</param>
		/// <returns></returns>
		/// <remarks>如果是分包的消息且当前并未接收完全，则同样返回为空</remarks>
		public Message ResolveToMessage(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null || buffer.Length < 0) return null;

			Message m = null;

			if (MessagePacker.Test(buffer))
			{
				m = MessagePacker.TryToTranslateMessage(buffer, remoteEndPoint);

				if (m != null)
				{
					m.Host = LivedHost.GetHost(remoteEndPoint.Address.ToString());

					//确认是否要发送回复
					if (DetermineConfirm(m))
					{
						Message cm = Message.Create(m.Host, remoteEndPoint, Config.GetRandomTick(), Config.GroupName, Config.NickName,
							  FSLib.IPMessager.Define.Consts.Commands.RecvMsg, 0, m.PackageNo.ToString(), "");
						Send(cm);
					}
				}
			}
			else if (MessagePackerV2.Test(buffer))
			{
				PackedNetworkMessage pack = MessagePackerV2.Parse(buffer, remoteEndPoint);

				if (pack == null) return null;

				if (DetermineConfirm(pack))
				{
					//发送确认标志
					Message cm = Message.Create(m.Host, remoteEndPoint, Config.GetRandomTick(), Config.GroupName, Config.NickName,
						 FSLib.IPMessager.Define.Consts.Commands.Ex_PackageRecevied, 0, pack.PackageNo.ToString(), pack.PackageIndex.ToString());
					Send(cm);
				}
				m = MessagePackerV2.TryToTranslateMessage(pack);

				if (m != null)
				{
					m.Host = LivedHost.GetHost(remoteEndPoint.Address.ToString());

					//确认是否要发送回复
					if (DetermineConfirm(m))
					{
						Message cm = Message.Create(m.Host, remoteEndPoint, Config.GetRandomTick(), Config.GroupName, Config.NickName,
							 FSLib.IPMessager.Define.Consts.Commands.RecvMsg, 0, m.PackageNo.ToString(), "");
						Send(cm);
					}
					if (DetermineConfirm2(m))
					{
						Message cm = Message.Create(m.Host, remoteEndPoint, Config.GetRandomTick(), Config.GroupName, Config.NickName,
							 FSLib.IPMessager.Define.Consts.Commands.AnsReadMsg, 0, m.PackageNo.ToString(), "");
						Send(cm);
					}
				}
			}

			return m;
		}

		/// <summary>
		/// 检测是否需要发送回复包来确认收到
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		static bool DetermineConfirm(Message message)
		{
			return !message.IsBroadCast && message.IsRequireReceiveCheck && !message.IsAutoSendMessage;
		}

		/// <summary>
		/// 检测是否需要发送回复包来确认收到
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		static bool DetermineConfirm2(Message message)
		{
			return !message.IsBroadCast && message.IsRequireReadCheck && !message.IsAutoSendMessage;
		}

		/// <summary>
		/// 检测是否需要发送回复包来确认收到
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		static bool DetermineConfirm(Entity.PackedNetworkMessage message)
		{
			return message.IsReceiveSignalRequired;
		}


		#endregion

		#region 公共事件

		#region 文本消息发送

		/// <summary>
		/// 发送消息并且要求对方返回已接收消息
		/// </summary>
		/// <param name="host">远程主机</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <exception cref="System.ArgumentNullException">当要求对方确认收到时,必须设置主机 host</exception>
		/// <returns>返回发出的消息包编号</returns>
		public ulong SendWithCheck(Host host, Define.Consts.Commands cmd, ulong options, string normalMsg, string extendMessage)
		{
			if (host == null) throw new ArgumentNullException("host");

			return Send(host, host.HostSub.Ipv4Address, cmd, options, normalMsg, extendMessage, true);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="host">远程主机</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <returns>返回发出的消息包编号</returns>
		public ulong SendWithNoCheck(Host host, Define.Consts.Commands cmd, ulong options, string normalMsg, string extendMessage)
		{
			return Send(host, host == null ? broadcastEndPoint : host.HostSub.Ipv4Address, cmd, options, normalMsg, extendMessage, false);
		}

		/// <summary>
		/// 直接向目标IP发送信息
		/// </summary>
		/// <param name="host">远程主机</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <returns>返回发出的消息包编号</returns>
		public ulong SendByIp(IPEndPoint remoteEndPoint, Define.Consts.Commands cmd, ulong options, string normalMsg, string extendMessage)
		{
			return Send(null, remoteEndPoint ?? broadcastEndPoint, cmd, options, normalMsg, extendMessage, false);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="remoteEndPoint">远程主机地址</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <returns>返回发出的消息包编号</returns>
		public ulong Send(Host host, IPEndPoint remoteEndPoint, Define.Consts.Commands cmd, ulong options, string normalMsg, string extendMessage, bool sendCheck)
		{
			Message cm = Message.Create(host, remoteEndPoint, Config.GetRandomTick(), Config.HostName, Config.HostUserName,
				 cmd, options, normalMsg, extendMessage);
			if (sendCheck) cm.IsRequireReceiveCheck = sendCheck;

			return Send(cm);
		}

		/// <summary>
		/// 发送打包好的消息
		/// </summary>
		/// <param name="cm"></param>
		/// <returns>返回发出的消息包编号</returns>
		public ulong Send(Message cm)
		{
			if (!Client.IsInitialized) return 0ul;

			cm.Options |= (ulong)Define.Consts.Cmd_All_Option.EnableNewDataContract;

			MessageEventArgs mea = new MessageEventArgs(cm) { Message = cm, IsHandled = false, Host = cm.Host };
			OnMessageSending(mea);
			if (mea.IsHandled) return mea.Message.PackageNo;

			//判断远程主机是否支持这个模式
			if (!Config.ForceOldContract && cm.Host != null && cm.Host.IsEnhancedContractEnabled)
			{
				Entity.PackedNetworkMessage[] pnm = MessagePackerV2.BuildNetworkMessage(cm);
				PackageEventArgs pea = new PackageEventArgs(pnm.Length > 1, pnm[0], pnm);
				OnPckageSending(pea);
				if (!pea.IsHandled)
				{
					Array.ForEach(pnm, s => { Client.Send(s); });
					OnPackageSended(pea);
				}
			}
			else
			{
				Entity.PackedNetworkMessage pn = MessagePacker.BuildNetworkMessage(cm);
				PackageEventArgs pe = new PackageEventArgs(false, pn, null);
				OnPckageSending(pe);
				if (!pe.IsHandled)
				{
					Client.Send(pn);
					OnPackageSended(pe);
				}
			}
			OnMessageSended(mea);

			return cm.PackageNo;
		}

		/// <summary>
		/// 通过广播发送消息
		/// </summary>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <returns>返回发出的消息包编号</returns>
		public void Send(Define.Consts.Commands cmd, ulong options, string normalMsg, string extendMessage)
		{
			if (!Client.IsInitialized) return;

			SendWithNoCheck(null, cmd, options, normalMsg, extendMessage);
		}

		#endregion

		#region 二进制模式发送消息

		/// <summary>
		/// 以二进制模式发送消息并且要求对方返回已接收消息
		/// </summary>
		/// <param name="host">远程主机</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <returns>返回发出的消息包编号</returns>
		public ulong SendWithNoCheck(Host host, Define.Consts.Commands cmd, ulong options, byte[] normalMsg, byte[] extendMessage)
		{
			if (host == null) throw new ArgumentNullException("host");

			return Send(host, host.HostSub.Ipv4Address, cmd, options, normalMsg, extendMessage, false);
		}

		/// <summary>
		/// 发送消息并且要求对方返回已接收消息
		/// </summary>
		/// <param name="host">远程主机.如果为null则会抛出异常</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <returns>返回发出的消息包编号</returns>
		public ulong SendWithCheck(Host host, Define.Consts.Commands cmd, ulong options, byte[] normalMsg, byte[] extendMessage)
		{
			if (host == null) throw new ArgumentNullException("host");

			return Send(host, host.HostSub.Ipv4Address, cmd, options, normalMsg, extendMessage, true);
		}

		/// <summary>
		/// 以二进制模式发送消息
		/// </summary>
		/// <param name="host">关联的远程主机,不可以为null</param>
		/// <param name="remoteEndPoint">远程主机地址</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">参数</param>
		/// <param name="normalMsg">常规信息</param>
		/// <param name="extendMessage">扩展消息</param>
		/// <param name="sendCheck">是否检查发送到</param>
		/// <exception cref="InvalidOperationException">如果对方主机不在列表中,或未知是否支持增强协议,则会抛出此异常</exception>
		/// <returns>返回发出的消息包编号</returns>
		public ulong Send(Host host, IPEndPoint remoteEndPoint, Define.Consts.Commands cmd, ulong options, byte[] normalMsg, byte[] extendMessage, bool sendCheck)
		{
			if (!Client.IsInitialized) return 0ul;

			//判断远程主机是否支持这个模式
			if (host == null || !host.IsEnhancedContractEnabled) throw new InvalidOperationException("尚不知道主机是否支持增强协议模式，无法以二进制模式发送消息！");

			Message cm = Message.Create(host, remoteEndPoint, Config.GetRandomTick(), Config.HostName, Config.NickName,
				 cmd, options, "", "");
			cm.ExtendMessageBytes = extendMessage;
			cm.NormalMsgBytes = normalMsg;
			cm.IsRequireReceiveCheck = sendCheck;

			//设置选项
			if (sendCheck)
			{
				cm.Options |= (ulong)Define.Consts.Cmd_All_Option.RequireReceiveCheck;
			}
			cm.Options |= (ulong)Define.Consts.Cmd_All_Option.EnableNewDataContract | (ulong)Define.Consts.Cmd_All_Option.BinaryMessage;

			MessageEventArgs mea = new MessageEventArgs(cm) { Message = cm, IsHandled = false, Host = host };
			OnMessageSending(mea);
			if (mea.IsHandled) return mea.Message.PackageNo;

			Entity.PackedNetworkMessage[] pnm = MessagePackerV2.BuildNetworkMessage(cm);
			PackageEventArgs pea = new PackageEventArgs(pnm.Length > 1, pnm[0], pnm);
			OnPckageSending(pea);
			if (!pea.IsHandled)
			{
				Array.ForEach(pnm, s => { Client.Send(s); });
				OnPackageSended(pea);
			}
			OnMessageSended(mea);

			return cm.PackageNo;
		}

		#endregion

		#endregion

		#region 捕捉网络层的收到的消息

		//用来检测重复收到的消息包
		Queue<ulong> receivedQueue;

		/// <summary>
		/// 接收到UDP的消息包
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void udpClient_PackageReceived(object sender, PackageReceivedEventArgs e)
		{
			if (!e.IsHandled)
			{
				e.IsHandled = true;
				Message m = ResolveToMessage(e.Data, e.RemoteIP);
				if (m == null) return;

				//检查最近收到的消息队列里面是否已经包含了这个消息包，如果是，则丢弃
				if (!receivedQueue.Contains(m.PackageNo))
				{
					receivedQueue.Enqueue(m.PackageNo);
					if (receivedQueue.Count > 100) receivedQueue.Dequeue();

					OnMessageReceived(new MessageEventArgs(m));
				}
			}
		}

		#endregion

		#region 事件

		/// <summary>
		/// 接收到消息包（UDP）
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageReceived;
		SendOrPostCallback messageReceivedCallBack;

		/// <summary>
		/// 引发接收到消息包事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMessageReceived(MessageEventArgs e)
		{
			if (MessageReceived == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				MessageReceived(this, e);
			}
			else
			{
				if (messageReceivedCallBack == null) messageReceivedCallBack = s => MessageReceived(this, s as MessageEventArgs);

				IPMClient.SendSynchronizeMessage(messageReceivedCallBack, e);
			}
		}

		/// <summary>
		/// 消息将要发送事件
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageSending;
		SendOrPostCallback messageSendingCallBack;


		/// <summary>
		/// 引发消息将要发送事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMessageSending(MessageEventArgs e)
		{
			if (MessageSending == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				MessageSending(this, e);
			}
			else
			{
				if (messageSendingCallBack == null) messageSendingCallBack = s => MessageSending(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(messageSendingCallBack, e);
			}
		}

		/// <summary>
		/// 消息已经发送事件
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageSended;
		SendOrPostCallback messageSendedCall;
		/// <summary>
		/// 引发消息已经发送事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnMessageSended(MessageEventArgs e)
		{
			if (MessageSended == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				MessageSended(this, e);
			}
			else
			{
				if (messageSendedCall == null) messageSendedCall = s => MessageSended(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(messageSendedCall, e);
			}
		}

		/// <summary>
		/// 数据包将要发送
		/// </summary>
		public event EventHandler<PackageEventArgs> PackageSending;
		SendOrPostCallback callPackageSending;

		/// <summary>
		/// 触发数据包将要发送事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnPckageSending(PackageEventArgs e)
		{
			if (PackageSending == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				PackageSending(this, e);
			}
			else
			{
				if (callPackageSending == null) callPackageSending = s => PackageSending(this, s as PackageEventArgs);
				IPMClient.SendASynchronizeMessage(callPackageSending, e);
			}
		}

		/// <summary>
		/// 数据包已经发送
		/// </summary>
		public event EventHandler<PackageEventArgs> PackageSended;
		SendOrPostCallback callPackageSended;
		/// <summary>
		/// 触发数据包已发送事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnPackageSended(PackageEventArgs e)
		{
			if (PackageSended == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				PackageSended(this, e);
			}
			else
			{
				if (callPackageSended == null) callPackageSended = s => PackageSended(this, s as PackageEventArgs);
				IPMClient.SendASynchronizeMessage(callPackageSended, e);
			}
		}



		#endregion
	}

}
