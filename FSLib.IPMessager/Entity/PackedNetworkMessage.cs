using System;
using System.Net;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 将要发送的数据包信息
	/// </summary>
	public class PackedNetworkMessage : IComparable<PackedNetworkMessage>
	{
		/// <summary>
		/// 封包版本
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// 要发送的数据包
		/// </summary>
		public byte[] Data { get; set; }

		/// <summary>
		/// 远程地址
		/// </summary>
		public IPEndPoint RemoteIP { get; set; }

		/// <summary>
		/// 发送次数
		/// </summary>
		public int SendTimes { get; set; }

		/// <summary>
		/// 包编号
		/// </summary>
		public ulong PackageNo { get; set; }

		/// <summary>
		/// 分包索引
		/// </summary>
		public int PackageIndex { get; set; }

		/// <summary>
		/// 分包总数
		/// </summary>
		public int PackageCount { get; set; }

		/// <summary>
		/// 获得或设置是否需要返回已收到标志
		/// </summary>
		public bool IsReceiveSignalRequired { get; set; }

		public PackedNetworkMessage()
		{
			Version = 1;
		}

		#region IComparable<PackedNetworkMessage> 成员

		public int CompareTo(PackedNetworkMessage other)
		{
			return PackageIndex < other.PackageIndex ? -1 : 1;
		}

		#endregion
	}
}
