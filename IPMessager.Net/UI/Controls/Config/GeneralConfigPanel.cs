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
	public partial class GeneralConfigPanel : API.ConfigPanelBase
	{
		public GeneralConfigPanel()
		{
			InitializeComponent();
		}

		private void GeneralConfigPanel_Load(object sender, EventArgs e)
		{
			txtNickName.DataInstance = txtGroupName.DataInstance = Env.IPMClient.Config;
		}
	}
}
