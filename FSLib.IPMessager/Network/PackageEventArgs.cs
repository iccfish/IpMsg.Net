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
	/// 数据包事件数据
	/// </summary>
	public class PackageEventArgs : EventArgs
	{
		/// <summary>
		/// 是否是多包
		/// </summary>
		public bool IsMultiPackage { get; set; }

		/// <summary>
		/// 网络消息包
		/// </summary>
		public Entity.PackedNetworkMessage Package { get; set; }

		/// <summary>
		/// 网络消息包组（仅针对版本2协议有效）
		/// </summary>
		public Entity.PackedNetworkMessage[] Packages { get; set; }

		/// <summary>
		/// 是否已经处理
		/// </summary>
		public bool IsHandled { get; set; }

		/// <summary>
		/// 创建一个新的 PackageEventArgs 对象.
		/// </summary>
		public PackageEventArgs(bool isMultiPackage, Entity.PackedNetworkMessage package, Entity.PackedNetworkMessage[] packages)
		{
			this.IsMultiPackage = isMultiPackage;
			this.Package = package;
			this.Packages = packages;
			this.IsHandled = false;
		}
	}
}
