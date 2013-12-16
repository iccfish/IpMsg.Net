using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace IPMessagerNet._Embed
{
	public class WebBrowserEx : WebBrowser
	{
		private const int WM_DROPFILES = 0x233;

		[DllImport("shell32.dll")]
		private static extern uint DragQueryFile(
			IntPtr hDrop,
			uint iFile,
			StringBuilder lpszFile,
			uint cch);

		[DllImport("shell32.dll")]
		private static extern void DragAcceptFiles(IntPtr hWnd, bool fAccept);

		public WebBrowserEx()
			: base()
		{
		}

		/// <summary>
		/// Handle创建时触发
		/// </summary>
		/// <param name="e"></param>
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!DesignMode)
			{
				DragAcceptFiles(Handle, _enableFileDrag);
			}
		}

		#region 文件拖放

		private bool _enableFileDrag;
		/// <summary>
		/// 是否允许文件拖放
		/// </summary>
		public bool EnableFileDrag
		{
			get
			{
				return _enableFileDrag;
			}
			set
			{
				_enableFileDrag = value;
				if (this.Handle != IntPtr.Zero && !DesignMode)
				{
					DragAcceptFiles(this.Handle, value);
					if (value) base.AllowWebBrowserDrop = false;
				}
			}
		}

		/// <summary>
		/// 文件拖放事件
		/// </summary>
		public event DragFileEventHandler DragFile;

		/// <summary>
		/// 重载事件
		/// </summary>
		/// <param name="m">消息</param>
		protected override void WndProc(ref Message m)
		{
			StringBuilder sb = new StringBuilder(1024);
			if (m.Msg == WM_DROPFILES && _enableFileDrag)
			{
				uint count = DragQueryFile(m.WParam, 0xffffffff, null, 0);
				string[] files = new string[count];
				for (uint i = 0; i < count; i++)
				{
					DragQueryFile(m.WParam, i, sb, 1024);
					files[i] = sb.ToString();
				}
				OnDragFile(new DragFileEventArgs(files));
				return;
			}

			base.WndProc(ref m);
		}


		/// <summary>
		/// 引发文件拖放事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnDragFile(DragFileEventArgs e)
		{
			if (DragFile != null)
			{
				DragFile(this, e);
			}
		}


		#endregion
	}
}