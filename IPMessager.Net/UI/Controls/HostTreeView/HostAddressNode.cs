using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostAddressNode : HostInfoNodeBase
	{
		public HostAddressNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = "IP地址：" + Host.HostSub.Ipv4Address.Address.ToString();

			SetIcon(HostIcon.HostInfo_IP);
		}


		#endregion

	}
}
