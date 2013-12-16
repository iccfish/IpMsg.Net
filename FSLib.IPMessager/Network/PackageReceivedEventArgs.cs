using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 远程数据包收到时的信息包
	/// </summary>
	public class PackageReceivedEventArgs : EventArgs
	{
		/// <summary>
		/// 远程数据
		/// </summary>
		public byte[] Data { get; set; }

		/// <summary>
		/// 远程IP
		/// </summary>
		public IPEndPoint RemoteIP { get; set; }

		/// <summary>
		/// 是否已经处理
		/// </summary>
		public bool IsHandled { get; set; }

		/// <summary>
		/// 创建一个新的 PackageReceived 对象.
		/// </summary>
		public PackageReceivedEventArgs(byte[] data, IPEndPoint remoteIP)
		{
			Data = data;
			RemoteIP = remoteIP;
			IsHandled = false;
		}
		public PackageReceivedEventArgs()
			: this(null, null)
		{

		}

	}
}
