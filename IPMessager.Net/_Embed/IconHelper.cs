using System;
using System.Runtime.InteropServices;

namespace IPMessagerNet._Embed
{
	public class IconHelper
	{
		#region API Call

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		const uint SHGFI_ICON = 0x100;
		const uint SHGFI_LARGEICON = 0x0; // 'Large icon
		const uint SHGFI_SMALLICON = 0x1; // 'Small icon

		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

		[DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern int DestroyIcon(IntPtr hIcon);


		#endregion

		/// <summary>
		/// 从指定的文件中加载图标
		/// </summary>
		/// <param name="path">图标文件路径</param>
		/// <returns></returns>
		public static System.Drawing.Icon LoadIcon(string path)
		{
			if (!System.IO.File.Exists(path)) return null;

			SHFILEINFO s = new SHFILEINFO();
			SHGetFileInfo(path, 0, ref s, (uint)Marshal.SizeOf(s), SHGFI_ICON);
			IntPtr p = s.hIcon;
			if (p == IntPtr.Zero) return null;
			else
			{
				System.Drawing.Icon ico = System.Drawing.Icon.FromHandle(p).Clone() as System.Drawing.Icon;
				DestroyIcon(p);

				return ico;
			}
		}
	}
}
