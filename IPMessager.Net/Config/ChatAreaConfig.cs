using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Config
{
	/// <summary>
	/// 聊天区域设置
	/// </summary>
	[Serializable]
	public class ChatAreaConfig
	{
		/// <summary>
		/// 是否允许使用Ctrl+Enter快捷键
		/// </summary>
		public bool EnableCtrlEnterShortKey { get; set; }

		/// <summary>
		/// 是否自动切换到当前的选项页到有新信息的页面
		/// </summary>
		public bool AutoChangeCurrentTabToNew { get; set; }
	}
}
