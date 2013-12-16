using System;

namespace FSLib.IPMessager.Core
{
	/// <summary>
	/// 包含收到的文件的数据
	/// </summary>
	public class FileReceivedEventArgs : EventArgs
	{
		public Entity.Host Host { get; private set; }

		public Entity.Message Message { get; private set; }

		public Entity.FileTaskInfo File { get; private set; }

		/// <summary>
		/// 创建 <see cref="FileReceivedEventArgs" /> 的新实例
		/// </summary>
		public FileReceivedEventArgs(Entity.Host host, Entity.Message message, Entity.FileTaskInfo file)
		{
			Host = host;
			Message = message;
			File = file;
		}
	}
}
