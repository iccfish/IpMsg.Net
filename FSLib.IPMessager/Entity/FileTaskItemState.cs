using System;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 文件传输单个任务的状态
	/// </summary>
	public enum FileTaskItemState : int
	{
		/// <summary>
		/// 队列中
		/// </summary>
		Scheduled = 0,
		/// <summary>
		/// 初始化中
		/// </summary>
		Initializing = 1,
		/// <summary>
		/// 传输中
		/// </summary>
		Processing = 2,
		/// <summary>
		/// 失败
		/// </summary>
		Failure = 3,
		/// <summary>
		/// 传输成功
		/// </summary>
		Finished = 4,
		/// <summary>
		/// 任务取消
		/// </summary>
		Canceled = 5,
		/// <summary>
		/// 正在取消
		/// </summary>
		Canceling = 6
	}
}
