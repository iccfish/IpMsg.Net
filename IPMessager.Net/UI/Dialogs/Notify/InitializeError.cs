using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet.Core;

namespace IPMessagerNet.UI.Dialogs.Notify
{
	partial class InitializeError : Base.DialogBase
	{
		public InitializeError()
		{
			InitializeComponent();

			pictureBox1.Image = Core.ProfileManager.GetThemePicture_Alert();

			this.Shown += (s, e) => { btnOk.Focus(); };
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
