using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMessagerNet.API;
using System.Drawing;

namespace IPMessagerNet.UI.Controls.Config
{
	class ThemeConfigMenuItem : IConfigMenuItem
	{
		#region IConfigMenuItem Members

		public int Height
		{
			get { return 30; }
		}

		Image _image;

		public Image Image
		{
			get
			{
				if (_image == null) _image = Core.ProfileManager.GetThemePicture("16px_icons", "config_themes");

				return _image;
			}
		}

		public string Name
		{
			get { return "界面主题"; }
		}

		ConfigPanelBase _userControl;

		public ConfigPanelBase UserControl
		{
			get
			{
				if (_userControl == null) _userControl = new ThemeConfigPanel();
				return _userControl;
			}
		}

		#endregion
	}
}
