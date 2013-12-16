using System;
using System.Collections.Generic;
using System.Text;
using FSLib.IPMessager.Define;
using System.Net;


namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 信息封包对象
	/// </summary>
	/// <remarks></remarks>
	public class Message
	{
		/// <summary>
		/// 是否已经被处理.在挂钩过程中,如果为true,则底层代码不会再对信息进行处理
		/// </summary>
		public bool Handled { get; set; }

		//Public Parsed As Boolean
		//Public Ignored As Boolean

		/// <summary>
		/// 获得或设置当前的消息编号
		/// </summary>
		/// <value></value>
		/// <remarks></remarks>
		public ulong PackageNo { get; set; }

		/// <summary>
		/// 获得或设置当前的消息所属的主机名
		/// </summary>
		public string HostName { get; set; }

		/// <summary>
		/// 获得或设置当前的消息所属的用户名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 获得或设置当前的命令代码
		/// </summary>
		public Define.Consts.Commands Command { get; set; }

		/// <summary>
		/// 获得或设置当前的命令选项
		/// </summary>
		public ulong Options { get; set; }

		/// <summary>
		/// 获得或设置当前的命令消息文本
		/// </summary>
		public string NormalMsg { get; set; }

		/// <summary>
		/// 消息文本字节
		/// </summary>
		public byte[] NormalMsgBytes { get; set; }

		/// <summary>
		/// 扩展消息文本字节
		/// </summary>
		public byte[] ExtendMessageBytes { get; set; }

		/// <summary>
		/// 获得或设置当前命令的扩展文本
		/// </summary>
		public string ExtendMessage { get; set; }

		public IPEndPoint HostAddr { get; set; }

		private Host _host;
		/// <summary>
		/// 关联的主机
		/// </summary>
		public Host Host
		{
			get
			{
				return _host;
			}
			set
			{
				_host = value;
				if (value != null) HostAddr = value.HostSub.Ipv4Address;
			}
		}

		public Message(IPEndPoint Addr)
		{
			HostAddr = Addr;
			Handled = false;
		}

		public Message(IPEndPoint addr, ulong packagerNumber, string hostName, string userName, Consts.Commands command, ulong options, string message, string extendMessage)
		{
			HostAddr = addr;
			Handled = false;

			PackageNo = packagerNumber;
			HostName = hostName;
			UserName = userName;
			Command = command;
			Options = System.Convert.ToUInt32(options);
			NormalMsg = message;
			ExtendMessage = extendMessage;
		}


		/// <summary>
		/// 直接创建一个新的Message对象
		/// </summary>
		/// <param name="host">主机对象</param>
		/// <param name="addr">远程地址</param>
		/// <param name="packagerNumber">端口</param>
		/// <param name="hostName">主机名</param>
		/// <param name="userName">用户名</param>
		/// <param name="command">命令</param>
		/// <param name="options">选项</param>
		/// <param name="message">信息</param>
		/// <param name="extendMessage">扩展信息</param>
		/// <returns></returns>
		public static Message Create(Host host, IPEndPoint addr, ulong packagerNumber, string hostName, string userName, Consts.Commands command, ulong options, string message, string extendMessage)
		{
			return new Message(addr, packagerNumber, hostName, userName, command, options, message, extendMessage) { Host = host };
		}

		#region 扩展属性

		/// <summary>
		/// 获得或设置是否附带了文件
		/// </summary>
		public bool IsFileAttached
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_All_Option.FileAttach);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_All_Option.FileAttach;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置是不是新版本的文件传输协议
		/// </summary>
		public bool IsFileEnhancedProtocol
		{
			get
			{
				return IsFileAttached && Define.Consts.Check(Options, Consts.Cmd_All_Option.NewFileAttach);
			}
			set
			{
				if (value) IsFileAttached = true;
				ulong opt = (ulong)Consts.Cmd_All_Option.NewFileAttach;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置是否确认阅读的消息
		/// </summary>
		public bool IsReadMsgConfirmRequired
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.Secret);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.Secret;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置消息是否是自动回复的
		/// </summary>
		public bool IsAutoSendMessage
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.AutoRet);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.AutoRet;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置消息是否使用ReadCheck标志
		/// </summary>
		public bool IsReadCheck
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.ReadCheck);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.ReadCheck;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置消息收到时是否需要返回确认包
		/// </summary>
		public bool IsRequireReceiveCheck
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.SendCheck);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.SendCheck | (ulong)Consts.Cmd_Send_Option.ReadCheck | (ulong)Consts.Cmd_All_Option.RequireReceiveCheck;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置消息收到时是否需要返回确认包(ReadCheck)
		/// </summary>
		public bool IsRequireReadCheck
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.ReadCheck);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.ReadCheck;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 获得或设置是否是广播包
		/// </summary>
		public bool IsBroadCast
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.BroadCast);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.BroadCast;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 是否批量发送
		/// </summary>
		public bool IsMultiSend
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.MultiCast);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.MultiCast;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 是否加密
		/// </summary>
		public bool IsEncrypt
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_All_Option.Encrypt);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_All_Option.Encrypt;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 是否使用Unicode编码
		/// </summary>
		public bool IsEncodingUnicode
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.Content_Unicode);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.Content_Unicode;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 是否封包
		/// </summary>
		public bool IsSecret
		{
			get
			{
				return Define.Consts.Check(Options, Consts.Cmd_Send_Option.Secret);
			}
			set
			{
				ulong opt = (ulong)Consts.Cmd_Send_Option.Secret;
				if (!value) Options &= ~opt;
				else Options |= opt;
			}
		}

		/// <summary>
		/// 自动回复时间
		/// </summary>
		public DateTime? AutoReplyTime { get; set; }

		#endregion


	}
}
