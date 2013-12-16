using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Entity;

namespace IPMessagerNet.Utility
{
	public static class Convertor
	{
		/// <summary>
		/// 将初始化的任务项目信息转换为JSON格式
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string ToJsonInfo(this FileTaskItem item)
		{
			System.Text.StringBuilder sb = new StringBuilder();

			sb.Append("{");
			sb.AppendFormat("pkgid:\"{10}\",index:{0},curname:\"{11}\",filename:\"{1}\",filecount:{2},sended:{3},filesize:\"{4}\",sizesended:\"{5}\",percentage:{6},timeused:\"{7}\",timerest:\"{8}\",state:{9},speed:'',path:\"{12}\",isfolder:{13}",
				item.Index, Helper.ConvertJsString(item.Name), item.FileCount + item.FolderCount, item.FinishedFileCount + item.FinishedFolderCount, item.TotalSize.ToSizeDescription(),
				item.FinishedSize.ToSizeDescription(), 0, "--:--:--", "--:--:--", (int)item.State, item.TaskInfo.PackageID, Helper.ConvertJsString(item.CurrentName),
				Helper.ConvertJsString(item.FullPath), item.IsFolder ? 1 : 0
				);

			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// 将初始化的任务项目信息转换为JSON格式
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string ToJsonInfoWithProgress(this FileTaskItem item)
		{
			TimeSpan usedTime, restTime;
			double speed;
			int percentage;

			item.GetStateInfo(out usedTime, out restTime, out speed, out percentage);

			System.Text.StringBuilder sb = new StringBuilder();

			sb.Append("{");
			sb.AppendFormat("pkgid:\"{10}\",index:{0},curname:\"{12}\",filename:\"{1}\",filecount:{2},sended:{3},filesize:\"{4}\",sizesended:\"{5}\",percentage:{6},timeused:\"{7}\",timerest:\"{8}\",state:{9},speed:'{11}/S'",
				item.Index, Helper.ConvertJsString(item.Name), item.FileCount, item.FinishedFileCount, item.TotalSize.ToSizeDescription(),
				item.FinishedSize.ToSizeDescription(), percentage, string.Format("{0:00}:{1:00}:{2:00}", usedTime.Hours, usedTime.Minutes, usedTime.Seconds),
				string.Format("{0:00}:{1:00}:{2:00}", restTime.Hours, restTime.Minutes, restTime.Seconds), (int)item.State, item.TaskInfo.PackageID,
				((ulong)speed).ToSizeDescription(),
				Helper.ConvertJsString(item.CurrentName)
				);
			sb.Append("}");

			return sb.ToString();
		}

		/// <summary>
		/// 将初始化的任务信息转换为JSON格式
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string ToJsonInfo(this FileTaskInfo task)
		{
			System.Text.StringBuilder sb = new StringBuilder();
			sb.AppendLine("{");
			sb.AppendFormat("pkgid:'{0}',host:'{1}',isretry:{2},", task.PackageID, task.RemoteHost.HostSub.Ipv4Address.ToString(), task.IsRetry ? 1 : 0);
			sb.AppendLine("tasks:[");
			for (int i = 0; i < task.TaskList.Count; i++)
			{
				if (i < task.TaskList.Count - 1)
				{
					sb.Append(task.TaskList[i].ToJsonInfo());
					sb.AppendLine(",");
				}
				else
				{
					sb.AppendLine(task.TaskList[i].ToJsonInfo());
				}
			}
			sb.AppendLine("]");
			sb.AppendLine("}");

			return sb.ToString();
		}
	}
}
