using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib;

namespace System
{
	[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
	public static class FSLib_ValueExtract
	{
		readonly static string[] SizeDefinitions = new[] {
		"字节",
		"KB",
		"MB",
		"GB",
		"TB"
		};

		/// <summary>
		/// 控制尺寸显示转换上限
		/// </summary>
		readonly static double SizeLevel = 0x400 * 0.9;

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this double size)
		{
			return ToSizeDescription(size, 2);
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <param name="digits">小数位数</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this double size, int digits)
		{
			var sizeDefine = 0;


			while (sizeDefine < SizeDefinitions.Length && size > SizeLevel)
			{
				size /= 0x400;
				sizeDefine++;
			}


			if (sizeDefine == 0) return size.ToString("#0") + " " + SizeDefinitions[sizeDefine];
			else
			{
				return size.ToString("#0." + string.Empty.PadLeft(digits, '#')) + " " + SizeDefinitions[sizeDefine];
			}
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this ulong size)
		{
			return ((double)size).ToSizeDescription();
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <param name="digits">小数位数</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this ulong size, int digits)
		{
			return ((double)size).ToSizeDescription(digits);
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this long size)
		{
			return ((double)size).ToSizeDescription();
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <param name="digits">小数位数</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this long size, int digits)
		{
			return ((double)size).ToSizeDescription(digits);
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this int size)
		{
			return ((double)size).ToSizeDescription();
		}

		/// <summary>
		/// 转换为尺寸显示方式
		/// </summary>
		/// <param name="size">大小</param>
		/// <param name="digits">小数位数</param>
		/// <returns>尺寸显示方式</returns>
		public static string ToSizeDescription(this int size, int digits)
		{
			return ((double)size).ToSizeDescription(digits);
		}
	}
}
