using System;
using System.Linq;
using System.Net;
using System.Threading;
using FSLib.IPMessager.Define;
using FSLib.IPMessager.Entity;
using FSLib.IPMessager.Network;
using FSLib.IPMessager.Properties;


namespace FSLib.IPMessager.Core
{
	/// <summary>
	/// 负责命令的解析等操作
	/// </summary>
	public class CommandExecutor : IDisposable
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		internal CommandExecutor(Config cfg)
		{
			this.Config = cfg;
		}


		/// <summary>
		/// 初始化所有对象，并初始化类
		/// </summary>
		public void Init()
		{
			Client = new UDPThread(this.Config.BindedIP, this.Config.Port);			//文本网络对象
			LivedHost = new OnlineHost(this.Config);								//主机列表
			MessageProxy = new MessageTranslator(Client, this.Config, LivedHost);	//文本信息翻译层对象

			//自动查询主机客户端版本
			LivedHost.HostOnline += (s, e) =>
			{
				if (Config.AutoDetectVersion) this.GetVersion(e.Host);
			};

			//文件传输
			FileTaskManager = new FileTaskManager(this.Config);
			FileTaskManager.FileReceiveTaskDiscarded += (s, e) => this.SendReleaseFilesSignal(e.TaskInfo.RemoteHost, e.TaskInfo.PackageID);
			FileTaskModule = new FSLib.IPMessager.Network.TCPThread(Config)
			{
				TaskManager = FileTaskManager
			};


			//与接收消息进行挂钩
			MessageProxy.MessageReceived += MessageProxy_MessageReceived;
		}

		/// <summary>
		/// 获得底层是否成功初始化的状态
		/// </summary>
		public bool IsInitialized
		{
			get
			{
				return Client.IsInitialized;
			}
		}

		#region 消息处理

		//处理接收到消息事件
		void MessageProxy_MessageReceived(object sender, MessageEventArgs e)
		{
			if (e.Message == null || e.IsHandled) return;

			MessageEventArgs me = new MessageEventArgs(e.Message, e.Host);
			OnMessageProcessing(me);
			if (me.IsHandled) return;

			//分析请求
			switch (e.Message.Command)
			{
				case Consts.Commands.NoOperaiton: ProcessCommand_Nop(e.Message, e.Host); break;
				case Consts.Commands.Br_Entry: ProcessCommand_Br_Entry(e.Message, e.Host); break;
				case Consts.Commands.Br_Exit: ProcessCommand_Br_Exit(e.Message, e.Host); break;
				case Consts.Commands.AnsEntry: ProcessCommand_AnsEntry(e.Message, e.Host); break;
				case Consts.Commands.Br_Absence: ProcessCommand_BrAbsence(e.Message, e.Host); break;
				case Consts.Commands.IsGetList: ProcessCommand_IsGetList(e.Message, e.Host); break;
				case Consts.Commands.OkGetList: ProcessCommand_OkGetList(e.Message, e.Host); break;
				case Consts.Commands.GetList: ProcessCommand_GetList(e.Message, e.Host); break;
				case Consts.Commands.AnsList: ProcessCommand_AnsList(e.Message, e.Host); break;
				case Consts.Commands.IsGetList2: ProcessCommand_IsGetList2(e.Message, e.Host); break;
				case Consts.Commands.SendMsg: ProcessCommand_SendMsg(e.Message, e.Host); break;
				case Consts.Commands.RecvMsg: ProcessCommand_RecvMsg(e.Message, e.Host); break;
				case Consts.Commands.ReadMsg: ProcessCommand_ReadMsg(e.Message, e.Host); break;
				case Consts.Commands.DelMsg: ProcessCommand_DelMsg(e.Message, e.Host); break;
				case Consts.Commands.AnsReadMsg: ProcessCommand_AnsReadMsg(e.Message, e.Host); break;
				case Consts.Commands.GetInfo: ProcessCommand_GetInfo(e.Message, e.Host); break;
				case Consts.Commands.SendInfo: ProcessCommand_SendInfo(e.Message, e.Host); break;
				case Consts.Commands.GetAbsenceInfo: ProcessCommand_GetAbsenceInfo(e.Message, e.Host); break;
				case Consts.Commands.SendAbsenceInfo: ProcessCommand_SendAbsenceInfo(e.Message, e.Host); break;
				case Consts.Commands.GetFileData: ProcessCommand_GetFileData(e.Message, e.Host); break;
				case Consts.Commands.ReleaseFiles: ProcessCommand_ReleaseFiles(e.Message, e.Host); break;
				case Consts.Commands.GetDirFiles: ProcessCommand_GetDirFiles(e.Message, e.Host); break;
				case Consts.Commands.Ex_PackageRecevied: ProcessCommand_Ex_PackageRecevied(e.Message, e.Host); break;
				default: break;
			}
			//
			OnMessageProcessed(me);
		}

