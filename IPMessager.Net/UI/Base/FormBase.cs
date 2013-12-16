using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Base
{
	public class FormBase : FunctionalForm
	{

		public static ImageList ButtonImages { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		public FormBase()
		{
			InitializeButtonImage();

			this.Load += (s, e) => SetButtonImage(this);
		}

		/// <summary>
		/// 初始化按钮图像
		/// </summary>
		private static void InitializeButtonImage()
		{
			if (ButtonImages == null)
			{
				System.Drawing.Image img = null;
				if (Env.ClientConfig == null)
				{
					//开发环境，硬编码
					img = Properties.Resources.ButtonImage;
				}
				else
				{
					//运行环境
					img = Core.ProfileManager.GetThemePicture("Toolbar", "ButtonImage");
				}
				if (img != null)
				{
					ButtonImages = new ImageList()
					{
						ColorDepth = ColorDepth.Depth32Bit,
						ImageSize = new System.Drawing.Size(16, 16)
					};
					int temp;
					ButtonImages.Images.AddRange(ImageHelper.SplitImage(img, 16, 16, out temp).ToArray());
				}
			}
		}

		/// <summary>
		/// 设置按钮的图标
		/// </summary>
		static void SetButtonImage(Control ctl)
		{
			foreach (Control c in ctl.Controls)
			{
				if (c is Button)
				{
					Button btn = c as Button;
					if (btn.ImageIndex > -1 && btn.ImageIndex < ButtonImages.Images.Count)
					{
						btn.ImageList = ButtonImages;
						btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
						btn.Padding = new Padding(5, 2, 5, 2);
					}
				}
				else if (c.Controls != null && c.Controls.Count > 0) SetButtonImage(c);
			}
		}
	}
}
