using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FSLib.IPMessager.Entity;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class HostConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public HostConfigPanel()
		{
			InitializeComponent();
		}

		private void HostConfigPanel_Load(object sender, EventArgs e)
		{
			//
			LoadUserGroups();
			LoadUserMemo();
		}

		#region 用户分组

		void LoadUserGroups()
		{
			List<ListViewItem> lvtlist = new List<ListViewItem>();
			Env.ClientConfig.HostGroupConfig.Values.GroupBy<string, string>(s => s).ToList().ForEach(s => { lvtlist.Add(new ListViewItem(new string[] { s.Key, s.Count().ToString() })); });
			this.lvUserGroup.Items.AddRange(lvtlist.ToArray());

			lnkGroupEdit.Click += (s, e) => { if (lvUserGroup.SelectedItems.Count > 0)lvUserGroup.SelectedItems[0].BeginEdit(); };
			lnkGroupDelete.Click += (s, e) =>
			{
				if (lvUserGroup.SelectedIndices.Count == 0 || !Question("确定要删除这个分组吗?", false)) return;
				//删除分组
				string gname = lvUserGroup.SelectedItems[0].Text;
				Env.ClientConfig.HostGroupConfig.Where(m => m.Value == gname).ToList().ForEach(m => Env.ClientConfig.HostGroupConfig.Remove(m.Key));
				Cache.ControlCache.RemoveUserGroup(gname);
				lvUserGroup.SelectedItems[0].Remove();
			};
			lvUserGroup.AfterLabelEdit += lvUserGroup_AfterLabelEdit;
			lnkGroupHostManage.Click += lnkGroupHostManage_Click;
		}

		//编辑主机组
		void lnkGroupHostManage_Click(object sender, EventArgs e)
		{
			if (lvUserGroup.SelectedIndices.Count == 0) return;
			//删除分组
			string gname = lvUserGroup.SelectedItems[0].Text;

			Dialogs.Config.UserGroupListEditor editor = new IPMessagerNet.UI.Dialogs.Config.UserGroupListEditor() { GroupName = gname };
			if (editor.ShowDialog() == DialogResult.OK)
			{
				Env.ClientConfig.HostGroupConfig.Where(m => m.Value == gname).ToList().ForEach(m => Env.ClientConfig.HostGroupConfig.Remove(m.Key));
				Cache.ControlCache.RemoveUserGroup(gname);
				lvUserGroup.SelectedItems[0].Remove();
			}
			else
			{
				lvUserGroup.SelectedItems[0].SubItems[1].Text = editor.HostCount.ToString();
			}
		}

		//结束编辑组
		void lvUserGroup_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListViewItem lvt = lvUserGroup.Items[e.Item];
			if (string.IsNullOrEmpty(e.Label) || e.Label == lvt.Text) { e.CancelEdit = true; return; }

			//编辑
			Cache.ControlCache.RemoveUserGroup(lvt.Text);
			Cache.ControlCache.UserGroupList.Add(e.Label);
			Env.ClientConfig.HostGroupConfig.Where(m => m.Value == lvt.Text).ToList().ForEach(m => Env.ClientConfig.HostGroupConfig[m.Key] = e.Label);
			lvt.Text = e.Label;
		}


		#endregion

		#region 备注

		/// <summary>
		/// 加载备注
		/// </summary>
		void LoadUserMemo()
		{
			lvMemo.Items.AddRange(Env.HostConfig.HostMemo.Select<KeyValuePair<string, string>, ListViewItem>(m =>
			{
				Host h = Env.IPMClient.Commander.LivedHost.GetHost(m.Key);
				return new ListViewItem(new string[] { m.Value, m.Key, h == null ? "-" : h.NickName + "/" + h.GroupName });
			}).ToArray());

			lnkMemoEdit.Click += (s, e) => { if (lvMemo.SelectedIndices.Count > 0)lvMemo.SelectedItems[0].BeginEdit(); };
			lnkMemoClear.Click += (s, e) => { if (lvMemo.Items.Count > 0 && Question("确定要全部删除吗?", true)) { lvMemo.Items.Clear(); Env.HostConfig.HostMemo.Clear(); } };
			lnkMemoDelete.Click += (s, e) =>
			{
				if (lvMemo.SelectedIndices.Count > 0)
				{
					ListViewItem[] it = new ListViewItem[lvMemo.SelectedIndices.Count];
					int i = 0;
					foreach (ListViewItem idx in lvMemo.SelectedItems)
					{
						it[i++] = idx;
					}
					Array.ForEach(it, m => m.Remove());
				}
			};
			lvMemo.AfterLabelEdit += lvMemo_AfterLabelEdit;
		}

		//完成编辑
		void lvMemo_AfterLabelEdit(object sender, LabelEditEventArgs e)
		{
			ListViewItem lvt = lvMemo.Items[e.Item];
			if (string.IsNullOrEmpty(e.Label) || e.Label == lvt.Text) { e.CancelEdit = true; return; }

			Env.HostConfig.HostMemo[lvt.SubItems[1].Text] = e.Label;
			lvt.Text = e.Label;
		}

		#endregion

	}
}
