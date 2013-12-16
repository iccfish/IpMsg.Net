using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Entity;


namespace IPMessagerNet.Core
{
	class HostInfoManager
	{
		/// <summary>
		/// 获得主机显示名称
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public static string GetHostDisyplayName(Host host)
		{
			string key = host.HostSub.Ipv4Address.Address.ToString();
			string memo = Env.ClientConfig.HostInfo.HostMemo.ContainsKey(key) ? Env.ClientConfig.HostInfo.HostMemo[key] : string.Empty;
			if (string.IsNullOrEmpty(memo) || Env.ClientConfig.HostInfo.DisplayStyle == IPMessagerNet.Config.HostNameDisplayStyle.NameOnly) return string.Format("{0} ({1})", host.NickName, host.GroupName);

			switch (Env.ClientConfig.HostInfo.DisplayStyle)
			{
				case IPMessagerNet.Config.HostNameDisplayStyle.MemoOnly:
					return memo;
				case IPMessagerNet.Config.HostNameDisplayStyle.MemoBeforeName:
					return string.Format("{2} ({0}/{1})", host.NickName, host.GroupName, memo);
				case IPMessagerNet.Config.HostNameDisplayStyle.NameOnly:
					return string.Format("{0} ({1})", host.NickName, host.GroupName);
				default:
					return string.Format("{0} ({1})", host.NickName, host.GroupName);
			}
		}
		
	}
}
