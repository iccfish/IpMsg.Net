using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IPMessagerNet.API;
using System.Drawing;

namespace IPMessagerNet.UI.Controls.Config
{
	class PluginConfigMenuItem : IConfigMenuItem
	{
		public PluginConfigMenuItem(FSLib.IPMessager.Services.ServiceInfo si)
		{
			this._service = si;
		}


		private FSLib.IPMessager.Services.ServiceInfo _service;

		#region IConfigMenuItem 成员

		public int Height
		{
			get { return 30; }
		}

		public Image Image
		{
			get
			{
				return _service.ServiceProvider.PluginIcon;
			}
		}

		public string Name
		{
			get { return _service.ServiceDescription.Name; }
		}

		public ConfigPanelBase UserControl
		{
			get
			{
				return _service.ServiceProvider.ControlPanel as ConfigPanelBase;
			}
		}

		#endregion
	}
}