		#endregion

		#region 对具体命令进行处理

		//处理空命令
		private void ProcessCommand_Nop(Message m, Host h)
		{
			if (h == null) return;
		}

		//Br_Entry
		void ProcessCommand_Br_Entry(Message m, Host h)
		{
			if (h == null && (Config.IgnoreNoAddListFlag || !Consts.Check(m.Options, Consts.Cmd_Send_Option.NoAddList)))
			{
				h = new Host()
				{
					ClientVersion = string.Empty,
					GroupName = m.ExtendMessage,
					HasShare = false,
					HostSub = new HostSub() { HostName = m.HostName, Ipv4Address = m.HostAddr, UserName = m.UserName },
					Index = 0,
					IsEnhancedContractEnabled = Consts.Check(m.Options, Consts.Cmd_All_Option.EnableNewDataContract),
					NickName = m.NormalMsg,
					HostFeature = m.Options
				};
				if (Consts.Check(m.Options, Consts.Cmd_All_Option.Absence)) h.ChangeAbsenceMode(true, Resources.CommandExecutor_ProcessCommand_Br_Entry_LeaveModeText);
				h.SupportEncrypt = Consts.Check(m.Options, Consts.Cmd_All_Option.Encrypt);
				h.SupportFileTransport = Consts.Check(m.Options, Consts.Cmd_All_Option.FileAttach);

				LivedHost.Add(m.HostAddr.Address.ToString(), h);
				h.AbsenceModeChanged += Host_AbsendModeChanged;

				//立刻查询离开消息
				if (h.IsInAbsenceMode) Host_AbsendModeChanged(h, null);
				m.Host = h;
			}

			ulong opt = 0ul;
			if (Config.IsInAbsenceMode) opt |= (ulong)Consts.Cmd_All_Option.Absence;
			if (Config.EnableFileTransfer) opt |= (ulong)Consts.Cmd_All_Option.FileAttach;
			//插件
			if (Config.Services != null) Config.Services.ProviderExecute(s => opt |= s.GenerateClientFeatures());

			//回复证明自己在线
			MessageProxy.SendWithNoCheck(h, Consts.Commands.AnsEntry, opt, Config.NickName, Config.GroupName);

			//如果开启了通知功能，则告知非本网段主机
			if (h != null && Config.EnableHostNotifyBroadcast && !Config.ForceOldContract)
			{
				Host[] list = LivedHost.Values.Where(s => s.HostSub.IPHeader != h.HostSub.IPHeader && h.IsEnhancedContractEnabled).Distinct(Helper.HostSubEqualityCompare.StaticObj).ToArray();
				Array.ForEach(list, s => MessageProxy.SendWithNoCheck(s, Consts.Commands.Br_Entry_Forward, 0, s.HostSub.Ipv4Address.Address.GetAddressBytes(), BitConverter.GetBytes(s.HostSub.PortNo)));
			}
		}

		//Br_Exit
		void ProcessCommand_Br_Exit(Message m, Host h)
		{
			//如果开启了通知功能，则告知非本网段主机
			if (h == null)
				return;

			LivedHost.Delete(h.HostSub.Ipv4Address.Address.ToString());
			//通知非本网段主机
			if (Config.EnableHostNotifyBroadcast && !Config.ForceOldContract)
			{
				Host[] list = LivedHost.Values.Where(s => s.HostSub.IPHeader != h.HostSub.IPHeader).Distinct(Helper.HostSubEqualityCompare.StaticObj).ToArray();
				Array.ForEach(list, s =>
				{
					MessageProxy.SendWithNoCheck(s, Consts.Commands.Br_Exit_Forward, 0, s.HostSub.Ipv4Address.Address.GetAddressBytes(), BitConverter.GetBytes(s.HostSub.PortNo));
				});
			}
		}

