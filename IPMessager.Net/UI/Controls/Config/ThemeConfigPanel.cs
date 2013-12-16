using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class ThemeConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public ThemeConfigPanel()
		{
			InitializeComponent();

			this.Load += new EventHandler(ThemeConfigPanel_Load);
			this.cbTheme.SelectedIndexChanged += new EventHandler(cbTheme_SelectedIndexChanged);
		}

		void cbTheme_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbTheme.SelectedIndex == -1) return;

			string infoName = System.IO.Path.Combine(Core.ProfileManager.GetThemeFolderRoot(), string.Format("{0}{1}{2}", cbTheme.SelectedItem.ToString(), System.IO.Path.DirectorySeparatorChar, "SkinInfo.txt"));
			if (System.IO.File.Exists(infoName)) this.txtNote.Text = System.IO.File.ReadAllText(infoName);

			Image previewImg = Core.ProfileManager.GetThemePicture("", "preview.png");
			if (previewImg != null) pbPreview.Image = previewImg;
			else pbPreview.Image = null;

			Env.ClientConfig.Themes = cbTheme.SelectedItem.ToString();
		}

		void ThemeConfigPanel_Load(object sender, EventArgs e)
		{
			string[] themes = System.IO.Directory.GetDirectories(Core.ProfileManager.GetThemeFolderRoot());
			Array.ForEach(themes, s =>
			{
				if (!System.IO.File.Exists(System.IO.Path.Combine(s, "SkinInfo.txt"))) return;

				string name = System.IO.Path.GetFileName(s);
				int index = cbTheme.Items.Add(name);
				if (string.Compare(name, Env.ClientConfig.Themes) == 0) cbTheme.SelectedIndex = index;
			});
		}
	}
}
