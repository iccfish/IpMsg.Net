using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostShareNode : HostInfoNodeBase
	{
		public HostShareNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = Host.HasShare ? "有共享资料" : "没有共享";
			SetIcon(HostIcon.HostInfo_Share_None);
		}


		#endregion
	}
}
