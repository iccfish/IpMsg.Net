using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using FSLib.IPMessager.Core;
using FSLib.IPMessager.Entity;
using System.Threading;
using FSLib.IPMessager.Services;

namespace FSLib.IPMessager
{

	/// <summary>
	/// FSLib.IPMessager 的客户端对象
	/// <para>这是核心的飞鸽传书.Net对象，通常来说，所有的操作都会通过这个对象内部封装的对象来执行。在绝大多数情况下，应该只使用本对象来初始化整个飞鸽传书.Net网络客户端。</para>
	/// <para>本对象内部封装了 UDP协议实现、TCP协议实现、文件传输管理、命令解析执行、在线列表维护、插件 等对象的实例。</para>
	/// </summary>
	/// <remarks></remarks>
	public class IPMClient : IDisposable
	{

		bool _isInitialized = false;
		SynchronizationContext _context;

		/// <summary>
		/// Bin文件所在基础路径
		/// </summary>
		internal static string RootPath;

		static IPMClient()
		{
			RootPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		}

		/// <summary>
		/// 本地IP地址
		/// </summary>
		public IPAddress[] LocalAddresses { get; set; }

		#region 构造函数

		/// <summary>
		/// 创建一个新的客户端
		/// </summary>
		/// <param name="port">使用的端口</param>
		/// <param name="username">昵称</param>
		/// <param name="userGroup">用户组</param>
		/// <param name="ip">要绑定到的IP地址</param>
		/// <remarks></remarks>
		public IPMClient(int port, string username, string userGroup, IPAddress ip)
		{
			if (Config == null) Config = new Config
											{
												AbsenceMessage = "有事暂时不在，稍后联系你",
												IsInAbsenceMode = false,
												IgnoreNoAddListFlag = false,
												AutoReply = false,
												AutoReplyMessage = "暂时不在，稍后联系你",
												AutoReplyWhenAbsence = true,
												EnableHostNotifyBroadcast = false,
												ForceOldContract = true
											};

			Config.Port = port > 0 && port <= 65535 ? port : 2425;
			Config.NickName = !string.IsNullOrEmpty(username) ? username : "IPM Client";
			Config.GroupName = !string.IsNullOrEmpty(userGroup) ? userGroup : "By 随风飘扬";
			Config.BindedIP = ip;

			Initialize(Config);
		}

		/// <summary>
		/// 使用默认参数构建一个新的客户端
		/// </summary>
		/// <remarks></remarks>
		public IPMClient()
			: this(2425, null, null, IPAddress.Any)
		{
		}

		/// <summary>
		/// 指定创建的端口
		/// </summary>
		/// <param name="port">端口</param>
		/// <remarks></remarks>
		public IPMClient(int port)
			: this(port, null, null, IPAddress.Any)
		{
		}

