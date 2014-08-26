using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using FSLib.IPMessager.Define;
using System.Net;
using FSLib.IPMessager.Entity;


namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 封包消息-版本2，这个版本的封包消息本身是经过压缩的，并且支持消息分片
	/// </summary>
	/// <remarks>这个版本的封包消息飞鸽传书VC版是不支持的</remarks>
	class MessagePackerV2
	{
		Timer _timer;

		public MessagePackerV2()
		{
			_timer = new Timer(_ => CheckForOutdateMessage(), null, new TimeSpan(0, 5, 0), new TimeSpan(0, 0, 5, 0));
		}

		/*
		 * 版本2消息包注意：
		 * 1.第一位始终是2(ASCII码50)
		 * 2.第二位到第九位是一个ulong类型的整数，代表消息编号
		 * 3.第十位到第十三位是一个int类型的整数，代表消息内容总长度
		 * 4.第十四位到第十七位是一个int类型的整数，代表分包的总数
		 * 5.第十八位到第二十一位是一个uint类型的整数，代表当前的分包编号
		 * 6.第二十二位表示是否需要返回一个确认标识(1/0)
		 * 7.第二十三到第三十一位是保留的(Reserved)
		 * 8.第三十二字节以后是数据包
		 * */

		/// <summary>
		/// 消息版本号
		/// </summary>
		public static byte VersionHeader { get { return 50; } }

		/// <summary>
		/// 返回当前消息封包的头字节数
		/// </summary>
		public static int PackageHeaderLength { get { return 32; } }

		/// <summary>
		/// 获得消息包的字节流
		/// </summary>
		/// <param name="message">要打包的消息对象</param>
		/// <returns></returns>
		public static PackedNetworkMessage[] BuildNetworkMessage(IPMessager.Entity.Message message)
		{
			if (message.ExtendMessageBytes != null)
			{
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
			else
			{
				return BuildNetworkMessage(
				message.HostAddr,
				message.PackageNo,
				message.Command,
				message.Options,
				message.UserName,
				message.HostName,
				System.Text.Encoding.Unicode.GetBytes(message.NormalMsg),
				System.Text.Encoding.Unicode.GetBytes(message.ExtendMessage)
				);
			}
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
		public static Entity.PackedNetworkMessage[] BuildNetworkMessage(IPEndPoint remoteIp, ulong packageNo, Define.Consts.Commands command, ulong options, string userName, string hostName, byte[] content, byte[] extendContents)
		{
			options |= (ulong)Define.Consts.Cmd_Send_Option.Content_Unicode;

			//每次发送所能容下的数据量
			var maxBytesPerPackage = Define.Consts.MAX_UDP_PACKAGE_LENGTH - PackageHeaderLength;
			//压缩数据流
			var ms = new System.IO.MemoryStream();
			var zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress);
			var bw = new System.IO.BinaryWriter(zip, System.Text.Encoding.Unicode);
			//写入头部数据
			bw.Write(packageNo);						//包编号
			bw.Write(((ulong)command) | options);		//命令|选项

			bw.Write(userName);				//用户名
			bw.Write(hostName);				//主机名

			bw.Write(content == null ? 0 : content.Length);					//数据长度

			//写入消息数据
			if (content != null) bw.Write(content);
			bw.Write(extendContents == null ? 0 : extendContents.Length);	//补充数据长度
			if (extendContents != null) bw.Write(extendContents);
			bw.Close();
			zip.Close();
			ms.Flush();
			ms.Seek(0, System.IO.SeekOrigin.Begin);

			//打包数据总量
			var dataLength = (int)ms.Length;

			var packageCount = (int)Math.Ceiling(dataLength * 1.0 / maxBytesPerPackage);
			var pnma = new PackedNetworkMessage[packageCount];
			for (var i = 0; i < packageCount; i++)
			{
				var count = i == packageCount - 1 ? dataLength - maxBytesPerPackage * (packageCount - 1) : maxBytesPerPackage;

				var buf = new byte[count + PackageHeaderLength];
				buf[0] = VersionHeader;
				BitConverter.GetBytes(packageNo).CopyTo(buf, 1);
				BitConverter.GetBytes(dataLength).CopyTo(buf, 9);
				BitConverter.GetBytes(packageCount).CopyTo(buf, 13);
				BitConverter.GetBytes(i).CopyTo(buf, 17);
				buf[21] = Define.Consts.Check(options, Define.Consts.Cmd_All_Option.RequireReceiveCheck) ? (byte)1 : (byte)0;	//包确认标志？

				ms.Read(buf, 32, buf.Length - 32);

				pnma[i] = new Entity.PackedNetworkMessage()
				{
					Data = buf,
					PackageCount = packageCount,
					PackageIndex = i,
					PackageNo = packageNo,
					RemoteIP = remoteIp,
					SendTimes = 0,
					Version = 2,
					IsReceiveSignalRequired = buf[21] == 1
				};
			}
			ms.Close();

			return pnma;
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
		/// 缓存接收到的片段
		/// </summary>
		static Dictionary<ulong, Entity.PackedNetworkMessage[]> packageCache = new Dictionary<ulong, PackedNetworkMessage[]>();

		/// <summary>
		/// 组合所有的网络数据包并执行解压缩
		/// </summary>
		/// <param name="packs"></param>
		/// <returns></returns>
		static MemoryStream DecompressMessagePacks(params PackedNetworkMessage[] packs)
		{
			try
			{
				//尝试解压缩，先排序
				Array.Sort(packs);
				var msout = new MemoryStream();
				using (var ms = new System.IO.MemoryStream())
				{
					//合并写入
					Array.ForEach(packs, s => ms.Write(s.Data, 0, s.Data.Length));
					ms.Seek(0, SeekOrigin.Begin);

					//解压缩
					using (var gz = new GZipStream(ms, CompressionMode.Decompress))
					{
						var buffer = new byte[0x400];
						var count = 0;
						while ((count = gz.Read(buffer, 0, buffer.Length)) > 0)
						{
							msout.Write(buffer, 0, count);
						}
					}
				}
				msout.Seek(0, SeekOrigin.Begin);

				return msout;
			}
			catch (Exception)
			{

				return null;
			}
		}

		/// <summary>
		/// 分析网络数据包并进行转换为信息对象
		/// </summary>
		/// <param name="packs">接收到的封包对象</param>
		/// <returns></returns>
		/// <remarks>
		/// 对于分包消息，如果收到的只是片段并且尚未接收完全，则不会进行解析
		/// </remarks>
		public static IPMessager.Entity.Message ParseToMessage(params Entity.PackedNetworkMessage[] packs)
		{
			if (packs.Length == 0 || (packs[0].PackageCount > 1 && packs.Length != packs[0].PackageCount))
				return null;

			var ms = DecompressMessagePacks(packs);
			if (ms == null)
			{
				OnDecompressFailed(new DecomprssFailedEventArgs(packs));
				return null;
			}

			//构造读取流
			var br = new System.IO.BinaryReader(ms, System.Text.Encoding.Unicode);

			//开始读出数据
			var m = new FSLib.IPMessager.Entity.Message(packs[0].RemoteIP);
			m.PackageNo = br.ReadUInt64();						//包编号
			var tl = br.ReadUInt64();
			m.Command = (Define.Consts.Commands)(tl & 0xFF);	//命令编码
			m.Options = tl & 0xFFFFFF00;						//命令参数

			m.UserName = br.ReadString();	//用户名
			m.HostName = br.ReadString();	//主机名

			var length = br.ReadInt32();
			m.NormalMsgBytes = new byte[length];
			br.Read(m.NormalMsgBytes, 0, length);

			length = br.ReadInt32();
			m.ExtendMessageBytes = new byte[length];
			br.Read(m.ExtendMessageBytes, 0, length);

			if (!Consts.Check(m.Options, Consts.Cmd_All_Option.BinaryMessage))
			{
				m.NormalMsg = System.Text.Encoding.Unicode.GetString(m.NormalMsgBytes, 0, length);	//正文
				m.ExtendMessage = System.Text.Encoding.Unicode.GetString(m.ExtendMessageBytes, 0, length);	//扩展消息
			}

			return m;
		}

		/// <summary>
		/// 尝试将收到的网络包解析为实体
		/// </summary>
		/// <param name="pack">收到的网络包</param>
		/// <returns></returns>
		/// <remarks>如果收到的包是分片包，且其所有子包尚未接受完全，则会返回空值</remarks>
		public static IPMessager.Entity.Message TryToTranslateMessage(Entity.PackedNetworkMessage pack)
		{
			if (pack == null || pack.PackageIndex >= pack.PackageCount - 1) return null;
			if (pack.PackageCount == 1) return ParseToMessage(pack);

			lock (packageCache)
			{
				if (packageCache.ContainsKey(pack.PackageNo))
				{
					var array = packageCache[pack.PackageNo];
					array[pack.PackageIndex] = pack;

					//检测是否完整
					//TODO 这里没有过期机制.当丢失数据包永远收不到的时候,会发生数据包永远保存在内存中的泄露
					//TODO 要解决此问题,需要启动定时器定时检查数据包队列,并丢弃过期数据
					if (Array.FindIndex(array, s => s == null) == -1)
					{
						packageCache.Remove(pack.PackageNo);
						return ParseToMessage(array);
					}
					return null;
				}
				else
				{
					var array = new Entity.PackedNetworkMessage[pack.PackageCount];
					array[pack.PackageIndex] = pack;
					packageCache.Add(pack.PackageNo, array);
					return null;
				}
			}
		}

		/// <summary>
		/// 将网络信息解析为封包
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public static Entity.PackedNetworkMessage Parse(byte[] buffer, IPEndPoint clientAddress)
		{
			if (!Test(buffer)) return null;

			var p = new PackedNetworkMessage()
			{
				RemoteIP = clientAddress,
				SendTimes = 0
			};
			p.PackageNo = BitConverter.ToUInt64(buffer, 1);
			//p. = BitConverter.ToUInt64(buffer, 9); //长度
			p.PackageCount = BitConverter.ToInt32(buffer, 17);
			p.PackageIndex = BitConverter.ToInt32(buffer, 21);
			p.IsReceiveSignalRequired = buffer[21] == 1;
			p.Data = new byte[buffer.Length - PackageHeaderLength];
			Array.Copy(buffer, PackageHeaderLength, p.Data, 0, p.Data.Length);

			return p;
		}

		void CheckForOutdateMessage()
		{
			lock (packageCache)
			{
				//TODO 这里设置最短的过期时间为5分钟，也就是说五分钟之前的消息会被干掉
				var minTime = DateTime.Now.AddMinutes(5.0);
				var targetList = new List<ulong>();
				foreach (var pkgid in packageCache.Keys)
				{
					if (Array.TrueForAll(packageCache[pkgid], s => s == null || s.CreationTime < minTime))
					{
						targetList.Add(pkgid);
					}
				}

				foreach (var pkgid in targetList)
				{
					packageCache.Remove(pkgid);
				}
			}
			
		}

		#region 事件

		/// <summary>
		/// 解压缩失败的事件数据
		/// </summary>
		public class DecomprssFailedEventArgs : EventArgs
		{
			/// <summary>
			/// 解压缩失败的网络包
			/// </summary>
			public Entity.PackedNetworkMessage[] Packs { get; set; }

			/// <summary>
			/// 创建一个新的 DecomprssFailedEventArgs 对象.
			/// </summary>
			public DecomprssFailedEventArgs(Entity.PackedNetworkMessage[] packs)
			{

				Packs = packs;
			}
		}

		/// <summary>
		/// 网络层数据包解压缩失败
		/// </summary>
		public static event EventHandler<DecomprssFailedEventArgs> DecompressFailed;

		/// <summary>
		/// 触发解压缩失败事件
		/// </summary>
		/// <param name="e">事件包含的参数</param>
		protected static void OnDecompressFailed(DecomprssFailedEventArgs e)
		{
			if (DecompressFailed != null) DecompressFailed(typeof(MessagePackerV2), e);
		}


		#endregion
	}
}