		//AnsEntry
		void ProcessCommand_AnsEntry(Message m, Host h)
		{
			if (h == null && (Config.IgnoreNoAddListFlag || !Consts.Check(m.Options, Consts.Cmd_Send_Option.NoAddList)))
			{
				h = new Host()
				{
					ClientVersion = string.Empty,
					GroupName = m.ExtendMessage,
					HasShare = false,
					HostSub = new HostSub() { HostName = m.HostName, Ipv4Address = m.HostAddr, UserName = m.UserName },
					Index = 0,
					IsEnhancedContractEnabled = Consts.Check(m.Options, Consts.Cmd_All_Option.EnableNewDataContract),
					NickName = m.NormalMsg
				};
				if (Consts.Check(m.Options, Consts.Cmd_All_Option.Absence)) h.ChangeAbsenceMode(true, Resources.CommandExecutor_ProcessCommand_Br_Entry_LeaveModeText);
				LivedHost.Add(m.HostAddr.Address.ToString(), h);
				h.AbsenceModeChanged += Host_AbsendModeChanged;

				//立刻查询离开消息
				if (h.IsInAbsenceMode) Host_AbsendModeChanged(h, null);
				m.Host = h;
			}

			h.SupportEncrypt = Consts.Check(m.Options, Consts.Cmd_All_Option.Encrypt);
			h.SupportFileTransport = Consts.Check(m.Options, Consts.Cmd_All_Option.FileAttach);
		}

		private void ProcessCommand_GetDirFiles(Message message, Host host)
		{
			if (host == null) return;

			//this command was executed by tcp file send,so we doesn't do this
			//but maybe we need to keep this section because it maybe useable
		}

		private void ProcessCommand_ReleaseFiles(Message message, Host host)
		{
			if (host == null) return;

			//释放文件
			ulong pkgid = 0;
			if (!ulong.TryParse(message.NormalMsg, out pkgid) || pkgid == 0) return;

			FileTaskManager.ReleaseFile(pkgid);
		}

		private void ProcessCommand_GetFileData(Message message, Host host)
		{
			if (host == null) return;

			//this command was executed by tcp file send,so we doesn't process this command here
			//but maybe we need to keep this section because it maybe useable
		}

		//process received absencemode
		private void ProcessCommand_SendAbsenceInfo(Message message, Host host)
		{
			if (host == null) return;

			host.ChangeAbsenceMode(host.IsInAbsenceMode, message.NormalMsg);
		}

		//发送离开信息？
		private void ProcessCommand_GetAbsenceInfo(Message message, Host host)
		{
			if (host == null) return;

			MessageProxy.SendWithNoCheck(host, Consts.Commands.SendAbsenceInfo, 0ul, Config.IsInAbsenceMode ? Config.AbsenceMessage : Resources.CommandExecutor_ProcessCommand_GetAbsenceInfo_NotAbsenceMode, "");
		}

		//查询版本回应
		private void ProcessCommand_SendInfo(Message message, Host host)
		{
			if (host == null) return;

			host.ClientVersion = message.NormalMsg;
		}

		//请求查询版本信息
		private void ProcessCommand_GetInfo(Message message, Host host)
		{
			if (host == null) return;

			string version = Config.VersionInfo;
			if (string.IsNullOrEmpty(version))
			{
				version = System.Reflection.Assembly.GetExecutingAssembly().FullName;
			}

			MessageProxy.SendWithNoCheck(host, Consts.Commands.SendInfo, 0, version, "");
		}


		private void ProcessCommand_AnsReadMsg(Message message, Host host)
		{
			if (host != null)
				OnTextMessageOpened(new MessageEventArgs(message, host));
		}

		private void ProcessCommand_DelMsg(Message message, Host host)
		{
			if (host == null) return;

		}

		private void ProcessCommand_ReadMsg(Message message, Host host)
		{
			if (host != null)
				OnTextMessageOpened(new MessageEventArgs(message, host));
		}

