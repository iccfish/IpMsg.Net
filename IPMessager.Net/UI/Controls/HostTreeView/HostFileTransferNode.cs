using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostFileTransferNode : HostInfoNodeBase
	{
		public HostFileTransferNode(FSLib.IPMessager.Entity.Host host)
			: base(host)
		{
			SetInfo();
		}

		#region 界面响应

		void SetInfo()
		{
			this.Text = Host.SupportFileTransport ? "支持文件发送" : "不支持文件传输";
			SetIcon(Host.SupportFileTransport ? HostIcon.HostInfo_File_Ok : HostIcon.HostInfo_File_Disabled);
		}


		#endregion

	}
}
