namespace FSLib.IPMessager
{

	using System.Collections.Generic;
	using System;
	using System.Net;

	namespace Entity
	{
		/// <summary>
		/// 使用的客户端通信配置
		/// </summary>
		/// <remarks></remarks>
		[Serializable]
		public class Config
		{


			/// <summary>
			/// 构造器
			/// </summary>
			/// <remarks></remarks>
			public Config()
			{
				Port = 2425;
				Buffer = 128 * 1024;
				TaskKeepTime = 60 * 10;	//发送文件任务十分钟后失效
				ConnectionTimeout = 10;

				HostName = Environment.MachineName;
				EnableFileTransfer = false;
			}

			#region 状态信息

			/// <summary>
			/// 是否在离开状态
			/// </summary>
			public bool IsInAbsenceMode { get; set; }

			/// <summary>
			/// 离开状态信息
			/// </summary>
			public string AbsenceMessage { get; set; }

			/// <summary>
			/// 离开信息后缀
			/// </summary>
			public string AbsenceSuffix { get; set; }

			/// <summary>
			/// 版本信息
			/// </summary>
			[System.Xml.Serialization.XmlIgnore]
			public string VersionInfo { get; set; }

			#endregion

			#region 特殊属性

			/// <summary>
			/// 已经被屏蔽的主机IP列表
			/// </summary>
			public List<string> BanedHost { get; set; }

			private List<IPAddress> _keepedHostList_Addr;

			/// <summary>
			/// DialUp的主机IP列表
			/// </summary>
			[System.Xml.Serialization.XmlIgnore]
			internal List<IPAddress> KeepedHostList_Addr
			{
				get
				{
					if (_keepedHostList_Addr == null)
					{
						if (_keepedHostList_Addr == null) _keepedHostList_Addr = new List<IPAddress>();
						else _keepedHostList_Addr.Clear();

						Array.ForEach(KeepedHostList.ToArray(), (s) =>
						{
							_keepedHostList_Addr.Add(IPAddress.Parse(s));
						});
					}
					return _keepedHostList_Addr;
				}
				set
				{
					_keepedHostList_Addr = value;
				}
			}

			/// <summary>
			/// 将主机添加到拨号列表
			/// </summary>
			/// <param name="address"></param>
			public void AddHostToDialList(IPAddress address)
			{
				string ip = address.ToString();

				if (KeepedHostList.Contains(ip)) return;

				KeepedHostList.Add(ip);
				KeepedHostList_Addr.Add(address);
			}

			/// <summary>
			/// 将主机从拨号列表中移除
			/// </summary>
			/// <param name="address"></param>
			public void RemoveHostFromDialList(string address)
			{
				IPAddress ip = IPAddress.Parse(address);

				if (!KeepedHostList.Contains(address)) return;

				KeepedHostList.Remove(address);
				KeepedHostList_Addr.Remove(ip);
			}

			private List<string> _keepedHostList;
			/// <summary>
			/// DialUp的主机IP列表
			/// </summary>
			public List<string> KeepedHostList
			{
				get
				{
					if (_keepedHostList == null) _keepedHostList = new List<string>();
					return _keepedHostList;
				}
				set
				{
					_keepedHostList = value;
				}
			}

			/// <summary>
			/// 离开时自动回复
			/// </summary>
			public bool AutoReplyWhenAbsence { get; set; }

			/// <summary>
			/// 自动回复
			/// </summary>
			public bool AutoReply { get; set; }

			/// <summary>
			/// 自动回复消息
			/// </summary>
			public string AutoReplyMessage { get; set; }

			private string _hostName;
			/// <summary>
			/// 主机名
			/// </summary>
			public string HostName
			{
				get
				{
					if (string.IsNullOrEmpty(_hostName)) return Environment.MachineName;
					else return _hostName;
				}
				set
				{
					_hostName = value;
				}
			}

			private string _hostUserName;
			/// <summary>
			/// 主机用户名
			/// </summary>
			public string HostUserName
			{
				get
				{
					if (string.IsNullOrEmpty(_hostUserName)) return Environment.UserName;
					else return _hostUserName;
				}
				set
				{
					_hostUserName = value;
				}
			}

			#endregion

			#region 网络设置

			/// <summary>
			/// 通信使用的端口
			/// </summary>
			/// <value></value>
			/// <returns></returns>
			/// <remarks>默认是2425,改变此端口后重新启动通信才可</remarks>
			public int Port { get; set; }

			private string _nickName;
			/// <summary>
			/// 飞鸽的用户名
			/// </summary>
			/// <value></value>
			/// <returns></returns>
			/// <remarks></remarks>
			public string NickName
			{
				get
				{
					if (string.IsNullOrEmpty(_nickName)) return this.HostUserName;
					else return _nickName;
				}
				set
				{
					_nickName = value;
				}
			}