		//数据包收到确认
		private void ProcessCommand_Ex_PackageRecevied(Message message, Host host)
		{
			if (host == null) return;

			ulong pkno = message.NormalMsg.TryParseToInt(0ul);
			int pkindex = message.ExtendMessage.TryParseToInt(0);

			if (pkno == 0)
				return;

			Client.PopSendItemFromList(pkno, pkindex);
		}

		//收到消息包了？
		private void ProcessCommand_RecvMsg(Message message, Host host)
		{
			if (host == null) return;

			ulong pkno = message.NormalMsg.TryParseToInt(0ul);

			if (pkno == 0) return;

			Client.PopSendItemFromList(pkno, 0);

			OnTextMessageArrived(new MessageEventArgs(message, host));
		}

		//接收到文字消息
		private void ProcessCommand_SendMsg(Message message, Host host)
		{
			if (host == null) return;

			//确认是否需要自动回复？
			if (!message.IsAutoSendMessage && (Config.AutoReply || (Config.AutoReplyWhenAbsence && Config.IsInAbsenceMode)))
			{
				string msg = string.IsNullOrEmpty(Config.AutoReplyMessage) ? Config.AutoReplyMessage : Config.AbsenceMessage;
				if (string.IsNullOrEmpty(msg)) msg = Resources.CommandExecutor_ProcessCommand_SendMsg_AutoReplyMessage;

				MessageProxy.SendWithNoCheck(host, Consts.Commands.SendMsg, (ulong)Consts.Cmd_Send_Option.AutoRet, msg, string.Empty);
				message.AutoReplyTime = DateTime.Now;
			}

			//触发事件
			MessageEventArgs e = new MessageEventArgs(message, host);
			OnTextMessageReceiving(e);
			if (!e.IsHandled) OnTextMessageReceived(e);
		}

		private void ProcessCommand_IsGetList2(Message message, Host host)
		{
			if (host == null) return;

		}

		private void ProcessCommand_AnsList(Message message, Host host)
		{
			if (host == null) return;

		}

		private void ProcessCommand_GetList(Message message, Host host)
		{
			if (host == null) return;

		}
		private void ProcessCommand_OkGetList(Message message, Host host)
		{
			if (host == null) return;

		}

		private void ProcessCommand_IsGetList(Message message, Host host)
		{
			if (host == null) return;

		}

		//离开状态或昵称更改
		private void ProcessCommand_BrAbsence(Message message, Host host)
		{
			if (host == null) return;

			host.ChangeAbsenceMode(Consts.Check(message.Options, Consts.Cmd_All_Option.Absence), "");
			if (!string.IsNullOrEmpty(message.NormalMsg)) host.NickName = message.NormalMsg;
			if (!string.IsNullOrEmpty(message.ExtendMessage)) host.GroupName = message.ExtendMessage;
			host.HostFeature = message.Options;
		}

		#endregion

		#region 处理内部对象的事件

		//捕捉主机离开状态变化
		void Host_AbsendModeChanged(object sender, EventArgs e)
		{
			Host h = sender as Host;
			if (h.IsInAbsenceMode)
			{
				//如果处于离开模式，则要求获得对象离开信息
				MessageProxy.SendWithNoCheck(h, Consts.Commands.GetAbsenceInfo, 0, "", "");
			}
		}

		#endregion

		#region 公共函数

		/// <summary>
		/// 发送上线消息
		/// </summary>
		public void SendEntryMessage()
		{
			ulong options = 0ul;
			if (Config.IsInAbsenceMode) options |= (ulong)Consts.Cmd_All_Option.Absence;
			if (Config.EnableFileTransfer) options |= (ulong)Define.Consts.Cmd_All_Option.FileAttach;
			//插件
			if (Config.Services != null) Config.Services.ProviderExecute(s => options |= s.GenerateClientFeatures());


			//广播
			MessageProxy.SendWithNoCheck(null, Consts.Commands.Br_Entry, options | (ulong)Consts.Cmd_Send_Option.BroadCast, Config.NickName, Config.GroupName);

			//单个通知
			options |= (ulong)Consts.Cmd_All_Option.DialUp;

			Config.KeepedHostList_Addr.ForEach(s => MessageProxy.SendByIp(new IPEndPoint(s, Config.Port), Consts.Commands.Br_Entry, options, Config.NickName, Config.GroupName));
		}

