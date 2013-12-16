using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet.UI.EditorControls;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class AudioConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public AudioConfigPanel()
		{
			InitializeComponent();
		}

		private void AudioConfigPanel_Load(object sender, EventArgs e)
		{
			foreach (var item in gbEvents.Controls)
			{
				(item as CheckBoxEditor).DataInstance = Env.ClientConfig.Sound;
			}
		}
	}
}
