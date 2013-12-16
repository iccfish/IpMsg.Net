using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class TestingConfigPanel : API.ConfigPanelBase
	{
		public TestingConfigPanel()
		{
			InitializeComponent();
		}

		protected override void InitEditor()
		{
			base.InitEditor();

			warningImg.Image = Core.ProfileManager.GetThemePicture("Signal", "testtip");

			chkForceOldContract.DataInstance = Env.ClientConfig.IPMClientConfig;
			chkForceOldContract.CheckAvailability = s => { if (!s)MessageBox.Show("您选择了启用内置的新协议。如果发现有丢失消息或不稳定的错误时，请将错误信息和环境报告给作者帮助我们。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information); return true; };
			chkForceOldContract.Enabled = false;	//强行关闭,因为当前根本没测试么.
		}
	}
}
