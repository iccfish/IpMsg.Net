using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.EditorControls
{
	public class ImageLoader : PictureBox
	{
		/// <summary>
		/// Initializes a new instance of the ImageLoader class.
		/// </summary>
		public ImageLoader()
		{
			this.SizeMode = PictureBoxSizeMode.CenterImage;

			if (Env.ClientConfig == null) this.BorderStyle = BorderStyle.FixedSingle;
			else this.BorderStyle = BorderStyle.None;
		}

		/// <summary>
		/// 边框的样式
		/// </summary>
		public new BorderStyle BorderStyle
		{
			get
			{
				return BorderStyle.None;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		private string _imageLocationDefinition;
		/// <summary>
		/// 图像定义
		/// </summary>
		public string ImageLocationDefinition
		{
			get
			{
				return _imageLocationDefinition;
			}
			set
			{
				_imageLocationDefinition = value;

				if (string.IsNullOrEmpty(value) || Env.ClientConfig == null) this.Image = null;
				else
				{
					string[] arg = value.Split(',');
					this.Image = Core.ProfileManager.GetThemePicture(arg[0], arg[1]);
				}
			}
		}
	}
}
