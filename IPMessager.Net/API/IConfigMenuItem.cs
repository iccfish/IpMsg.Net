using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace IPMessagerNet.API
{
	interface IConfigMenuItem
	{
		int Height { get; }

		Image Image { get; }

		string Name { get; }

		ConfigPanelBase UserControl { get; }
	}
}
