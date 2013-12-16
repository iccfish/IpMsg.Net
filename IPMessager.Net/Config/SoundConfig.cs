using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Config
{
	/// <summary>
	/// 声音配置
	/// </summary>
	[Serializable]
	public class SoundConfig
	{
		/// <summary>
		/// 允许上线提示
		/// </summary>
		public bool EnableOnlineSound { get; set; }

		/// <summary>
		/// 允许下线提示
		/// </summary>
		public bool EnableOfflineSound { get; set; }

		/// <summary>
		/// 允许新信息声音提示
		/// </summary>
		public bool EnableNewMsgSound { get; set; }

		/// <summary>
		/// 允许收到文件提示
		/// </summary>
		public bool EnableNewFileSound { get; set; }

		/// <summary>
		/// 允许文件传输成功声音提示
		/// </summary>
		public bool EnableFileSuccSound { get; set; }

		/// <summary>
		/// 允许文件传输出错提示
		/// </summary>
		public bool EnableFileErrorSound { get; set; }
	}
}
