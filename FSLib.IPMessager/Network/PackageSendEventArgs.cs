using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 发送失败的事件数据类
	/// </summary>
	public class PackageSendEventArgs : EventArgs
	{
		/// <summary>
		/// 发送失败的数据
		/// </summary>
		public PackedNetworkMessage Package { get; set; }

		/// <summary>
		/// 构造一个新的对象
		/// </summary>
		public PackageSendEventArgs()
		{
			Package = null;
		}
	}
}
