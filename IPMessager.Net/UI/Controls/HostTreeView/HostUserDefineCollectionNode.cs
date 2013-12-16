using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostUserDefineCollectionNode : HostCollectionBase
	{
		/// <summary>
		/// 组名
		/// </summary>
		public string GroupName
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		public override bool IsHostInThisGroup(FSLib.IPMessager.Entity.Host host)
		{
			//查找组
			return string.Compare(GetHostGroupName(host), this.Text, true) == 0;
		}

		/// <summary>
		/// 获得主机应该在的用户组名
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public static string GetHostGroupName(FSLib.IPMessager.Entity.Host host)
		{
			string key = host.HostSub.Ipv4Address.Address.ToString();
			if (Env.ClientConfig.HostGroupConfig.ContainsKey(key))
			{
				return Env.ClientConfig.HostGroupConfig[key];
			}
			else
				return "未定义分组";
		}
	}
}
