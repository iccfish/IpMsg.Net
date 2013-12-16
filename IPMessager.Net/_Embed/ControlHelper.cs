using System.Windows.Forms;
using System.Drawing;

namespace IPMessagerNet._Embed
{
	/// <summary>
	/// 控件辅助类
	/// </summary>
	public class ControlHelper
	{
		#region 工具栏、菜单类

		/// <summary>
		/// 自动使用指定的图像序列填充菜单、工具栏等图标
		/// </summary>
		/// <param name="s">目标控件</param>
		/// <param name="img">图像序列</param>
		/// <param name="startIndex">起始的图标索引</param>
		public static void FillMenuButtonImage(object s, Image[] img, ref int startIndex)
		{
			if (startIndex >= img.Length) return;

			if (s is ToolStripButton)
			{
				if (!IsBlankImage(img[startIndex]))
				{
					(s as ToolStripButton).Image = img[startIndex++];
				}
			}
			else if (s is ToolStripMenuItem)
			{
				ToolStripMenuItem _tsd = (s as ToolStripMenuItem);
				if (!IsBlankImage(img[startIndex]))
				{
					_tsd.Image = img[startIndex++];
				}
				else
				{
					startIndex++;
				}
				if (_tsd.DropDownItems != null) { foreach (var i in _tsd.DropDownItems)FillMenuButtonImage(i, img, ref startIndex); }
			}
			else if (s is ToolStripDropDownButton)
			{
				ToolStripDropDownButton _tsb = (s as ToolStripDropDownButton);
				if (!IsBlankImage(img[startIndex]))
				{
					_tsb.Image = img[startIndex++];
				}
				else
				{
					startIndex++;
				}
				foreach (var item in _tsb.DropDownItems) FillMenuButtonImage(item, img, ref startIndex);
			}
			else if (s is ToolStripSplitButton)
			{
				ToolStripSplitButton _tsb = (s as ToolStripSplitButton);
				if (!IsBlankImage(img[startIndex]))
				{
					_tsb.Image = img[startIndex++];
				}
				else
				{
					startIndex++;
				}
				foreach (var item in _tsb.DropDownItems) FillMenuButtonImage(item, img, ref startIndex);
			}
			else if (s is ToolStrip)
			{
				foreach (var item in (s as ToolStrip).Items) FillMenuButtonImage(item, img, ref startIndex);
			}

		}

		/// <summary>
		/// 判断是否是空白的图像
		/// </summary>
		/// <param name="img">图像</param>
		/// <returns></returns>
		static bool IsBlankImage(Image img)
		{
			Color c = (img as Bitmap).GetPixel(0, 0);
			return c.R == 255 && c.G == 0 && c.B == 255;
		}


		#endregion
	}
}
