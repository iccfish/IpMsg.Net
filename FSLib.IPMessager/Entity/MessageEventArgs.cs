using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 包事件参数
	/// </summary>
	public class MessageEventArgs : EventArgs
	{
		/// <summary>
		/// 消息对象
		/// </summary>
		public Message Message { get; set; }

		/// <summary>
		/// 发送到的主机
		/// </summary>
		public Entity.Host Host { get; set; }

		/// <summary>
		/// 是否已经处理过了
		/// </summary>
		public bool IsHandled { get; set; }

		/// <summary>
		/// 创建一个新的 PackageEventArgs 对象.
		/// </summary>
		public MessageEventArgs(Message message)
		{
			Message = message;
			IsHandled = false;
			this.Host = message.Host;
		}


		/// <summary>
		/// 创建一个新的 MessageEventArgs 对象.
		/// </summary>
		public MessageEventArgs(Message message, Host host)
		{
			Message = message;
			this.Host = host;
			IsHandled = false;
		}

	}
}
