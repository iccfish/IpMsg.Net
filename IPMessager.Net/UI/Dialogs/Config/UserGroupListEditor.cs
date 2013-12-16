using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;

namespace IPMessagerNet.UI.Dialogs.Config
{
	public partial class UserGroupListEditor : Base.DialogBase
	{
		public UserGroupListEditor()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 关联的组名
		/// </summary>
		public string GroupName { get; set; }

		/// <summary>
		/// 主机数目
		/// </summary>
		public int HostCount { get; set; }

		private void UserGroupListEditor_Load(object sender, EventArgs e)
		{
			string[] hostIPList = Env.ClientConfig.HostGroupConfig.Where(m => m.Value == GroupName).Select<KeyValuePair<string, string>, string>(m => m.Key).ToArray();

			ListViewItem[] lvtlist = new ListViewItem[hostIPList.Length];
			int index = 0;
			Array.ForEach(hostIPList, s =>
			{
				Host h = Env.IPMClient.Commander.LivedHost.GetHost(s);
				lvtlist[index++] = new ListViewItem(new string[] { s, h == null ? "离线" : h.NickName + "/" + h.GroupName, Env.HostConfig.HostMemo.ContainsKey(s) ? Env.HostConfig.HostMemo[s] : "-" });
			});
			this.hlist.Items.AddRange(lvtlist);

			this.btnClose.Click += btnClose_Click;
			this.FormClosing += UserGroupListEditor_FormClosing;
			btnClear.Click += (s, f) => hlist.Items.Clear();
			btnRemove.Click += (s, f) =>
			{
				if (hlist.SelectedIndices.Count > 0)
				{
					ListViewItem[] it=new ListViewItem[hlist.SelectedIndices.Count];
					int i = 0;
					foreach (ListViewItem idx in hlist.SelectedItems)
					{
						it[i++] = idx;
					}
					Array.ForEach(it, m => m.Remove());
				}
			};
		}

		void UserGroupListEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			HostCount = this.hlist.Items.Count;
			DialogResult = HostCount > 0 ? DialogResult.No : DialogResult.OK;
		}

		void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
