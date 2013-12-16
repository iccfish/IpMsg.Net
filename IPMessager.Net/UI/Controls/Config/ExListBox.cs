using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IPMessagerNet.API;

namespace IPMessagerNet.UI.Controls.Config
{
	public class ExListBox : ListBox
	{
		/// <summary>
		/// 创建 ExListBox class 的新实例
		/// </summary>
		public ExListBox()
		{
			this.MeasureItem += ExListBox_MeasureItem;
			this.DrawItem += ExListBox_DrawItem;
			this.DrawMode = DrawMode.OwnerDrawVariable;

		}

		void ExListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index >= this.Items.Count) return;

			IConfigMenuItem cfgItem = this.Items[e.Index] as IConfigMenuItem;
			if (cfgItem == null) return;

			e.DrawBackground();
			e.DrawFocusRectangle();

			Graphics g = e.Graphics;

			if (cfgItem.Image != null) g.DrawImage(cfgItem.Image, 3 + e.Bounds.Left, 7 + e.Bounds.Top, 16, 16);
			SolidBrush brush = new SolidBrush(e.ForeColor);
			g.DrawString(cfgItem.Name, new Font("宋体", 12.0F, FontStyle.Regular, GraphicsUnit.Pixel), brush, new PointF(23 + e.Bounds.Left, 9 + e.Bounds.Top));
		}

		void ExListBox_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			if (e.Index >= this.Items.Count) return;

			IConfigMenuItem cfgItem = this.Items[e.Index] as IConfigMenuItem;
			if (cfgItem == null) return;

			e.ItemHeight = cfgItem.Height;
		}
	}
}
