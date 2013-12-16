using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	public class HostNode : HostInfoNodeBase
	{
		public HostNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = Core.HostInfoManager.GetHostDisyplayName(Host);
			SetIcon(Host.IsInAbsenceMode ? HostIcon.HostStatus_Absence : HostIcon.HostStatus_Online);


			this.Nodes.AddRange(new TreeNode[] {
				new HostNameNode(Host),
				new HostStatusNode(Host),
				new HostAddressNode(Host),
				new HostFileTransferNode(Host),
				new HostEncryptNode(Host),
				new HostShareNode(Host),
				new HostVersionNode(Host)

			});

			Host.AbsenceModeChanged += (s, e) => { SetIcon(Host.IsInAbsenceMode ? HostIcon.HostStatus_Absence : HostIcon.HostStatus_Online); };
			Host.NickNameChanged += (s, e) =>
			{
				this.Text = Core.HostInfoManager.GetHostDisyplayName(Host);
			};
		}


		#endregion

	}
}
