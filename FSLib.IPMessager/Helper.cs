using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using FSLib.IPMessager.Entity;
using System.Net;

namespace FSLib.IPMessager
{

	/// <summary>
	/// 结构的定义,类的定义
	/// </summary>
	/// <remarks></remarks>
	public static class Helper
	{
		/// <summary>
		/// 辅助类，用于比较两个IP是否在同一个IP段
		/// </summary>
		public class HostSubEqualityCompare : IEqualityComparer<Host>
		{
			/// <summary>
			/// 单件模式对象
			/// </summary>
			public static HostSubEqualityCompare StaticObj { get; set; }

			/// <summary>
			/// 静态构造函数
			/// </summary>
			static HostSubEqualityCompare()
			{
				StaticObj = new HostSubEqualityCompare();
			}

			#region IEqualityComparer<HostSub> 成员

			/// <summary>
			/// 是否相等？
			/// </summary>
			/// <param name="x">对象1</param>
			/// <param name="y">对象2</param>
			/// <returns>是否相等</returns>
			public bool Equals(Host x, Host y)
			{
				return x.HostSub.IPHeader == y.HostSub.IPHeader;
			}

			/// <summary>
			/// 获得HashCode
			/// </summary>
			/// <param name="obj"></param>
			/// <returns></returns>
			public int GetHashCode(Host obj)
			{
				return obj.GetHashCode();
			}

			#endregion
		}

		/// <summary>
		/// 比较两个IP地址是否是同一个IP段
		/// </summary>
		/// <param name="addr1">要比较的IP地址1</param>
		/// <param name="addr2">要比较的IP地址2</param>
		/// <returns>true为相同,false为不同</returns>
		public static bool IsSameIPSectionAS(this IPAddress addr1, IPAddress addr2)
		{
			byte[] a1 = addr1.GetAddressBytes();
			byte[] a2 = addr2.GetAddressBytes();

			for (int i = 0; i < a1.Length - 1; i++)
			{
				if (a1[i] != a2[i]) return false;
			}

			return true;
		}

		/// <summary>
		/// 比较两个IP地址是否是同一个IP
		/// </summary>
		/// <param name="addr1">要比较的IP地址1</param>
		/// <param name="addr2">要比较的IP地址2</param>
		/// <returns>true为相同,false为不同</returns>
		public static bool IsSameIPAs(this IPAddress addr1, IPAddress addr2)
		{
			byte[] a1 = addr1.GetAddressBytes();
			byte[] a2 = addr2.GetAddressBytes();

			for (int i = 0; i < a1.Length; i++)
			{
				if (a1[i] != a2[i]) return false;
			}

			return true;
		}


		///// <summary>
		///// TCP传输文件的信息
		///// </summary>
		///// <remarks></remarks>
		//public class ConnectInfo
		//{
		//    /// <summary>
		//    ///
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public int sd;

		//    /// <summary>
		//    /// 远程地址
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public int Addr;

		//    /// <summary>
		//    /// 端口
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public short Port;

		//    /// <summary>
		//    /// 是不是服务器
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public bool IsServer;

		//    /// <summary>
		//    /// 是否已经完成
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public bool HasComplete;

		//    /// <summary>
		//    /// 开始传输的时刻
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public long startTick;
		//    /// <summary>
		//    /// 当前传输的时刻
		//    /// </summary>
		//    /// <remarks></remarks>
		//    public long lastTick;
		//}


		/// <summary>
		/// 编码IP
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="calcAll"></param>
		/// <returns><see cref="T:System.Int32"/></returns>
		/// <remarks></remarks>
		public static uint IPGen(IPAddress ip, bool calcAll)
		{
			byte[] tip = ip.GetAddressBytes();

			if (calcAll)
			{
				return (((uint)(tip[0])) << 24) + (((uint)(tip[1])) << 16) + (((uint)(tip[2])) << 8) + ((uint)(tip[3]));
			}
			else
			{
				return (((uint)(tip[0])) << 24) + (((uint)(tip[1])) << 16) + (((uint)(tip[2])) << 8);
			}
		}
		/// <summary>
		/// 反编码IP
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static string IpDisGen(uint ip)
		{
			uint ip1 = (ip & 0xFF000000) >> 24;
			uint ip2 = (ip & 0x00FF0000) >> 16;
			uint ip3 = (ip & 0x0000FF00) >> 8;
			uint ip4 = (ip & 0x000000FF);

			return string.Format("{0}.{1}.{2}.{3}", ip1, ip2, ip3, ip4 == 0 ? "*" : ip4.ToString());
		}
		/// <summary>
		/// 判断两个IP是否属于相同的IP段
		/// </summary>
		/// <param name="ip1"></param>
		/// <param name="ip2"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsSameIPBlock(uint ip1, uint ip2)
		{
			return ((ip1 & 0xFFFFFF00) == (ip2 & 0xFFFFFF00));
		}

		/// <summary>
		/// 判断一个IP地址是否属于IP段中的
		/// </summary>
		/// <param name="ip1">IP地址</param>
		/// <param name="ipd">IP段</param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static bool IsInIPBlock(uint ip1, uint ipd)
		{
			return ((ip1 & 0xFFFFFF00) == ipd);
		}

