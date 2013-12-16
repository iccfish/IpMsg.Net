using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostStatusNode : HostInfoNodeBase
	{
		public HostStatusNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();

			host.AbsenceModeChanged += (s, e) => { SetInfo(); };
			host.AbsenceMessageChanged += (s, e) => { SetInfo(); };
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = Host.IsInAbsenceMode ? "离开：" + Host.AbsenceMessage : "在线";
			SetIcon(Host.IsInAbsenceMode ? HostIcon.HostStatus_Absence : HostIcon.HostStatus_Online);
		}


		#endregion

	}
}
