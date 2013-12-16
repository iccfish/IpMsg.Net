using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.API
{
	public interface IEditorService
	{
		/// <summary>
		/// 请求发送消息
		/// </summary>
		event EventHandler TextMessageSendRequired;

		/// <summary>
		/// 是否可用
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// 是否是HTML编辑器
		/// </summary>
		bool IsHtml { get; }

		/// <summary>
		/// 是否是RTF编辑器
		/// </summary>
		bool IsRTF { get; }

		/// <summary>
		/// 获得或设置文本内容
		/// </summary>
		string Content { get; set; }
	}
}