		/// <summary>
		/// 获得本地IP地址
		/// </summary>
		/// <returns></returns>
		public static IPAddress[] GetLocalAddresses()
		{
			return Dns.GetHostAddresses(Dns.GetHostName());
		}

		/// <summary>
		/// 编码IP(IP地址-数字)
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		public static uint IPGen(IPAddress ip)
		{
			return IPGen(ip, true);
		}

		/// <summary>
		/// 尝试将字符串解析为数字
		/// </summary>
		/// <param name="val">字符串</param>
		/// <param name="defValue">默认值</param>
		/// <returns></returns>
		public static int TryParseToInt(this string val, int defValue)
		{
			int temp;
			if (int.TryParse(val, out temp)) return temp;
			return defValue;
		}

		/// <summary>
		/// 尝试将字符串解析为数字
		/// </summary>
		/// <param name="val">字符串</param>
		/// <param name="defValue">默认值</param>
		/// <returns></returns>
		public static ulong TryParseToInt(this string val, ulong defValue)
		{
			ulong temp;
			if (ulong.TryParse(val, out temp)) return temp;
			return defValue;
		}

		/// <summary>
		/// 转换一组字符串为字节
		/// </summary>
		/// <param name="val">字符串，长度必须为2的整数倍</param>
		/// <returns></returns>
		public static byte[] ConvertToBytes(this string val)
		{
			if (val.Length % 2 != 0) val = "0" + val;

			Func<char, byte> getDig = (s) =>
			{
				int byt = 0;
				if (s <= '9' && s >= '0') byt = s - '0';
				else if (s <= 'F' && s >= 'A') byt = s - 'A' + 10;
				else if (s <= 'f' && s >= 'a') byt = s - 'a' + 10;

				return (byte)byt;
			};

			byte[] buf = new byte[val.Length / 2];
			for (int i = 0; i < buf.Length; i++)
			{
				buf[i] = (byte)((getDig(val[i * 2]) << 4) + getDig(val[i * 2 + 1]));
			}

			return buf;
		}

		/// <summary>
		/// 转换一组字符串为字节
		/// </summary>
		/// <param name="val">字符串，长度必须为2的整数倍</param>
		/// <param name="startIndex"></param>
		/// <param name="endIndex"></param>
		/// <returns></returns>
		public static byte[] ConvertToBytes(this string val, int startIndex, int endIndex)
		{
			int len = endIndex - startIndex;

			Func<char, byte> getDig = (s) =>
			{
				int byt = 0;
				if (s <= '9' && s >= '0') byt = s - '0';
				else if (s <= 'F' && s >= 'A') byt = s - 'A' + 10;
				else if (s <= 'f' && s >= 'a') byt = s - 'a' + 10;

				return (byte)byt;
			};

			string substring = val.Substring(startIndex, endIndex - startIndex);
			if (len % 2 != 0) substring = "0" + substring;

			byte[] buf = new byte[len / 2];
			for (int i = (len % 2); i < buf.Length; i++)
			{
				buf[i] = (byte)((getDig(substring[i * 2]) << 4) + getDig(substring[i * 2 + 1]));
			}

			return buf;
		}

		/// <summary>
		/// 将字节数组转换为字符串
		/// </summary>
		/// <param name="buffer">数组</param>
		/// <returns></returns>
		public static string ConvertToString(this byte[] buffer)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			Func<int, char> getDig = (s) =>
			{
				if (s < 10) return (char)(s + '0');
				else return (char)(s - 10 + 'a');
			};

			Array.ForEach(buffer, (b) =>
			{
				sb.Append(getDig(b >> 4));
				sb.Append(getDig(b & 0xf));
			});

			return sb.ToString();
		}

		/// <summary>
		/// 根据任务大小信息创建任务列
		/// </summary>
		/// <param name="sizeList">尺寸大小定义</param>
		/// <returns>创建的列表</returns>
		public static FileTaskItem[] BuildTaskList(this Dictionary<string, long> sizeList)
		{
			return FileTaskItemHelper.BuildTaskList(sizeList);
		}

		/// <summary>
		/// 根据任务列表创建扩展信息
		/// </summary>
		/// <param name="taskList">任务列表</param>
		/// <returns>任务信息</returns>
		public static string BuildTaskMessage(this FileTaskItem[] taskList)
		{
			return FileTaskItemHelper.BuildTaskMessage(taskList);
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDesc(this ulong size)
		{
			double d = 1024 * 0.9;
			if (size < d) return String.Format("{0} 字节", size);
			if (size < 0x400 * d) return String.Format("{0:#0.0} KB", size / 1024.0);
			if (size < 0x100000 * d) return String.Format("{0:#0.0} MB", size / 1048576.0);
			return string.Format("{0:#0.0} GB", size / (0x40000000 * 1.0));
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDesc(this long size)
		{
			double d = 1024 * 0.9;
			if (size < d) return String.Format("{0} 字节", size);
			if (size < 0x400 * d) return String.Format("{0:#0.0} KB", size / 1024.0);
			if (size < 0x100000 * d) return String.Format("{0:#0.0} MB", size / 1048576.0);
			return string.Format("{0:#0.0} GB", size / (0x40000000 * 1.0));
		}

	}

}