			private string _groupName;
			/// <summary>
			/// 用户组(显示为飞鸽的用户组)
			/// </summary>
			/// <value>用户组</value>
			/// <returns>值</returns>
			/// <remarks>默认是 飞鸽用户</remarks>
			public string GroupName
			{
				get
				{
					if (string.IsNullOrEmpty(_groupName)) return this.HostName;
					else return _groupName;
				}
				set
				{
					_groupName = value;
				}
			}

			/// <summary>
			/// Socket的缓冲区大小,只读
			/// </summary>
			/// <value></value>
			/// <returns></returns>
			/// <remarks>由C版拓展而来,保留为 65535</remarks>
			[System.Xml.Serialization.XmlIgnore]
			public int SocketBuffer
			{
				get
				{
					return 65536;
				}
			}

			/// <summary>
			/// UDP通信的缓冲区大小
			/// </summary>
			/// <value></value>
			/// <returns></returns>
			/// <remarks>由C版转移而来,保留为16384</remarks>
			[System.Xml.Serialization.XmlIgnore]
			public int UdpBuffer
			{
				get
				{
					return 16384;
				}
			}

			/// <summary>
			/// 绑定的IP
			/// </summary>
			[System.Xml.Serialization.XmlIgnore]
			public IPAddress BindedIP { get; set; }

			/// <summary>
			/// 绑定的IP的字符串表现形式
			/// </summary>
			/// <remarks>这是供序列化使用的，请不要在代码中使用它</remarks>
			public string BindedIPString
			{
				get
				{
					return BindedIP.ToString();
				}
				set
				{
					BindedIP = IPAddress.Parse(value);
				}
			}

			/// <summary>
			/// 限制的内容
			/// </summary>
			public string[] FileShareLimits { get; set; }


			/// <summary>
			/// 忽略不加入列表的标志位
			/// </summary>
			public bool IgnoreNoAddListFlag { get; set; }

			/// <summary>
			/// 是否开启通知非本网段主机上线功能
			/// </summary>
			public bool EnableHostNotifyBroadcast { get; set; }

			/// <summary>
			/// 是否强制启用老版本协议
			/// </summary>
			public bool ForceOldContract { get; set; }

			/// <summary>
			/// 是否允许网络文件传输
			/// </summary>
			[System.Xml.Serialization.XmlIgnore]
			public bool EnableFileTransfer { get; set; }

			/// <summary>
			/// 忽略回传的消息
			/// </summary>
			public bool IgnoreLoopBack { get; set; }

			/// <summary>
			/// 自动查询主机版本
			/// </summary>
			public bool AutoDetectVersion { get; set; }

			/// <summary>
			/// TCP连接超时
			/// </summary>
			public int ConnectionTimeout { get; set; }

			#endregion

			#region 静态函数

			/// <summary>
			/// 加入一个主机到黑名单
			/// </summary>
			/// <param name="ip"></param>
			public void BanHost(IPAddress ip)
			{
				if (BanedHost == null) BanedHost = new List<string>();
				if (!BanedHost.Contains(ip.ToString())) BanedHost.Add(ip.ToString());
			}

			/// <summary>
			/// 加入一个主机到黑名单
			/// </summary>
			/// <param name="ip"></param>
			public void BanHost(Entity.Host ip)
			{
				BanHost(ip.HostSub.Ipv4Address.Address);
			}

			/// <summary>
			/// 检测一个主机是否在黑名单中
			/// </summary>
			/// <param name="ip"></param>
			/// <returns></returns>
			public bool IsHostInBlockList(IPAddress ip)
			{
				return (BanedHost != null && BanedHost.Contains(ip.ToString()));
			}

			/// <summary>
			/// 检测一个主机是否在黑名单中
			/// </summary>
			/// <param name="ip"></param>
			/// <returns></returns>
			public bool IsHostInBlockList(Entity.Host ip)
			{
				return IsHostInBlockList(ip.HostSub.Ipv4Address.Address);
			}

			/// <summary>
			/// 取消指定主机的屏蔽
			/// </summary>
			/// <param name="ip"></param>
			public void UnBanIP(IPAddress ip)
			{
				if (BanedHost != null && BanedHost.Contains(ip.ToString())) BanedHost.Remove(ip.ToString());
			}

			/// <summary>
			/// 清空屏蔽列表
			/// </summary>
			public void UnBanAllIp()
			{
				if (BanedHost != null) BanedHost.Clear();
			}

			//本次启动后包发送的序号
			static ulong packageIndex = 0;

			/// <summary>
			/// 获得随机的编号
			/// </summary>
			/// <returns></returns>
			public static ulong GetRandomTick()
			{
				return (ulong)((new Random()).Next()) + (packageIndex++);
			}

			#endregion

			#region 文件传输设置


			/// <summary>
			/// 文件传输的缓冲区大小,单位是字节
			/// </summary>
			/// <value></value>
			/// <returns></returns>
			/// <remarks></remarks>
			public int Buffer { get; set; }

			/// <summary>
			/// 文件发送任务过期时间(S)
			/// </summary>
			public int TaskKeepTime { get; set; }

			/// <summary>
			/// 同时接收文件的任务数
			/// </summary>
			public int TasksMultiReceiveCount { get; set; }

