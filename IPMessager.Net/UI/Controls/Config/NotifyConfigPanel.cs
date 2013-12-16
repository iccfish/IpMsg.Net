using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class NotifyConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public NotifyConfigPanel()
		{
			InitializeComponent();
		}

		private void NotifyConfigPanel_Load(object sender, EventArgs e)
		{
			chkDOTInQuite.DataInstance = Env.ClientConfig.HostInfo;
			chkAutoChange.DataInstance = Env.ClientConfig.ChatConfig;

			switch (Env.HostConfig.HostOnlineTip)
			{
				case IPMessagerNet.Config.HostBallonTip.None:
					rbOTNone.Checked = true;
					break;
				case IPMessagerNet.Config.HostBallonTip.All:
					rbOTAll.Checked = true;
					break;
				case IPMessagerNet.Config.HostBallonTip.Special:
					rbOTSpec.Checked = true;
					break;
			}
			btnOTSpec.Enabled = rbOTSpec.Checked;

			switch (Env.HostConfig.HostOfflineTip)
			{
				case IPMessagerNet.Config.HostBallonTip.None:
					rbFTNone.Checked = true;
					break;
				case IPMessagerNet.Config.HostBallonTip.All:
					rbFTAll.Checked = true;
					break;
				case IPMessagerNet.Config.HostBallonTip.Special:
					rbFTSpec.Checked = true;
					break;
			}
			btnFTSpec.Enabled = rbFTSpec.Checked;

			rbOTNone.CheckedChanged += rbOTNone_CheckedChanged;
			rbOTAll.CheckedChanged += rbOTNone_CheckedChanged;
			rbOTSpec.CheckedChanged += rbOTNone_CheckedChanged;
			rbFTNone.CheckedChanged += rbFTNone_CheckedChanged;
			rbFTAll.CheckedChanged += rbFTNone_CheckedChanged;
			rbFTSpec.CheckedChanged += rbFTNone_CheckedChanged;
		}

		//Handle checkbox checked changed events.
		void rbOTNone_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == rbOTAll) SwitchOT(IPMessagerNet.Config.HostBallonTip.All);
			else if (sender == rbOTNone) SwitchOT(IPMessagerNet.Config.HostBallonTip.None);
			else SwitchOT(IPMessagerNet.Config.HostBallonTip.Special);
		}

		//Handle checkbox checked changed events.
		void rbFTNone_CheckedChanged(object sender, EventArgs e)
		{
			if (sender == rbFTAll) SwitchFT(IPMessagerNet.Config.HostBallonTip.All);
			else if (sender == rbFTNone) SwitchFT(IPMessagerNet.Config.HostBallonTip.None);
			else SwitchFT(IPMessagerNet.Config.HostBallonTip.Special);
		}

		//选择上线提示类型
		void SwitchOT(IPMessagerNet.Config.HostBallonTip type)
		{
			btnOTSpec.Enabled = type == IPMessagerNet.Config.HostBallonTip.Special;
			Env.HostConfig.HostOnlineTip = type;
		}

		//选择下线提示类型
		void SwitchFT(IPMessagerNet.Config.HostBallonTip type)
		{
			btnFTSpec.Enabled = type == IPMessagerNet.Config.HostBallonTip.Special;
			Env.HostConfig.HostOfflineTip = type;
		}

		Dialogs.HostListEditor hle;

		//设置上线提示主机列表
		private void btnOTSpec_Click(object sender, EventArgs e)
		{
			if (hle == null) hle = new IPMessagerNet.UI.Dialogs.HostListEditor();
			hle.SelectedHost = Env.HostConfig.OnlineTip.ToArray();

			if (hle.ShowDialog() == DialogResult.OK)
			{
				if (Env.HostConfig.OnlineTip != null) Env.HostConfig.OnlineTip.Clear();
				else Env.HostConfig.OnlineTip = new List<string>();

				Env.HostConfig.OnlineTip.AddRange(hle.SelectedHost);
				
			}
		}

		//设置下线提示主机列表
		private void btnFTSpec_Click(object sender, EventArgs e)
		{
			if (hle == null) hle = new IPMessagerNet.UI.Dialogs.HostListEditor();
			hle.SelectedHost = Env.HostConfig.OfflineTip.ToArray();

			if (hle.ShowDialog() == DialogResult.OK)
			{
				if (Env.HostConfig.OfflineTip != null) Env.HostConfig.OfflineTip.Clear();
				else Env.HostConfig.OfflineTip = new List<string>();

				Env.HostConfig.OfflineTip.AddRange(hle.SelectedHost);

			}
		}
	}
}
