using System;
using System.Collections.Generic;

namespace IPMessagerNet._Embed
{
	class ImageHelper
	{
		/// <summary>
		/// 从文件加载图像(不会锁定文件)
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns></returns>
		public static System.Drawing.Image LoadFromFile(string filepath)
		{
			System.Drawing.Image img = System.Drawing.Image.FromFile(filepath);
			System.Drawing.Image bmp = new System.Drawing.Bitmap(img);
			img.Dispose();

			return bmp;
		}

		/// <summary>
		/// 按照指定尺寸分割图像
		/// </summary>
		/// <param name="filePath">图像的路径</param>
		/// <param name="sizeW">每个图像的宽度</param>
		/// <param name="sizeH">每个图像的高度</param>
		/// <param name="RowCount">行数</param>
		/// <returns></returns>
		public static List<System.Drawing.Image> SplitImage(string filePath, int sizeW, int sizeH, out int RowCount)
		{
			return SplitImage(LoadFromFile(filePath), sizeW, sizeH, out RowCount);
		}

		/// <summary>
		/// 按照指定尺寸分割图像
		/// </summary>
		/// <param name="img">要分割的图像</param>
		/// <param name="sizeW">每个图像的宽度</param>
		/// <param name="sizeH">每个图像的高度</param>
		/// <param name="RowCount">行数</param>
		/// <returns></returns>
		public static List<System.Drawing.Image> SplitImage(System.Drawing.Image img, int sizeW, int sizeH, out int RowCount)
		{
			if (img == null) throw new ArgumentNullException("img");
			else if (img.Width < sizeW) throw new ArgumentOutOfRangeException("sizeW");
			else if (img.Height < sizeH) throw new ArgumentOutOfRangeException("sizeH");

			List<System.Drawing.Image> imglist = new List<System.Drawing.Image>();

			int startIndex = 0;
			int startY = 0;
			RowCount = 0;

			while (startY < img.Height)
			{
				if (sizeH + startY > img.Height) continue;

				while (startIndex < img.Width)
				{
					if (startIndex + sizeW > img.Width) continue;

					System.Drawing.Image img1 = new System.Drawing.Bitmap(sizeW, sizeH);
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img1);
					g.DrawImage(img, 0F, 0F, new System.Drawing.Rectangle(startIndex, startY, sizeW, sizeH), System.Drawing.GraphicsUnit.Pixel);
					g.Flush();
					g.Dispose();

					imglist.Add(img1);

					startIndex += sizeW;
				}
				RowCount++;
				startY += sizeH;
			}

			return imglist;
		}
	}
}
