using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostVersionNode : HostInfoNodeBase
	{
		public HostVersionNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();
			host.ClientVersionResolved += (s, e) => { SetInfo(); };
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = string.IsNullOrEmpty(Host.ClientVersion) ? "未知" : Host.ClientVersion;
			SetIcon(HostIcon.HostInfo_Version);
		}


		#endregion

	}
}
