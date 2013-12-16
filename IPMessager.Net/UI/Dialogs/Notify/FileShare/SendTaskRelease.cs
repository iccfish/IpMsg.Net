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
	public partial class SendTaskRelease : Form
	{
		public SendTaskRelease()
		{
			InitializeComponent();
		}
		public FileTaskInfo Task { get; set; }

		/// <summary>
		/// 创建一个新的 SendTaskExpires 对象.
		/// </summary>
		public SendTaskRelease(FileTaskInfo task)
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
				lblDesc.Text = string.Format("好友 {1}({2}/{3}) 忽略了您于 {0} 发送的文件，如果需要请重新发送。包含的文件如下："
					, Task.CreateTime, Task.RemoteHost.NickName, Task.RemoteHost.GroupName, Task.RemoteHost.HostSub.Ipv4Address.Address);
				fileList.Items.Clear();
				fileList.Items.AddRange(Task.TaskList.ToArray());
			}
		}
	}
}
