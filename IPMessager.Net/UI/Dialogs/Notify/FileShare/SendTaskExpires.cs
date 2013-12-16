using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;

namespace IPMessagerNet.UI.Dialogs.Notify.FileShare
{
	public partial class SendTaskExpires : Form
	{
		public SendTaskExpires()
		{
			InitializeComponent();
		}
		public FileTaskInfo Task { get; set; }

		/// <summary>
		/// 创建一个新的 SendTaskExpires 对象.
		/// </summary>
		public SendTaskExpires(FileTaskInfo task)
		{
			Task = task;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			pbTip.Image = Core.ProfileManager.GetThemePicture_Alert();
			if (Task != null)
			{
				lblDesc.Text = string.Format("您于 {0} 发送至 {1}({2}/{3}) 的文件由于对方长时间未接收已经被系统自动删除，如果需要请重新发送。包含的文件如下："
					, Task.CreateTime, Task.RemoteHost.NickName, Task.RemoteHost.GroupName, Task.RemoteHost.HostSub.Ipv4Address.Address);
				fileList.Items.Clear();
				fileList.Items.AddRange(Task.TaskList.ToArray());
			}
		}
	}
}
