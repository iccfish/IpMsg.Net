using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using FSLib.IPMessager.Entity;


namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// UDP信息类，用于处理文本消息和命令通讯
	/// </summary>
	public class UDPThread : IDisposable, IUdpWorker
	{

		#region 私有变量

		/// <summary>
		/// UDP客户端
		/// </summary>
		UdpClient client;

		/// <summary>
		/// 用于轮询是否发送成功的记录
		/// </summary>
		List<PackedNetworkMessage> SendList;

		IPMClient _ipmClient;

		#endregion

		#region 构造函数

		/// <summary>
		/// 构造一个新的消息对象，并绑定到指定的端口和IP上。
		/// </summary>
		/// <param name="ip">绑定的IP</param>
		/// <param name="port">绑定的端口</param>
		internal UDPThread(IPMClient ipmClient, IPAddress ip, int port)
		{
			IsInitialized = false;
			try
			{
				client = new UdpClient(new IPEndPoint(ip, port));
			}
			catch (Exception)
			{
				OnNetworkError(new EventArgs());
				return;
			}
			SendList = new List<PackedNetworkMessage>();
			client.EnableBroadcast = true;
			_ipmClient = ipmClient;

			CheckQueueTimeInterval = 2000;
			MaxResendTimes = 5;
			new System.Threading.Thread(new System.Threading.ThreadStart(CheckUnConfirmedQueue)) { IsBackground = true }.Start();


			IsInitialized = true;

			//开始监听
			client.BeginReceive(EndReceiveDataAsync, null);
		}

		#endregion


		#region 私有方法


		void EndReceiveDataAsync(IAsyncResult ar)
		{
			IPEndPoint ipend = null;
			byte[] buffer = null;
			try
			{
				buffer = client.EndReceive(ar, ref ipend);
			}
			catch (Exception)
			{
				return;
			}
			finally
			{
				if (IsInitialized && client != null) client.BeginReceive(EndReceiveDataAsync, null);
			}

			if (buffer == null || buffer.Length == 0) return;

			//验证IP
			IpValidateRequiredEventArgs ev = new IpValidateRequiredEventArgs()
			{
				IPEndPoint = ipend,
				Data = buffer
			};
			OnIpValidateRequired(ev);
			//消息被过滤时，直接返回
			if (ev.IsPackageDroped)
			{
				NetworkPackageEventArgs pea = new NetworkPackageEventArgs() { IPEndPoint = ipend };
				OnPackageDroped(pea);
			}
			else
			{
				OnPackageReceived(new PackageReceivedEventArgs() { RemoteIP = ipend, Data = buffer });
			}

		}

		#endregion


		#region 公共函数

		/// <summary>
		/// 关闭客户端
		/// </summary>
		public void Close()
		{
			if (IsInitialized)
			{
				IsInitialized = false;
				if (IsInitialized) client.Close();
				client = null;
			}
		}


		/// <summary>
		/// 发送数据。这个请求发送的是广播请求
		/// </summary>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		public void Send(int port, byte[] data, ulong packageNo)
		{
			Send(IPAddress.Broadcast, port, data, packageNo, 0);
		}

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		public void Send(IPAddress address, int port, byte[] data, ulong packageNo)
		{
			Send(false, address, port, data, packageNo, 0);
		}

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="receiveConfirm">消息是否会回发确认包</param>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		public void Send(bool receiveConfirm, IPAddress address, int port, byte[] data, ulong packageNo)
		{
			Send(receiveConfirm, address, port, data, packageNo, 0);
		}

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">分包索引</param>
		public void Send(IPAddress address, int port, byte[] data, ulong packageNo, int packageIndex)
		{
			Send(false, new IPEndPoint(address, port), data, packageNo, packageIndex);
		}

		/// <summary>
		/// 发送数据，并对数据作回应检查。当数据包编号大于0时，将会在每隔 <see cref="CheckQueueTimeInterval"/> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"/> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="receiveConfirm">消息是否会回发确认包</param>
		/// <param name="address">远程主机地址</param>
		/// <param name="port">远程主机端口</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">分包索引</param>
		public void Send(bool receiveConfirm, IPAddress address, int port, byte[] data, ulong packageNo, int packageIndex)
		{
			Send(receiveConfirm, new IPEndPoint(address, port), data, packageNo, packageIndex);
		}

		/// <summary>
		/// 发送数据，并对数据作回应检查。当 <see cref="receiveConfirm"/> 为 true 时，将会在每隔 <see cref="CheckQueueTimeInterval"></see> 的间隔后重新发送，直到收到对方的回应。
		/// 注意：网络层不会解析回应，请调用 <see cref="PopSendItemFromList"></see> 方法来告知已收到数据包。
		/// </summary>
		/// <param name="receiveConfirm">消息是否会回发确认包</param>
		/// <param name="address">远程主机地址</param>
		/// <param name="data">数据流</param>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">分包索引</param>
		public void Send(bool receiveConfirm, IPEndPoint address, byte[] data, ulong packageNo, int packageIndex)
		{
			if (IsInitialized)
			{
				client.Send(data, data.Length, address);
				if (receiveConfirm)
					PushSendItemToList(new PackedNetworkMessage() { Data = data, RemoteIP = address, SendTimes = 0, PackageIndex = packageIndex, PackageNo = packageNo });
			}
		}

		/// <summary>
		/// 将已经打包的消息发送出去
		/// </summary>
		/// <param name="packedMessage">已经打包的消息</param>
		/// <param name="confirmReceived">是否回发确认消息</param>
		public void Send(Entity.PackedNetworkMessage packedMessage)
		{
			if (IsInitialized)
			{
				client.Send(packedMessage.Data, packedMessage.Data.Length, packedMessage.RemoteIP);
				if (packedMessage.IsReceiveSignalRequired)
					PushSendItemToList(packedMessage);
			}
		}

		#endregion

		#region 线程操作-重复确认收到消息

		static object lockObj = new object();

		System.Threading.SendOrPostCallback cucqCallpack;
		System.Threading.SendOrPostCallback resendCallback;

		/// <summary>
		/// 自由线程，检测未发送的数据并发出
		/// </summary>
		void CheckUnConfirmedQueue()
		{
			//异步调用委托
			if (cucqCallpack == null) cucqCallpack = (s) => OnPackageSendFailure(s as PackageSendEventArgs);
			if (resendCallback == null) resendCallback = (s) => OnPackageResend(s as PackageSendEventArgs);

			do
			{
				if (SendList.Count > 0)
				{
					PackedNetworkMessage[] array = null;

					lock (SendList)
					{
						array = SendList.ToArray();
					}
					//挨个重新发送并计数
					Array.ForEach(array, s =>
					{
						s.SendTimes++;
						if (s.SendTimes >= MaxResendTimes)
						{
							//发送失败啊
							PackageSendEventArgs e = new PackageSendEventArgs() { Package = s };
							if (IPMClient.NeedPostMessage)
							{
								IPMClient.SendSynchronizeMessage(cucqCallpack, e);
							}
							else
							{
								OnPackageSendFailure(e);
							}
							SendList.Remove(s);
						}
						else
						{
							//重新发送
							client.Send(s.Data, s.Data.Length, s.RemoteIP);

							PackageSendEventArgs e = new PackageSendEventArgs() { Package = s };
							if (IPMClient.NeedPostMessage)
							{
								IPMClient.SendASynchronizeMessage(resendCallback, e);
							}
							else
							{
								OnPackageResend(e);
							}
						}
					});
				}

				System.Threading.Thread.Sleep(CheckQueueTimeInterval);
			} while (IsInitialized);
		}

		/// <summary>
		/// 将数据信息压入列表
		/// </summary>
		/// <param name="item"></param>
		void PushSendItemToList(PackedNetworkMessage item)
		{
			SendList.Add(item);
		}

		/// <summary>
		/// 将数据包从列表中移除
		/// </summary>
		/// <param name="packageNo">数据包编号</param>
		/// <param name="packageIndex">数据包分包索引</param>
		public void PopSendItemFromList(ulong packageNo, int packageIndex)
		{
			lock (lockObj)
			{
				Array.ForEach(SendList.Where(s => s.PackageNo == packageNo && s.PackageIndex == packageIndex).ToArray(), s => SendList.Remove(s));
			}
		}

		#endregion

		#region 属性

		/// <summary>
		/// 是否已经初始化了
		/// </summary>
		public bool IsInitialized { get; private set; }

		/// <summary>
		/// 检查发送队列间隔
		/// </summary>
		public int CheckQueueTimeInterval { get; set; }

		/// <summary>
		/// 没有收到确认包时，最大重新发送的数目，超过此数目会丢弃并触发 <see cref="PackageSendFailture"/> 事件。
		/// </summary>
		public int MaxResendTimes { get; set; }

		#endregion

		#region 事件

		/// <summary>
		/// 网络出现异常（如端口无法绑定等，此时无法继续工作）
		/// </summary>
		public event EventHandler NetworkError;

		protected void OnNetworkError(EventArgs e)
		{
			if (NetworkError != null) NetworkError(this, e);

			IpmEvents.OnUdpNetworkError(_ipmClient, e);
		}

		/// <summary>
		/// 主机消息包被丢弃时触发
		/// </summary>
		public event EventHandler<NetworkPackageEventArgs> PackageDroped;

		/// <summary>
		/// 主机消息包被丢弃时，被调用
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnPackageDroped(NetworkPackageEventArgs e)
		{
			if (PackageDroped != null) PackageDroped(this, e);

			IpmEvents.OnUdpPackageDroped(_ipmClient, e);
		}

		/// <summary>
		/// 当收到远程主机的消息，需要对其进行验证时触发
		/// </summary>
		public event IpValidateRequiredEventHandler IpValidateRequired;

		/// <summary>
		/// 当事件触发时调用
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnIpValidateRequired(IpValidateRequiredEventArgs e)
		{
			if (IpValidateRequired != null) IpValidateRequired(this, e);

			IpmEvents.OnUdpIpValidateRequired(_ipmClient, e);
		}


		/// <summary>
		/// 当数据包收到时触发
		/// </summary>
		public event EventHandler<PackageReceivedEventArgs> PackageReceived;

		/// <summary>
		/// 当数据包收到事件触发时，被调用
		/// </summary>
		/// <param name="e">包含事件的参数</param>
		protected virtual void OnPackageReceived(PackageReceivedEventArgs e)
		{
			if (PackageReceived != null) PackageReceived(this, e);

			IpmEvents.OnUdpPackageReceived(_ipmClient, e);
		}
		/// <summary>
		/// 数据包发送失败
		/// </summary>
		public event EventHandler<PackageSendEventArgs> PackageSendFailure;

		/// <summary>
		/// 当数据发送失败时调用
		/// </summary>
		/// <param name="e">包含事件的参数</param>
		protected virtual void OnPackageSendFailure(PackageSendEventArgs e)
		{
			if (PackageSendFailure != null) PackageSendFailure(this, e);
			IpmEvents.OnUdpPackageSendFailure(_ipmClient, e);
		}

		/// <summary>
		/// 数据包未接收到确认，重新发送
		/// </summary>
		public event EventHandler<PackageSendEventArgs> PackageResend;


		/// <summary>
		/// 触发重新发送事件
		/// </summary>
		/// <param name="e">包含事件的参数</param>
		protected virtual void OnPackageResend(PackageSendEventArgs e)
		{
			if (PackageResend != null) PackageResend(this, e);
			IpmEvents.OnUdpPackageResend(_ipmClient, e);
		}


		#endregion

		#region IDisposable 成员

		/// <summary>
		/// 关闭客户端并释放资源
		/// </summary>
		public void Dispose()
		{
			Close();
		}

		#endregion
	}
}
