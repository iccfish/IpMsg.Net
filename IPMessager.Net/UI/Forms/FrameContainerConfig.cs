using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IPMessagerNet.UI.Forms
{
	[Serializable]
	public class FrameContainerConfig
	{
		/// <summary>
		/// 最小化到托盘
		/// </summary>
		public bool MinimizeToTray { get; set; }

		/// <summary>
		/// 主窗口位置
		/// </summary>
		public Point Location { get; set; }

		/// <summary>
		/// 窗口状态
		/// </summary>
		public int WindowState { get; set; }

		/// <summary>
		/// 窗口尺寸
		/// </summary>
		public Size WindowSize { get; set; }

		/// <summary>
		/// 左侧主机列表宽度
		/// </summary>
		public int HostListWidth { get; set; }

		/// <summary>
		/// 聊天窗口宽度
		/// </summary>
		public int ChatAreaWidth { get; set; }

		/// <summary>
		/// 主机列表窗口浮动
		/// </summary>
		public bool HostListFloat { get; set; }

		/// <summary>
		/// 主机列表浮动窗口是否总置顶
		/// </summary>
		public bool HostListContainerTopMost { get; set; }

		/// <summary>
		/// 主机列表浮动窗口位置
		/// </summary>
		public Point HostListContainerLocation { get; set; }

		/// <summary>
		/// 主机列表浮动窗口大小
		/// </summary>
		public Size HostListContainerSize { get; set; }

		/// <summary>
		/// 主机列表浮动窗口状态
		/// </summary>
		public int HostListContainerWindowState { get; set; }
	}
}
