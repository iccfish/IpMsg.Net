using System;
using FSLib;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 文件传输任务的单个文件记录
	/// </summary>
	public class FileTaskItem
	{
		/// <summary>
		/// 索引
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// 文件的完整路径
		/// </summary>
		public string FullPath { get; set; }

		/// <summary>
		/// 是否是文件夹
		/// </summary>
		public bool IsFolder { get; set; }

		/// <summary>
		/// 文件数目
		/// </summary>
		public int FileCount { get; set; }

		/// <summary>
		/// 文件夹数目
		/// </summary>
		public int FolderCount { get; set; }

		/// <summary>
		/// 当前名称
		/// </summary>
		public string CurrentName { get; set; }

		/// <summary>
		/// 当前传输的文件长度
		/// </summary>
		public ulong CurrentFileSize { get; set; }

		/// <summary>
		/// 当前传输完成的长度
		/// </summary>
		public ulong CurrentFileTransfered { get; set; }

		/// <summary>
		/// 以前传输过的长度（断点续传时速度不正确修正）
		/// </summary>
		public ulong FileTransferedAtPast { get; set; }

		/// <summary>
		/// 完成的文件数目
		/// </summary>
		public int FinishedFileCount { get; set; }

		/// <summary>
		/// 完成的文件夹数目
		/// </summary>
		public int FinishedFolderCount { get; set; }

		/// <summary>
		/// 总长度
		/// </summary>
		public ulong TotalSize { get; set; }

		/// <summary>
		/// 完成的长度
		/// </summary>
		public ulong FinishedSize { get; set; }

		/// <summary>
		/// 开始的时刻
		/// </summary>
		public DateTime? StartTime { get; set; }

		/// <summary>
		/// 停止的时间
		/// </summary>
		public DateTime? EndTime { get; set; }

		private FileTaskItemState _state;
		/// <summary>
		/// 任务状态
		/// </summary>
		public FileTaskItemState State
		{
			get
			{
				return _state;
			}
			internal set
			{
				_state = value;
				if (_state == FileTaskItemState.Processing) StartTime = DateTime.Now;
				if (_state == FileTaskItemState.Finished) EndTime = DateTime.Now;
			}
		}

		/// <summary>
		/// 所属的任务信息
		/// </summary>
		public FileTaskInfo TaskInfo { get; set; }

		string _name;

		/// <summary>
		/// 任务名称
		/// </summary>
		public string Name
		{
			get
			{
				if (string.IsNullOrEmpty(_name))
					return System.IO.Path.GetFileName(FullPath);
				else return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// 是否处于等待取消的状态
		/// </summary>
		public bool CancelPadding { get; set; }

		/// <summary>
		/// 获得任务相关信息
		/// </summary>
		/// <param name="ElapsedTime">使用的时间</param>
		/// <param name="EstimateTime">估计的剩余时间</param>
		/// <param name="AverageSpeed">平均速度</param>
		/// <param name="percentage">进度</param>
		public void GetStateInfo(out TimeSpan ElapsedTime, out TimeSpan EstimateTime, out double AverageSpeed, out int percentage)
		{
			ElapsedTime = TimeSpan.Zero;
			EstimateTime = TimeSpan.Zero;
			AverageSpeed = 0.0;
			percentage = 0;

			if (StartTime == null) return;

			//使用的时间
			ElapsedTime = (EndTime ?? DateTime.Now) - StartTime.Value;
			//平均速度
			if (ElapsedTime.Ticks > 0 && FinishedSize > 0)
			{
				AverageSpeed = (FinishedSize - FileTransferedAtPast) / ElapsedTime.TotalSeconds;
			}
			//估计剩余时间
			if (State == FileTaskItemState.Processing && AverageSpeed > 0)
			{
				if (TotalSize > 0)
				{
					EstimateTime = new TimeSpan((long)((TotalSize - FinishedSize) * (ulong)ElapsedTime.Ticks / FinishedSize));
				}
				else if (CurrentFileSize > 0)
				{
					EstimateTime = new TimeSpan(0, 0, (int)((CurrentFileSize - CurrentFileTransfered) / AverageSpeed));
				}
			}
			else
			{
				EstimateTime = TimeSpan.Zero;
			}

			//估计进度
			if (State == FileTaskItemState.Processing)
			{
				if (TotalSize > 0)
				{
					percentage = (int)Math.Floor(FinishedSize * 100.0 / TotalSize);
				}
				else if (CurrentFileSize > 0)
				{
					percentage = (int)Math.Floor(CurrentFileTransfered * 100.0 / CurrentFileSize);
				}
				else if (FileCount > 0)
				{
					percentage = (int)Math.Floor((FinishedFileCount + FinishedFolderCount) * 100.0 / (FileCount + FolderCount));
				}
			}
			else percentage = State == FileTaskItemState.Initializing || State == FileTaskItemState.Scheduled || State == FileTaskItemState.Failure || State == FileTaskItemState.Canceled || State == FileTaskItemState.Canceling ? 0 : 100;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("[{0}] {1} {2}", Index.ToString("00"), Name, IsFolder ? "[文件夹]" : TotalSize.ToSizeDesc());
		}

		/// <summary>
		/// 打开数据读取流
		/// </summary>
		/// <returns></returns>
		public System.IO.Stream OpenReadStream()
		{
			System.Diagnostics.Trace.Assert(this.TaskInfo.Direction == FileTransferDirection.Send, "Current task not a send task");

			return null;
		}

		/// <summary>
		/// 打开数据写入流
		/// </summary>
		/// <returns></returns>
		public System.IO.Stream OpenWriteStream()
		{
			System.Diagnostics.Trace.Assert(this.TaskInfo.Direction == FileTransferDirection.Receive, "Current task not a receive task");

			return null;
		}
	}
}
