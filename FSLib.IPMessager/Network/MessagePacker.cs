using System;
using System.Collections.Generic;
using System.Text;
using FSLib.IPMessager.Define;
using System.Net;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 封包消息-版本1
	/// </summary>
	/// <remarks>这个版本的封包消息与飞鸽传书VC版是兼容的</remarks>
	class MessagePacker
	{

		/// <summary>
		/// 消息版本号
		/// </summary>
		public static int VersionHeader { get { return 49; } }

		/// <summary>
		/// 返回当前消息封包的头字节数
		/// </summary>
		public static int PackageHeaderLength { get { return 3; } }

		/// <summary>
		/// 获得消息包的字节流
		/// </summary>
		/// <param name="message">要打包的消息对象</param>
		/// <returns></returns>
		public static Entity.PackedNetworkMessage BuildNetworkMessage(IPMessager.Entity.Message message)
		{
			if (message.NormalMsgBytes == null)
			{
				if (string.IsNullOrEmpty(message.NormalMsg)) message.NormalMsgBytes = new byte[] { };
				else message.NormalMsgBytes = System.Text.Encoding.Default.GetBytes(message.NormalMsg);
			}
			if (message.ExtendMessageBytes == null)
			{
				if (string.IsNullOrEmpty(message.ExtendMessage)) message.ExtendMessageBytes = new byte[] { };
				else message.ExtendMessageBytes = System.Text.Encoding.Default.GetBytes(message.ExtendMessage);
			}

			return BuildNetworkMessage(
				message.HostAddr,
				message.PackageNo,
				message.Command,
				message.Options,
				message.UserName,
				message.HostName,
				message.NormalMsgBytes,
				message.ExtendMessageBytes
				);
		}

		/// <summary>
		/// 获得消息包的字节流
		/// </summary>
		/// <param name="remoteIp">远程主机地址</param>
		/// <param name="packageNo">包编号</param>
		/// <param name="command">命令</param>
		/// <param name="options">参数</param>
		/// <param name="userName">用户名</param>
		/// <param name="hostName">主机名</param>
		/// <param name="content">正文消息</param>
		/// <param name="extendContents">扩展消息</param>
		/// <returns></returns>
		public static Entity.PackedNetworkMessage BuildNetworkMessage(IPEndPoint remoteIp, ulong packageNo, Define.Consts.Commands command, ulong options, string userName, string hostName, byte[] content, byte[] extendContents)
		{
			using (System.IO.MemoryStream bufferStream = new System.IO.MemoryStream())
			{
				int maxLength = Consts.MAX_UDP_PACKAGE_LENGTH;
				byte[] buffer;
				/*
							 * 注意：
							 * 1.优先保证扩展信息，如果不能保证则返回失败
							 * 2.保证扩展消息的前提下，文本消息可以截取
							 * */
				//写入消息头
				ulong cmdInfo = (ulong)command | options;
				buffer = System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}:", (char)Consts.VersionNumber, packageNo, userName, hostName, cmdInfo));
				bufferStream.Write(buffer, 0, buffer.Length);
				//计算扩展消息
				int extendMessageLength = extendContents == null ? 0 : extendContents.Length;
				if (extendMessageLength + bufferStream.Length > maxLength)
				{
					extendContents = null;
				}
				extendMessageLength = extendContents == null ? 0 : extendContents.Length + 1;
				//写入文本消息
				if (content != null)
				{
					if (content.Length <= maxLength - extendMessageLength - bufferStream.Length)
						bufferStream.Write(content, 0, content.Length);
					else
						bufferStream.Write(content, 0, maxLength - extendMessageLength - (int)bufferStream.Length);
				}
				//写入扩展消息？
				if (extendMessageLength > 0)
				{
					bufferStream.WriteByte(0);
					bufferStream.Write(extendContents, 0, extendMessageLength - 1);
				}
				bufferStream.Seek(0, System.IO.SeekOrigin.Begin);
				buffer = bufferStream.ToArray();

				return new Entity.PackedNetworkMessage()
				{
					PackageCount = 1,
					PackageIndex = 0,
					Data = buffer,
					SendTimes = 0,
					PackageNo = packageNo,
					RemoteIP = remoteIp,
					//回避BUG：全局参数里面，Absence 选项和 SendCheck 是一样的
					IsReceiveSignalRequired = command != Consts.Commands.Br_Absence && Define.Consts.Check(options, Consts.Cmd_Send_Option.SendCheck) && remoteIp.Address != IPAddress.Broadcast,
					Version = 0
				};
			}
		}


		/// <summary>
		/// 检测确认是否是这个类型的消息包
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static bool Test(byte[] buffer)
		{
			return buffer != null && buffer.Length > PackageHeaderLength && buffer[0] == VersionHeader;
		}

		/// <summary>
		/// 解析请求，并返回解析后的消息包
		/// </summary>
		/// <param name="buffer">数据包</param>
		/// <param name="remoteEndPoint">远程端点位置</param>
		/// <returns></returns>
		public static IPMessager.Entity.Message TryToTranslateMessage(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (!Test(buffer)) return null;

			IPMessager.Entity.Message m = new FSLib.IPMessager.Entity.Message(remoteEndPoint);
			int index = 0;
			int nextMatch = index;

			//查找的委托
			Func<char, bool> FindIndex = (c) =>
			{
				int flag = (int)c;
				while (nextMatch < buffer.Length && buffer[nextMatch] != flag) nextMatch++;
				return nextMatch <= buffer.Length;
			};
			Func<string> GetSubString = () =>
			{
				string str = System.Text.Encoding.Default.GetString(buffer, index, nextMatch - index);
				index = ++nextMatch;
				return str;
			};
			Func<ulong> GetULong = () =>
			{
				ulong t1;
				if (!ulong.TryParse(GetSubString(), out t1)) return 0;
				else return t1;
			};

			//设置数据包开始-为了兼容飞秋
			FindIndex(':');
			index = ++nextMatch;
			//查找包编号
			if (!FindIndex(':')) return null;
			m.PackageNo = GetULong();
			//用户名
			if (m.PackageNo == 0 || !FindIndex(':')) return null;
			m.UserName = GetSubString();
			//主机名
			if (!FindIndex(':')) return null;
			m.HostName = GetSubString();
			//查找命令和选项
			if (!FindIndex(':')) return null;
			ulong temp = ulong.Parse(GetSubString());
			m.Command = (Consts.Commands)(temp & 0xFF);
			m.Options = temp & 0xFFFFFF00;
			//正文？
			if (FindIndex('\0'))
			{
				m.NormalMsg = GetSubString();
			}
			else
			{
				m.NormalMsg = string.Empty;
			}
			if (index < buffer.Length)
			{
				nextMatch = buffer.Length;
				m.ExtendMessage = GetSubString().TrimEnd('\0');
			}
			else
			{
				m.ExtendMessage = string.Empty;
			}

			return m;
		}
	}
}