			/// <summary>
			/// 接收文件时允许断点续传
			/// </summary>
			public bool EnableBPContinue { get; set; }

			#endregion

			#region 插件设置

			/// <summary>
			/// 服务
			/// </summary>
			public Services.ServiceList Services { get; set; }

			#endregion

			#region 辅助函数

			/// <summary>
			/// 将本对象序列化
			/// </summary>
			/// <returns></returns>
			public string Serialize()
			{
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				System.Xml.Serialization.XmlSerializer bf = new System.Xml.Serialization.XmlSerializer(typeof(Config));
				bf.Serialize(ms, this);
				ms.Flush();

				string msg = System.Text.Encoding.Default.GetString(ms.ToArray());
				ms.Close();

				return msg;
			}

			/// <summary>
			/// 创建配置
			/// </summary>
			/// <param name="msg"></param>
			/// <returns></returns>
			public static Config CreateConfigFromString(string msg)
			{
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				byte[] buffer = System.Text.Encoding.Default.GetBytes(msg);
				ms.Write(buffer, 0, buffer.Length);
				ms.Seek(0, System.IO.SeekOrigin.Begin);

				System.Xml.Serialization.XmlSerializer bf = new System.Xml.Serialization.XmlSerializer(typeof(Config));

				Config cfg = bf.Deserialize(ms) as Config;
				ms.Close();

				if (cfg.Port <= 0 || cfg.Port > 65535) cfg.Port = 2425;
				if (string.IsNullOrEmpty(cfg.NickName)) cfg.NickName = "IPM Client";
				if (!string.IsNullOrEmpty(cfg.GroupName)) cfg.GroupName = "By 随风飘扬";

				return cfg;
			}

			#endregion

			#region 未用函数

			//Public ReadOnly Property CryptLen() As Integer
			//	Get
			//		Return CConfigBase.GetConfig("NetWork", "CryptLen", CInt((UdpBuffer - Buffer) / 2))
			//	End Get
			//End Property

			//Public ReadOnly Property NameBuf() As Integer
			//	Get
			//		Return CConfigBase.GetConfig("NetWork", "NameBuf", 50)
			//	End Get
			//End Property

			//Public ReadOnly Property LangBuf() As Integer
			//	Get
			//		Return CConfigBase.GetConfig("NetWork", "LangBuf", 10)
			//	End Get
			//End Property

			//Public ReadOnly Property ListBuf() As Integer
			//	Get
			//		Return CConfigBase.GetConfig("NetWork", "ListBuf", CInt(3 * NameBuf + 50))
			//	End Get
			//End Property

			//Public ReadOnly Property AnsList() As Integer
			//	Get
			//		Return CConfigBase.GetConfig("NetWork", "AnsList", 100)
			//	End Get
			//End Property

			//Public Sub ReloadSetting()
			//	_filesharelimits = CConfigBase.GetConfig("file", "visitauth", "").Split("|")

			//End Sub

			//#Region "主题控制"
			//		'''' <summary>
			//		'''' 界面的基础风格
			//		'''' </summary>
			//		'Public ReadOnly Property BaseColorSchema() As DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme
			//		'	Get
			//		'		Select Case CConfigBase.GetConfig("appearce", "colorschema", 0)
			//		'			Case 0
			//		'				Return DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue
			//		'			Case 1
			//		'				Return DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver
			//		'			Case 2
			//		'				Return DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Black
			//		'			Case Else
			//		'				Return DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Blue
			//		'		End Select
			//		'	End Get
			//		'End Property

			//		'''' <summary>
			//		'''' 界面基础颜色类型
			//		'''' </summary>
			//		'Public Enum BaseColorSchemaType As Integer
			//		'	Blue
			//		'	Silver
			//		'	Black
			//		'End Enum

			//		'''' <summary>
			//		'''' 记住基础颜色设置
			//		'''' </summary>
			//		'Public WriteOnly Property SetBaseColorSchema() As BaseColorSchemaType
			//		'	Set(ByVal value As BaseColorSchemaType)
			//		'		CConfigBase.SetConfig("appearce", "colorschema", CInt(value))
			//		'	End Set
			//		'End Property

			//		'Private _basecolor As System.Drawing.Color
			//		'''' <summary>
			//		'''' 界面的渲染颜色
			//		'''' </summary>
			//		'Public Property BaseColor() As System.Drawing.Color
			//		'	Get
			//		'		Return _basecolor
			//		'	End Get
			//		'	Set(ByVal value As System.Drawing.Color)
			//		'		_basecolor = value
			//		'		CConfigBase.SetConfig("appearce", "basecolor", FSLib.Drawing.Color.ColorHelper.TransFormColor2String(value, False))
			//		'	End Set
			//		'End Property

			//		'Public Sub ResetColorTable()
			//		'	DevComponents.DotNetBar.RibbonPredefinedColorSchemes.ChangeOffice2007ColorTable(BaseColorSchema, BaseColor)
			//		'End Sub
			#endregion

		}

	}
}
