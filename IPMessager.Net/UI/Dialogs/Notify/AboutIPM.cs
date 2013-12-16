using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Dialogs.Notify
{
	partial class AboutIPM : PerPixelAlphaForm
	{
		public AboutIPM()
		{
			InitializeComponent();

			this.FormBitmap = Core.ProfileManager.GetThemePicture("Images", "About") as Bitmap;

			this.ShowInTaskbar = false;
			this.TopMost = true;

			this.Click += (s, e) =>
			{
				Close();
			};
		}
	}
}
