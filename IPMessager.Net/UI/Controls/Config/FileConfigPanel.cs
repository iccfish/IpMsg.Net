using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class FileConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public FileConfigPanel()
		{
			InitializeComponent();
		}

		private void FileConfigPanel_Load(object sender, EventArgs e)
		{
			chkFileBpC.Checked = Env.IPMClient.Config.EnableBPContinue;
			numTaskThreadCount.Value = Env.IPMClient.Config.TasksMultiReceiveCount;
			numTaskTimeout.Value = (int)(Env.IPMClient.Config.TaskKeepTime / 60);

			chkFileBpC.CheckedChanged += (s, f) =>
			{
				Env.IPMClient.Config.EnableBPContinue = chkFileBpC.Checked;
			};
			numTaskTimeout.ValueChanged += (s, f) =>
			{
				Env.IPMClient.Config.TaskKeepTime = (int)numTaskTimeout.Value * 60;
			};
			numTaskThreadCount.ValueChanged += (s, f) =>
			{
				Env.IPMClient.Config.TasksMultiReceiveCount = (int)numTaskThreadCount.Value;
			};
		}
	}
}