		/// <summary>
		/// 指定创建的端口和IP的对象
		/// </summary>
		/// <param name="port">端口</param>
		/// <param name="ip">要绑定到的IP地址</param>
		/// <remarks></remarks>
		public IPMClient(int port, IPAddress ip)
			: this(port, null, null, ip)
		{
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="cfg">要使用的配置</param>
		public IPMClient(Config cfg)
		{
			Initialize(cfg);
			InitializeEvents();
		}

		/// <summary>
		/// 使用配置的XML文件创建对象
		/// </summary>
		/// <param name="configXml">配置的XML内容</param>
		/// <returns></returns>
		public static IPMClient Create(string configXml)
		{
			Config cfg = Config.CreateConfigFromString(configXml);

			return new IPMClient(cfg);
		}

		#endregion

		#region 属性

		/// <summary>
		/// 获得创建客户端时所使用的线程上下文
		/// </summary>
		public SynchronizationContext ThreadContext
		{
			get { return _context; }
		}


		/// <summary>
		/// 返回当前的配置对象
		/// </summary>
		public Config Config { get; private set; }

		/// <summary>
		/// UDP文本信息网络层对象
		/// </summary>
		public Network.UDPThread MessageClient { get { return Commander.Client; } }

		/// <summary>
		/// 文本信息翻译层对象
		/// </summary>
		public Network.MessageTranslator MessageProxy { get { return Commander.MessageProxy; } }

		/// <summary>
		/// 在线主机维护列表
		/// </summary>
		public Entity.OnlineHost OnlineHost { get { return Commander.LivedHost; } }

		/// <summary>
		/// 代表自己的主机信息
		/// </summary>
		public Entity.Host Host { get; private set; }

		/// <summary>
		/// 命令解释执行对象
		/// </summary>
		public Core.CommandExecutor Commander { get; private set; }

		/// <summary>
		/// 文件传输管理
		/// </summary>
		public Core.FileTaskManager FileTaskManager { get { return Commander.FileTaskManager; } }

		/// <summary>
		/// 文件传输管理
		/// </summary>
		public Network.TCPThread FileTaskModule { get { return Commander.FileTaskModule; } }

		/// <summary>
		/// 是否启用内部的IP过滤机制
		/// </summary>
		public Services.ServiceList ServiceList { get { return Config.Services; } }

		/// <summary>
		/// 返回是否初始化成功
		/// </summary>
		public bool IsInitialized
		{
			get
			{
				return this._isInitialized;
			}
		}

		/// <summary>
		/// 调试器对象
		/// </summary>
		public Debug.DebugHelper Debugger { get; private set; }

		#endregion

		#region 公共函数

		/// <summary>
		/// 初始化
		/// </summary>
		/// <param name="cfg">初始化使用的配置</param>
		private void Initialize(Config cfg)
		{
			if (_isInitialized) throw new InvalidOperationException("already initialized.");

			System.Diagnostics.Debug.WriteLine("IPMClient 开始初始化...端口：" + cfg.Port.ToString());

			_context = SynchronizationContext.Current;
			SynchronizationContext = SynchronizationContext.Current;

			LocalAddresses = Helper.GetLocalAddresses();

			this.Config = cfg;

			if (cfg.BindedIP == null) cfg.BindedIP = IPAddress.Any;


			//开始构造对象
			Commander = new CommandExecutor(this);
			Commander.Init();

			//初始化插件
			InitializeServiceProvider();

			//初始化调试器
			Debugger = new Debug.DebugHelper();
			Debugger.AttachHelperAuto(this);

			this._isInitialized = Commander.IsInitialized;
		}

		/// <summary>
		/// 初始化服务插件
		/// </summary>
		void InitializeServiceProvider()
		{
			if (Config.Services != null)
				Config.Services.ForEach(s => s.Load(this, true));
		}

		/// <summary>
		/// 发送在线讯号,通知网段所有主机本机已经在线
		/// </summary>
		/// <remarks>
		/// 这里的通知信号包括了两个部分，
		/// 一是通过广播消息的格式发送的，意味着这只能通知同网段的主机。
		/// 二是通过点对点发送的，发送的内容包含在 <see cref="FSLib.IPMessager.Entity.Config.KeepedHostList"/> 列表中的主机
		/// </remarks>
		public void Online()
		{
			Commander.SendEntryMessage();
		}

		/// <summary>
		/// 离线讯号,通知网段主机本机离线并关闭网络连接
		/// </summary>
		/// <remarks>
		/// 这里的通知信号包括了两个部分，
		/// 一是通过广播消息的格式发送的，意味着这只能通知同网段的主机。
		/// 二是通过点对点发送的，发送的内容包含在 <see cref="FSLib.IPMessager.Entity.Config.KeepedHostList"/> 列表中的主机
		///</remarks>
		public void OffLine()
		{
			Commander.SendExitEntryMessage();
		}

		/// <summary>
		/// 更改离开模式
		/// <para>要改变离开模式（离开或在线），通过调用这个函数来实现。</para>
		/// <para>值得注意的是，对于更改昵称、组别等信息，也是通过这个函数来实现更新的。</para>
		/// </summary>
		/// <param name="inAbsenceMode">是否是离开状态</param>
		/// <param name="message">离开状态信息</param>
		public void ChangeAbsenceMode(bool inAbsenceMode, string message)
		{
			Config.IsInAbsenceMode = inAbsenceMode;
			Config.AbsenceMessage = message;

			Commander.SendAbsenceMessage();
		}

		/// <summary>
		/// 向用户发送文件
		/// </summary>
		/// <param name="taskItem">文件列表</param>
		/// <param name="textMessage">附加的文本信息</param>
		/// <param name="isRtf">附加的文本信息是RTF格式</param>
		/// <param name="isHtml">附加的文本信息是HTML格式</param>
		/// <param name="remoteHost">远程主机</param>
		public void PerformSendFile(FileTaskItem[] taskItem, string textMessage, bool isRtf, bool isHtml, Host remoteHost)
		{
			FileTaskInfo task = new FileTaskInfo(FileTransferDirection.Send, 0ul, remoteHost);
			task.TaskList.AddRange(taskItem);

			task.PackageID = Commander.SendTextMessage(remoteHost, textMessage, isHtml, isRtf, false, false, false, false, taskItem.BuildTaskMessage());
			if (task.PackageID > 0) FileTaskManager.AddSendTask(task);
		}

		/// <summary>
		/// 检测指定的插件服务是否处于运行状态
		/// <seealso cref="InnerService"/>
		/// </summary>
		/// <param name="service">内置插件服务类型，这里是 <see cref="InnerService"/> 的枚举类型</param>
		/// <returns>检查结果,如果正常加载则返回true,否则返回 false</returns>
		public bool IsInnerServiceEnabled(Services.InnerService service)
		{
			return GetServiceState(service) == FSLib.IPMessager.Services.ServiceState.Running;
		}

		/// <summary>
		/// 检测指定的插件服务运行状态
		/// </summary>
		/// <param name="service">内置插件服务类型</param>
		/// <returns>检查结果</returns>
		public ServiceState GetServiceState(InnerService service)
		{
			return GetServiceState(ServiceManager.InnerServiceTypeList[service]);
		}

		/// <summary>
		/// 检测指定的插件服务运行状态
		/// </summary>
		/// <param name="serviceTypeName">插件名称</param>
		/// <returns>检查结果</returns>
		public ServiceState GetServiceState(string serviceTypeName)
		{
			ServiceInfo si = ServiceList.Find(s => s.TypeName == serviceTypeName);

			return si == null ? ServiceState.NotInstalled : si.State;
		}

		#endregion

		#region 资源管理

		private bool _disposedValue = false; // 检测冗余的调用

		// IDisposable
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					if (this._isInitialized)
					{
						OffLine();
						Commander.Dispose();
						this._isInitialized = false;
					}
				}

