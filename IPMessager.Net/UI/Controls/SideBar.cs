using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls
{
	class SideBar : TabControl
	{
		/// <summary>
		/// 创建一个新的 SideBar 对象.
		/// </summary>
		public SideBar()
		{
			this.Dock = DockStyle.Fill;
		}
	}
}
