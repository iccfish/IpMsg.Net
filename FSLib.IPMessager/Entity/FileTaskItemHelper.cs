using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 文件任务项目助手
	/// </summary>
	public class FileTaskItemHelper
	{
		/// <summary>
		/// 根据任务大小信息创建任务列
		/// </summary>
		/// <param name="sizeList">尺寸大小定义</param>
		/// <returns>创建的列表</returns>
		public static FileTaskItem[] BuildTaskList(Dictionary<string, long> sizeList)
		{
			FileTaskItem[] list = new FileTaskItem[sizeList.Count];
			int index = 0;

			foreach (var key in sizeList.Keys)
			{
				list[index] = new FileTaskItem()
				{
					CancelPadding = false,
					CurrentFileSize = sizeList[key] <= 0 ? 0ul : (ulong)sizeList[key],
					CurrentFileTransfered = 0,
					CurrentName = System.IO.Path.GetFileName(key),
					EndTime = null,
					FileCount = sizeList[key] <= 0 ? 0 : 1,
					FinishedFileCount = 0,
					FinishedFolderCount = 0,
					FinishedSize = 0ul,
					FolderCount = 0,
					FullPath = key,
					Index = index,
					IsFolder = sizeList[key] <= 0,
					StartTime = null,
					State = FileTaskItemState.Scheduled,
					TotalSize = (ulong)(sizeList[key] < 0 ? -sizeList[key] : sizeList[key])
				};
				index++;
			}

			return list;
		}

		/// <summary>
		/// 根据任务列表创建扩展信息
		/// </summary>
		/// <param name="taskList">任务列表</param>
		/// <returns>任务信息</returns>
		public static string BuildTaskMessage(FileTaskItem[] taskList)
		{
			System.Text.StringBuilder sb = new StringBuilder();

			int index = 0;
			Array.ForEach(taskList, s =>
			{
				if (s.IsFolder)
				{
					sb.AppendFormat("{0}:{1}:{2:x}:{3:x}:{4:x}:{5}", index++, s.CurrentName, s.TotalSize, DateTime.Now.ToFileTime(), (int)Define.Consts.Cmd_FileType_Option.Dir, '\a');
				}
				else
				{
					sb.AppendFormat("{0}:{1}:{2:x}:{3:x}:{4:x}:{5}", index++, s.CurrentName, s.TotalSize, DateTime.Now.ToFileTime(), (int)Define.Consts.Cmd_FileType_Option.Regular, '\a');
				}
			});

			return sb.ToString();
		}

		/// <summary>
		/// 从文本信息里面解析出任务信息
		/// </summary>
		/// <param name="taskMessage"></param>
		/// <returns></returns>
		public static FileTaskInfo DecompileTaskInfo(Host Host, Message msg)
		{
			if (!msg.IsFileAttached || string.IsNullOrEmpty(msg.ExtendMessage)) return null;

			FileTaskInfo task = new FileTaskInfo(FileTransferDirection.Receive, msg.PackageNo, Host);

			string[] f = msg.ExtendMessage.Split('\a');
			for (int i = 0; i < f.Length; i++)
			{
				string[] ef = f[i].Split(':');
				if (ef.Length < 5) continue;

				int index = 0;
				string name = "";
				ulong size = 0ul;
				ulong filetypeAttr;

				if (!int.TryParse(ef[0], out index) || !ulong.TryParse(ef[2], System.Globalization.NumberStyles.AllowHexSpecifier, null, out size) || !ulong.TryParse(ef[4], System.Globalization.NumberStyles.AllowHexSpecifier, null, out filetypeAttr)) continue;
				name = ef[1];

				if (string.IsNullOrEmpty(name)) continue;
				else name = Network.TCPThread.replaceReg.Replace(name, "_");

				task.TaskList.Add(new FileTaskItem()
				{
					TaskInfo = task,
					TotalSize = size,
					CancelPadding = false,
					FileCount = 0,
					FolderCount = 0,
					FinishedFileCount = 0,
					FinishedSize = 0,
					FinishedFolderCount = 0,
					EndTime = null,
					FullPath = "",
					CurrentFileTransfered = 0,
					Index = index,
					IsFolder = (filetypeAttr & (ulong)Define.Consts.Cmd_FileType_Option.Dir) == (ulong)Define.Consts.Cmd_FileType_Option.Dir,
					StartTime = null,
					CurrentName = name,
					CurrentFileSize = (filetypeAttr & (ulong)Define.Consts.Cmd_FileType_Option.Dir) == (ulong)Define.Consts.Cmd_FileType_Option.Dir ? 0ul : size,
					State = FileTaskItemState.Scheduled,
					Name = name
				});
			}

			if (task.TaskList.Count == 0) return null;
			else return task;
		}
	}
}