				// TODO: 释放您自己的状态(非托管对象)。
				// TODO: 将大型字段设置为 null。
			}
			this._disposedValue = true;
		}

		#region " IDisposable Support "
		/// <summary>
		/// 关闭网络对象
		/// </summary>
		/// <remarks></remarks>
		public void Dispose()
		{
			// 不要更改此代码。请将清理代码放入上面的 Dispose(bool disposing) 中。
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#endregion

		#region 线程同步

		/// <summary>
		/// 线程同步上下文
		/// </summary>
		internal static SynchronizationContext SynchronizationContext;

		/// <summary>
		/// 返回是否需要提交线程同步信息
		/// </summary>
		internal static bool NeedPostMessage { get { return SynchronizationContext != null; } }

		/// <summary>
		/// 提交同步消息
		/// </summary>
		/// <param name="call">调用的委托</param>
		/// <param name="arg">参数</param>
		internal static void SendSynchronizeMessage(SendOrPostCallback call, object arg)
		{
			if (!NeedPostMessage) call(arg);
			else SynchronizationContext.Send(call, arg);
		}

		/// <summary>
		/// 提交异步消息
		/// </summary>
		/// <param name="call">调用的委托</param>
		/// <param name="arg">参数</param>
		internal static void SendASynchronizeMessage(SendOrPostCallback call, object arg)
		{
			if (!NeedPostMessage) call(arg);
			else SynchronizationContext.Post(call, arg);
		}


		#endregion


		#region 事件定义

		/// <summary>
		/// 初始化事件
		/// </summary>
		void InitializeEvents()
		{
			if (!this.IsInitialized) return;

			//挂载一些内部事件
			this.Commander.TextMessageReceived += Commander_TextMessageReceived;
		}

		/// <summary>
		/// 预处理信息，检测是否有文件请求，有就处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Commander_TextMessageReceived(object sender, MessageEventArgs e)
		{
			if (e.IsHandled || !e.Message.IsFileAttached) return;

			var files = FileTaskItemHelper.DecompileTaskInfo(e.Host, e.Message);
			if (files != null)
			{
				var ea = new FileReceivedEventArgs(e.Host, e.Message, files);
				this.FileTaskManager.OnFileReceived(ea);

				//如果没有正文信息，就标记为已处理
				if (string.IsNullOrEmpty(e.Message.NormalMsg.Trim())) e.IsHandled = true;
			}
		}




		#endregion

		#region 辅助函数

		/// <summary>
		/// 生成默认的设置
		/// </summary>
		/// <param name="addinPath">默认扫描插件的目录</param>
		/// <returns></returns>
		public static Config GetDefaultConfig(params string[] addinPath)
		{
			return new Config
				{
					Port = 2425,
					GroupName = Environment.MachineName,
					NickName = Environment.UserName,
					ForceOldContract = true,
					IgnoreNoAddListFlag = false,
					EnableHostNotifyBroadcast = false,
					HostName = Environment.MachineName,
					AutoReplyWhenAbsence = true,
					AutoDetectVersion = true,
					BanedHost = new List<string>(),
					KeepedHostList = new List<string>(),
					BindedIP = IPAddress.Any,
					VersionInfo = String.Format("飞鸽传书.Net {0}，BY 木鱼", System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion),
					AbsenceSuffix = " [离开]",
					Services = ServiceManager.GetServices(addinPath),
					EnableBPContinue = true,
					TaskKeepTime = 600,
					TasksMultiReceiveCount = 1
				};
		}

		#endregion
	}

}