		/// <summary>
		/// 发送在线状态变化信息
		/// </summary>
		public void SendAbsenceMessage()
		{
			ulong options = 0ul;
			if (Config.IsInAbsenceMode) options |= (ulong)Consts.Cmd_All_Option.Absence;
			if (Config.EnableFileTransfer) options |= (ulong)Define.Consts.Cmd_All_Option.FileAttach;
			//插件
			if (Config.Services != null) Config.Services.ProviderExecute(s => options |= s.GenerateClientFeatures());

			string msg = Config.NickName;
			if (Config.IsInAbsenceMode) msg += Config.AbsenceSuffix;

			//广播
			MessageProxy.SendWithNoCheck(null, Consts.Commands.Br_Absence, options, msg, Config.GroupName);

			//单个通知
			options |= (ulong)Consts.Cmd_All_Option.DialUp;

			Config.KeepedHostList_Addr.ForEach(s => MessageProxy.SendByIp(new IPEndPoint(s, Config.Port), Consts.Commands.Br_Absence, options, msg, Config.GroupName));
		}

		/// <summary>
		/// 发送下线消息
		/// </summary>
		public void SendExitEntryMessage()
		{
			ulong options = 0ul;

			//广播
			MessageProxy.SendWithNoCheck(null, Consts.Commands.Br_Exit, options | (ulong)Consts.Cmd_Send_Option.BroadCast, Config.NickName, Config.GroupName);

			//单个通知
			options |= (ulong)Consts.Cmd_All_Option.DialUp;

			Config.KeepedHostList_Addr.ForEach(s => MessageProxy.SendByIp(new IPEndPoint(s, Config.Port), Consts.Commands.Br_Exit, options, Config.NickName, Config.GroupName));
		}

