using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace IPMessagerNet._Embed
{
	public class PerPixelAlphaForm : Form
	{
		class Win32
		{
			public enum Bool
			{
				False = 0,
				True
			};


			[StructLayout(LayoutKind.Sequential)]
			public struct Point
			{
				public Int32 x;
				public Int32 y;

				public Point(Int32 x, Int32 y) { this.x = x; this.y = y; }
			}


			[StructLayout(LayoutKind.Sequential)]
			public struct Size
			{
				public Int32 cx;
				public Int32 cy;

				public Size(Int32 cx, Int32 cy) { this.cx = cx; this.cy = cy; }
			}


			[StructLayout(LayoutKind.Sequential, Pack = 1)]
			struct ARGB
			{
				public byte Blue;
				public byte Green;
				public byte Red;
				public byte Alpha;

				/// <summary>
				/// 创建 ARGB 的新实例
				/// </summary>
				/// <param name="blue"></param>
				/// <param name="green"></param>
				/// <param name="red"></param>
				/// <param name="alpha"></param>
				public ARGB(byte blue, byte green, byte red, byte alpha)
				{
					Blue = blue;
					Green = green;
					Red = red;
					Alpha = alpha;
				}
			}


			[StructLayout(LayoutKind.Sequential, Pack = 1)]
			public struct BLENDFUNCTION
			{
				public byte BlendOp;
				public byte BlendFlags;
				public byte SourceConstantAlpha;
				public byte AlphaFormat;

				/// <summary>
				/// 创建 BLENDFUNCTION 的新实例
				/// </summary>
				/// <param name="blendOp"></param>
				/// <param name="blendFlags"></param>
				/// <param name="sourceConstantAlpha"></param>
				/// <param name="alphaFormat"></param>
				public BLENDFUNCTION(byte blendOp, byte blendFlags, byte sourceConstantAlpha, byte alphaFormat)
				{
					BlendOp = blendOp;
					BlendFlags = blendFlags;
					SourceConstantAlpha = sourceConstantAlpha;
					AlphaFormat = alphaFormat;
				}
			}


			public const Int32 ULW_COLORKEY = 0x00000001;
			public const Int32 ULW_ALPHA = 0x00000002;
			public const Int32 ULW_OPAQUE = 0x00000004;

			public const byte AC_SRC_OVER = 0x00;
			public const byte AC_SRC_ALPHA = 0x01;


			[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

			[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern IntPtr GetDC(IntPtr hWnd);

			[DllImport("user32.dll", ExactSpelling = true)]
			public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

			[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

			[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern Bool DeleteDC(IntPtr hdc);

			[DllImport("gdi32.dll", ExactSpelling = true)]
			public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

			[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
			public static extern Bool DeleteObject(IntPtr hObject);
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public PerPixelAlphaForm()
		{
			// This form should not have a border or else Windows will clip it.
			FormBorderStyle = FormBorderStyle.None;

			StartOpacity = 0;
			EndOpacity = 245;
			OpacityStep = 25;
			TimerInterval = 10;
			StartPosition = FormStartPosition.CenterParent;
			ShowInTaskbar = false;

			t.Tick += t_Tick;
		}

		/// <summary>
		/// 当前窗体状态
		/// </summary>
		int animationState;

		/// <summary>
		/// 定时器，用于刷新界面
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void t_Tick(object sender, EventArgs e)
		{
			if (animationState == 0)
			{
				if (CurrentOpacity >= EndOpacity - OpacityStep)
				{
					CurrentOpacity = EndOpacity;
					animationState = 1;
					t.Stop();

					if (FormFadeIn != null) FormFadeIn(this, new EventArgs());
				}
				else
				{
					CurrentOpacity += OpacityStep;
				}
			}
			else if (animationState == 2)
			{
				if (CurrentOpacity < OpacityStep)
				{
					animationState = 3;
					CurrentOpacity = 0;
					if (FormFadeOut != null) FormFadeOut(this, new EventArgs());

					t.Stop();

					this.Close();
				}
				else
				{
					CurrentOpacity -= OpacityStep;
				}
			}

			SetBitmap(_formBitmap, CurrentOpacity);
			if (FormRefreshed != null) FormRefreshed(this, new EventArgs());
		}

		/// <summary>
		/// 重载，阻止关闭窗口
		/// </summary>
		/// <param name="e"></param>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (animationState == 1)
			{
				animationState = 2;
				e.Cancel = true;
				t.Start();
				return;
			}
			else if (animationState == 2)
			{
				e.Cancel = true;
				t.Start();
				return;
			}

			base.OnFormClosing(e);
		}

		Timer t = new Timer();

		#region 事件

		/// <summary>
		/// 窗体显示完成
		/// </summary>
		public event EventHandler FormFadeIn;

		/// <summary>
		/// 窗体隐藏完成
		/// </summary>
		public event EventHandler FormFadeOut;


		/// <summary>
		/// 窗体已刷新
		/// </summary>
		public event EventHandler FormRefreshed;


		#endregion

		#region 属性

		private byte _startOpacity;

		/// <summary>
		/// 起始透明度
		/// </summary>
		[Description("起始透明度"), DisplayName("起始透明度")]
		public byte StartOpacity
		{
			get
			{
				return _startOpacity;
			}
			set
			{
				_startOpacity = value;
				CurrentOpacity = StartOpacity;
			}
		}

		/// <summary>
		/// 最终透明度
		/// </summary>
		[Description("最终透明度"), DisplayName("最终透明度")]
		public byte EndOpacity { get; set; }

		private byte _currentOpacity;
		/// <summary>
		/// 当前透明度
		/// </summary>
		[Browsable(false)]
		public byte CurrentOpacity
		{
			get
			{
				return _currentOpacity;
			}
			set
			{
				_currentOpacity = value;
			}
		}

		/// <summary>
		/// 刷新间隔
		/// </summary>
		[Description("刷新间隔"), DisplayName("刷新间隔")]
		public int TimerInterval
		{
			get
			{
				return t.Interval;
			}
			set
			{
				t.Stop();
				t.Interval = value;
			}
		}

		/// <summary>
		/// 是否允许点击拖动窗口
		/// </summary>
		[Description("是否允许点击拖动窗口"), DisplayName("是否允许点击拖动窗口")]
		public bool EnableDrag { get; set; }

		/// <summary>
		/// 透明度步进值
		/// </summary>
		[Description("透明度步进值"), DisplayName("透明度步进值")]
		public byte OpacityStep { get; set; }


		private Bitmap _formBitmap;

		/// <summary>
		/// 显示使用的图像
		/// </summary>
		public Bitmap FormBitmap
		{
			get
			{
				return _formBitmap;
			}
			set
			{
				t.Stop();
				_formBitmap = value;
				SetBitmap(value, CurrentOpacity);
				if (value != null) t.Start();
			}
		}

		#endregion


		/// <para>Changes the current bitmap with a custom opacity level.  Here is where all happens!</para>
		public void SetBitmap(Bitmap bitmap, byte opacity)
		{
			if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
				throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");

			// The ideia of this is very simple,
			// 1. Create a compatible DC with screen;
			// 2. Select the bitmap with 32bpp with alpha-channel in the compatible DC;
			// 3. Call the UpdateLayeredWindow.

			Bitmap bmp = null;
			if (this.Controls.Count == 0) bmp = bitmap;
			else
			{
				bmp = new Bitmap(bitmap);
				foreach (Control item in this.Controls)
				{
					if (item.Visible) item.DrawToBitmap(bmp, item.Bounds);
				}
			}


			IntPtr screenDc = Win32.GetDC(IntPtr.Zero);
			IntPtr memDc = Win32.CreateCompatibleDC(screenDc);
			IntPtr hBitmap = IntPtr.Zero;
			IntPtr oldBitmap = IntPtr.Zero;

			try
			{
				hBitmap = bmp.GetHbitmap(Color.FromArgb(0));  // grab a GDI handle from this GDI+ bitmap
				oldBitmap = Win32.SelectObject(memDc, hBitmap);

				Win32.Size size = new Win32.Size(bitmap.Width, bitmap.Height);
				Win32.Point pointSource = new Win32.Point(0, 0);
				Win32.Point topPos = new Win32.Point(Left, Top);
				Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
				blend.BlendOp = Win32.AC_SRC_OVER;
				blend.BlendFlags = 0;
				blend.SourceConstantAlpha = opacity;
				blend.AlphaFormat = Win32.AC_SRC_ALPHA;

				Win32.UpdateLayeredWindow(Handle, screenDc, ref topPos, ref size, memDc, ref pointSource, 0, ref blend, Win32.ULW_ALPHA);
			}
			finally
			{
				Win32.ReleaseDC(IntPtr.Zero, screenDc);
				if (hBitmap != IntPtr.Zero)
				{
					Win32.SelectObject(memDc, oldBitmap);
					Win32.DeleteObject(hBitmap);
				}
				Win32.DeleteDC(memDc);
			}
		}


		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x00080000; // This form has to have the WS_EX_LAYERED extended style
				return cp;
			}
		}

		protected override void WndProc(ref Message m)
		{
			if (EnableDrag)
			{
				if (m.Msg == 0x0084 /*WM_NCHITTEST*/)
				{
					m.Result = (IntPtr)2;	// HTCLIENT
					return;
				}
			}
			base.WndProc(ref m);

		}
	}
}
