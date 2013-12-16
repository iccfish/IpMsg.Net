using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostNameNode : HostInfoNodeBase
	{
		public HostNameNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();

			host.NickNameChanged += (s, e) => { SetInfo(); };
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = string.Format("{0} [{1}]", Host.NickName, Host.GroupName);
			SetIcon(HostIcon.HostInfo_Name);
		}


		#endregion

	}
}