		/// <summary>
		/// 发送确认打开消息的包
		/// </summary>
		/// <param name="m">原始数据包</param>
		public void SendReadMessageSignal(Message m)
		{
			MessageProxy.SendWithNoCheck(m.Host, Consts.Commands.ReadMsg, 0, m.PackageNo.ToString(), "");
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="host">主机对象</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">选项</param>
		/// <param name="msg">消息</param>
		/// <param name="extMsg">扩展消息</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendCommand(Host host, Consts.Commands cmd, ulong options, string msg, string extMsg)
		{
			return SendCommand(host, cmd, options, msg, extMsg, false, false, false, false, false, false, false, false);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="endPoint">主机地址</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">选项</param>
		/// <param name="msg">消息</param>
		/// <param name="extMsg">扩展消息</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendCommand(IPEndPoint endPoint, Consts.Commands cmd, ulong options, string msg, string extMsg)
		{
			return SendCommand(null, endPoint, cmd, options, msg, extMsg);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="host">主机对象</param>
		/// <param name="endPoint">主机地址</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">选项</param>
		/// <param name="msg">消息</param>
		/// <param name="extMsg">扩展消息</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendCommand(Host host, IPEndPoint endPoint, Consts.Commands cmd, ulong options, string msg, string extMsg)
		{
			return SendCommand(host, endPoint, cmd, options, msg, extMsg, false, false, false, false, false, false, false, false);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="host">主机</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">选项</param>
		/// <param name="msg">消息</param>
		/// <param name="extMsg">扩展消息</param>
		/// <param name="isBroadCast">是否是广播</param>
		/// <param name="sendCheck">是否需要确认收到</param>
		/// <param name="noAddList">是否忽略到主机列表</param>
		/// <param name="noLog">是否不记录日志</param>
		/// <param name="isAutoRet">是否是自动回复</param>
		/// <param name="isEncrypt">是否是加密信息</param>
		/// <param name="isSecret">是否需要阅读确认</param>
		/// <param name="noPopup">是否不自动弹出</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendCommand(Host host, Consts.Commands cmd, ulong options, string msg, string extMsg, bool isBroadCast, bool sendCheck
			, bool noAddList, bool noLog, bool isAutoRet, bool isEncrypt, bool isSecret, bool noPopup)
		{
			return SendCommand(host, null, cmd, options, msg, extMsg, isBroadCast, sendCheck, noAddList, noLog, isAutoRet, isEncrypt, isSecret, noPopup);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="remoteEndPoint">远程主机地址</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">选项</param>
		/// <param name="msg">消息</param>
		/// <param name="extMsg">扩展消息</param>
		/// <param name="isBroadCast">是否是广播</param>
		/// <param name="sendCheck">是否需要确认收到</param>
		/// <param name="noAddList">是否忽略到主机列表</param>
		/// <param name="noLog">是否不记录日志</param>
		/// <param name="isAutoRet">是否是自动回复</param>
		/// <param name="isEncrypt">是否是加密信息</param>
		/// <param name="isSecret">是否需要阅读确认</param>
		/// <param name="noPopup">是否不自动弹出</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendCommand(IPEndPoint remoteEndPoint, Consts.Commands cmd, ulong options, string msg, string extMsg, bool isBroadCast, bool sendCheck, bool noAddList, bool noLog, bool isAutoRet, bool isEncrypt, bool isSecret, bool noPopup)
		{
			return SendCommand(null, remoteEndPoint, cmd, options, msg, extMsg, isBroadCast, sendCheck, noAddList, noLog, isAutoRet, isEncrypt, isSecret, noPopup);
		}

		/// <summary>
		/// 发送消息
		/// </summary>
		/// <param name="host">主机</param>
		/// <param name="remoteEndPoint">远程主机地址</param>
		/// <param name="cmd">命令</param>
		/// <param name="options">选项</param>
		/// <param name="msg">消息</param>
		/// <param name="extMsg">扩展消息</param>
		/// <param name="isBroadCast">是否是广播</param>
		/// <param name="sendCheck">是否需要确认收到</param>
		/// <param name="noAddList">是否忽略到主机列表</param>
		/// <param name="noLog">是否不记录日志</param>
		/// <param name="isAutoRet">是否是自动回复</param>
		/// <param name="isEncrypt">是否是加密信息</param>
		/// <param name="isSecret">是否需要阅读确认</param>
		/// <param name="noPopup">是否不自动弹出</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendCommand(Host host, IPEndPoint remoteEndPoint, Consts.Commands cmd, ulong options, string msg, string extMsg, bool isBroadCast, bool sendCheck
			, bool noAddList, bool noLog, bool isAutoRet, bool isEncrypt, bool isSecret, bool noPopup)
		{
			if (isBroadCast) options |= (ulong)Consts.Cmd_Send_Option.BroadCast;
			if (sendCheck) options |= (ulong)Consts.Cmd_Send_Option.SendCheck;
			if (noAddList) options |= (ulong)Consts.Cmd_Send_Option.NoAddList;
			if (noLog) options |= (ulong)Consts.Cmd_Send_Option.NoLog;
			if (isAutoRet) options |= (ulong)Consts.Cmd_Send_Option.AutoRet;
			if (isEncrypt) options |= (ulong)Consts.Cmd_All_Option.Encrypt;
			if (isSecret) options |= (ulong)Consts.Cmd_Send_Option.Secret;
			if (noPopup) options |= (ulong)Consts.Cmd_Send_Option.NoPopup;

			if (sendCheck && host != null)
			{
				return MessageProxy.SendWithCheck(host, cmd, options, msg, extMsg);
			}
			else
			{
				if (host == null) return MessageProxy.SendByIp(remoteEndPoint, cmd, options, msg, extMsg);
				else return MessageProxy.SendWithNoCheck(host, cmd, options, msg, extMsg);
			}
		}

		/// <summary>
		/// 查询客户端版本
		/// </summary>
		/// <param name="host">客户端</param>
		public void GetVersion(Host host)
		{
			SendCommand(host, Consts.Commands.GetInfo, 0ul, "", "");
		}

		/// <summary>
		/// 发送消息打开确认包
		/// </summary>
		/// <param name="host">远程主机</param>
		/// <param name="packageNo">包编号</param>
		public void SendMessageOpenedSignal(Host host, ulong packageNo)
		{
			SendCommand(host, Consts.Commands.ReadMsg, 0ul, packageNo.ToString(), "");
		}

		/// <summary>
		/// 发送文件取消接收消息
		/// </summary>
		/// <param name="host">远程主机</param>
		/// <param name="packageNo">包编号</param>
		public void SendReleaseFilesSignal(Host host, ulong packageNo)
		{
			SendCommand(host, Consts.Commands.ReleaseFiles, 0ul, packageNo.ToString(), "");
		}

		/// <summary>
		/// 向用户发送消息
		/// </summary>
		/// <param name="host">主机</param>
		/// <param name="content">消息内容</param>
		/// <param name="isHtml">是否HTML格式消息</param>
		/// <param name="isRtf">是否RTF格式消息</param>
		/// <param name="packed">是否封包</param>
		/// <param name="password">是否加密</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendTextMessage(Host host, string content, bool isHtml, bool isRtf, bool packed, bool password)
		{
			return SendTextMessage(host, content, isHtml, isRtf, packed, password, false, false, String.Empty);
		}

		/// <summary>
		/// 向用户发送消息
		/// </summary>
		/// <param name="host">主机</param>
		/// <param name="content">消息内容</param>
		/// <param name="isHtml">是否HTML格式消息</param>
		/// <param name="isRtf">是否RTF格式消息</param>
		/// <param name="packed">是否封包</param>
		/// <param name="isAutoReply">是否是自动回复信息</param>
		/// <param name="password">是否加密</param>
		/// <param name="attachContent">附件内容</param>
		/// <param name="isBoardCast">是否是广播</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendTextMessage(Host host, string content, bool isHtml, bool isRtf, bool packed, bool password, bool isBoardCast, bool isAutoReply, string attachContent)
		{
			ulong options = 0ul;
			if (isHtml) options |= (ulong)Consts.Cmd_Send_Option.Content_Html;
			if (isRtf) options |= (ulong)Consts.Cmd_Send_Option.Content_RTF;
			if (password) options |= (ulong)Consts.Cmd_Send_Option.Password;
			if (!string.IsNullOrEmpty(attachContent)) options |= (ulong)Consts.Cmd_All_Option.FileAttach | (ulong)Consts.Cmd_All_Option.NewFileAttach;

			return SendCommand(host, Consts.Commands.SendMsg, options, content, attachContent, isBoardCast, true, false, false, isAutoReply, host.SupportEncrypt, packed, false);
		}

		/// <summary>
		/// 向用户发送消息
		/// </summary>
		/// <param name="packageID">消息包ID</param>
		/// <param name="host">主机</param>
		/// <param name="content">消息内容</param>
		/// <param name="isHtml">是否HTML格式消息</param>
		/// <param name="isRtf">是否RTF格式消息</param>
		/// <param name="packed">是否封包</param>
		/// <param name="isAutoReply">是否是自动回复信息</param>
		/// <param name="password">是否加密</param>
		/// <param name="attachContent">附件内容</param>
		/// <param name="isBoardCast">是否是广播</param>
		/// <returns>发出去的消息包编号</returns>
		public ulong SendTextMessage(ulong packageID, Host host, string content, bool isHtml, bool isRtf, bool packed, bool password, bool isBoardCast, bool isAutoReply, string attachContent)
		{
			ulong options = 0ul;
			if (isHtml) options |= (ulong)Consts.Cmd_Send_Option.Content_Html;
			if (isRtf) options |= (ulong)Consts.Cmd_Send_Option.Content_RTF;
			if (password) options |= (ulong)Consts.Cmd_Send_Option.Password;
			if (!string.IsNullOrEmpty(attachContent)) options |= (ulong)Consts.Cmd_All_Option.FileAttach | (ulong)Consts.Cmd_All_Option.NewFileAttach;

			return SendCommand(host, Consts.Commands.SendMsg, options, content, attachContent, isBoardCast, true, false, false, isAutoReply, host.SupportEncrypt, packed, false);
		}

		#endregion


		#region 事件

		/// <summary>
		/// 消息将要处理事件
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageProcessing;
		SendOrPostCallback messageProcessingCall;

		/// <summary>
		/// 触发消息将要处理的事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnMessageProcessing(MessageEventArgs e)
		{
			if (MessageProcessing == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (messageProcessingCall == null) messageProcessingCall = (s) => MessageProcessing(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(messageProcessingCall, e);
			}
			else
			{
				MessageProcessing(this, e);
			}
		}

		/// <summary>
		/// 消息处理后触发
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageProcessed;
		SendOrPostCallback messageProcessedCall;

		/// <summary>
		/// 触发消息已处理的事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnMessageProcessed(MessageEventArgs e)
		{
			if (MessageProcessed == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (messageProcessedCall == null) messageProcessedCall = (s) => MessageProcessed(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(messageProcessedCall, e);
			}
			else
			{
				MessageProcessed(this, e);
			}
		}

		/// <summary>
		/// 收到文本消息事件
		/// </summary>
		public event EventHandler<MessageEventArgs> TextMessageReceiving;
		SendOrPostCallback textMessageReceivingCall;

		/// <summary>
		/// 触发收到文本消息事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnTextMessageReceiving(MessageEventArgs e)
		{
			if (TextMessageReceiving == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (textMessageReceivingCall == null) textMessageReceivingCall = s => TextMessageReceiving(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(textMessageReceivingCall, e);
			}
			else
			{
				TextMessageReceiving(this, e);
			}
		}

		/// <summary>
		/// 收到文本消息事件
		/// </summary>
		public event EventHandler<MessageEventArgs> TextMessageReceived;
		SendOrPostCallback textMessageReceivedCall;

		/// <summary>
		/// 触发收到文本消息事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnTextMessageReceived(MessageEventArgs e)
		{
			if (TextMessageReceived == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (textMessageReceivedCall == null) textMessageReceivedCall = s => TextMessageReceived(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(textMessageReceivedCall, e);
			}
			else
			{
				TextMessageReceived(this, e);
			}
		}

		/// <summary>
		/// 消息已经投递到对方事件
		/// </summary>
		public event EventHandler<MessageEventArgs> TextMessageArrived;
		SendOrPostCallback textMessageArrivedCall;

		/// <summary>
		/// 触发消息已经投递到对方事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnTextMessageArrived(MessageEventArgs e)
		{
			if (TextMessageArrived == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (textMessageArrivedCall == null) textMessageArrivedCall = (s) => TextMessageArrived(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(textMessageArrivedCall, e);
			}
			else
			{
				TextMessageArrived(this, e);
			}
		}

		/// <summary>
		/// 对方消息已经打开事件
		/// </summary>
		public event EventHandler<MessageEventArgs> TextMessageOpened;
		SendOrPostCallback textMessageOpenedCall;

		/// <summary>
		/// 触发消息对方消息已经打开事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnTextMessageOpened(MessageEventArgs e)
		{
			if (TextMessageOpened == null) return;

			if (!IPMClient.NeedPostMessage) TextMessageOpened(this, e);
			else
			{
				if (textMessageOpenedCall == null) textMessageOpenedCall = (s) => TextMessageOpened(this, s as MessageEventArgs);
				IPMClient.SendSynchronizeMessage(textMessageOpenedCall, e);
			}
		}

		#endregion


		#region 属性

		private Config _config;
		/// <summary>
		/// 配置类
		/// </summary>
		public Config Config
		{
			get
			{
				return _config;
			}
			set
			{
				_config = value;
				BroadCastEndPoint = new IPEndPoint(IPAddress.Broadcast, Config.Port);
			}
		}

		/// <summary>
		/// UDP通信类
		/// </summary>
		public UDPThread Client { get; set; }

		/// <summary>
		/// 网络在线主机状态
		/// </summary>
		public OnlineHost LivedHost { get; set; }
		/// <summary>
		/// 消息发送和代理类
		/// </summary>
		public MessageTranslator MessageProxy { get; set; }

		/// <summary>
		/// 广播远程节点
		/// </summary>
		public IPEndPoint BroadCastEndPoint { get; set; }

		/// <summary>
		/// 文件传输管理器
		/// </summary>
		public Core.FileTaskManager FileTaskManager { get; set; }

		/// <summary>
		/// 文件线程
		/// </summary>
		public Network.TCPThread FileTaskModule { get; private set; }


		#endregion

		#region IDisposable 成员

		bool disposed = false;

		void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					this.Client.Close();
					this.FileTaskModule.Dispose();
				}
			}

			disposed = true;
		}

		/// <summary>
		/// Dispose模式支持
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~CommandExecutor()
		{
			Dispose(false);
		}
		#endregion


	}

}
