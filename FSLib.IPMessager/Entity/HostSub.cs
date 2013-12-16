using System;
using System.Net;

namespace FSLib.IPMessager.Entity
{
    /// <summary>
    /// 主机信息
    /// </summary>
    /// <remarks></remarks>
    public class HostSub
    {

        /// <summary>
        /// 登陆的用户名，长度最长为 MAX_NAMEBUFFER ，默认为 50
        /// </summary>
        /// <remarks></remarks>
		public string UserName { get; set; }

        /// <summary>
        /// 主机名，长度最长为 MAX_NAMEBUFFER ，默认为 50
        /// </summary>
        /// <remarks></remarks>
		public string HostName { get; set; }

		private IPEndPoint _ipv4Address;
		/// <summary>
		/// 主机地址（数值表示方法）
		/// </summary>
		/// <remarks></remarks>
		public IPEndPoint Ipv4Address
		{
			get
			{
				return _ipv4Address;
			}
			set
			{
				_ipv4Address = value;
				if (value != null)
				{
					IPHeader = Helper.IPGen(value.Address, false);
				}
			}
		}

		/// <summary>
		/// IP段
		/// </summary>
		public uint IPHeader { get; private set; }

        /// <summary>
        /// 主机的通信端口
        /// </summary>
        /// <remarks></remarks>
		public int PortNo
		{
			get
			{
				return Ipv4Address.Port;
			}
		}
    }
}
