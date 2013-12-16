using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using IPMessagerNet.API;

namespace IPMessagerNet.UI.Controls.Config
{
	class HostConfigMenuItem : IConfigMenuItem
	{

		#region IConfigMenuItem 成员

		public int Height
		{
			get { return 30; }
		}

		Image _image;

		public Image Image
		{
			get {
				if (_image == null) _image = Core.ProfileManager.GetThemePicture("16px_icons", "config_hostlist");

				return _image;
			}
		}

		public string Name
		{
			get { return "主机列表"; }
		}

		ConfigPanelBase _userControl;

		public ConfigPanelBase UserControl
		{
			get
			{
				if (_userControl == null) _userControl = new HostConfigPanel();
				return _userControl;
			}
		}

		#endregion
	}
}
