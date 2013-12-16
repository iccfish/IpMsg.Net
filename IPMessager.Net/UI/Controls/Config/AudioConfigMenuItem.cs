using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMessagerNet.API;
using System.Drawing;

namespace IPMessagerNet.UI.Controls.Config
{
	class AudioConfigMenuItem : API.IConfigMenuItem
	{
		#region IConfigMenuItem 成员

		public int Height
		{
			get { return 30; }
		}

		Image _image;

		public Image Image
		{
			get
			{
				if (_image == null) _image = Core.ProfileManager.GetThemePicture("16px_icons", "config_audio");

				return _image;
			}
		}

		public string Name
		{
			get { return "声音"; }
		}

		ConfigPanelBase _userControl;

		public ConfigPanelBase UserControl
		{
			get
			{
				if (_userControl == null) _userControl = new AudioConfigPanel();
				return _userControl;
			}
		}

		#endregion
	}
}
