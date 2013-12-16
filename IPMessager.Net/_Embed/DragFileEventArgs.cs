using System;

namespace IPMessagerNet._Embed
{
	/// <summary>
	/// 文件拖放事件数据
	/// </summary>
	public class DragFileEventArgs : EventArgs
	{

		public DragFileEventArgs(string[] files)
		{
			Files = files;
		}

		/// <summary>
		/// 包含的文件
		/// </summary>
		public string[] Files { get; private set; }
	}
}
