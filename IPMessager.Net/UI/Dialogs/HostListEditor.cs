using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;
using System.Net;

namespace IPMessagerNet.UI.Dialogs
{
	public partial class HostListEditor : IPMessagerNet.UI.Base.DialogBase
	{
		public HostListEditor()
		{
			InitializeComponent();
			InitOnlineHostList();
			InitInterfaceEvents();
		}

		public new DialogResult ShowDialog()
		{
			ClearPreDefindHostList();

			return base.ShowDialog();
		}

		#region 初始化绑定

		//初始化当前在线主机列表，仅在创建窗体的时候运行一次
		void InitOnlineHostList()
		{
			if (!Env.IsRunning) return;

			ListViewItem[] itemArray = Env.IPMClient.OnlineHost.Values.Select<Host, ListViewItem>(s => CreateHostListViewItem(s)).ToArray();
			this.hlist.Items.AddRange(itemArray);
		}


		#endregion

		#region 绑定主机上下线事件

		/// <summary>
		/// 创建供列表使用的主机项
		/// </summary>
		/// <param name="host">主机信息</param>
		/// <returns>创建好的ListViewItem</returns>
		static ListViewItem CreateHostListViewItem(Host host)
		{
			return new ListViewItem(new string[] { host.HostSub.Ipv4Address.Address.ToString(), host.NickName + "/" + host.GroupName, Env.HostConfig.HostMemo.ContainsKey(host.HostSub.Ipv4Address.Address.ToString()) ? Env.HostConfig.HostMemo[host.HostSub.Ipv4Address.Address.ToString()] : "-" }) { Tag = host };
		}

		#endregion

		#region 处理编辑信息

		/// <summary>
		/// 选定的主机
		/// </summary>
		public string[] SelectedHost { get; set; }

		//清空当前所有选定的主机，并重新添加回在线列表
		void ClearPreDefindHostList()
		{
			ListViewItem[] lvtlist = new ListViewItem[this.lvSelectedHostList.Items.Count];
			this.lvSelectedHostList.Items.CopyTo(lvtlist, 0);
			this.lvSelectedHostList.Items.Clear();
			this.hlist.Items.AddRange(lvtlist);

			if (SelectedHost != null)
			{
				//重新绑定主机
				Array.ForEach(SelectedHost, s => AddHostToSelectedList(s, false));
			}
		}

		#endregion

		#region 处理界面事件

		//绑定界面数据
		void InitInterfaceEvents()
		{
			if (!Env.IsRunning) return;

			//添加
			EventHandler addAction = (s, e) =>
			{
				foreach (ListViewItem item in this.hlist.SelectedItems)
				{
					AddHostToSelectedList(item);
				}
			};
			this.hlist.DoubleClick += addAction;
			this.btnAdd1.Click += addAction;

			//删除
			EventHandler removeAction = (s, e) =>
			{
				foreach (ListViewItem item in this.lvSelectedHostList.SelectedItems)
				{
					this.RemoveHostFromSelectedList(item);
				}
			};
			this.lvSelectedHostList.DoubleClick += removeAction;
			this.btnRemove.Click += removeAction;

			//手动添加
			Action manualAdd = () =>
			{
				if (!string.IsNullOrEmpty(this.txtIP.Text)) AddHostToSelectedList(this.txtIP.Text, true);
				this.txtIP.Text = string.Empty;
			};
			this.btnAdd2.Click += (s, e) => manualAdd();
			this.txtIP.KeyUp += (s, e) =>
			{
				if (e.KeyCode == Keys.Enter) manualAdd();
			};

			this.btnOk.Click += (s, e) =>
			{
				ListViewItem[] lvt = new ListViewItem[this.lvSelectedHostList.SelectedIndices.Count];
				int i = 0;
				foreach (ListViewItem item in this.lvSelectedHostList.SelectedItems)
				{
					lvt[i++] = item;
				}

				this.SelectedHost = Array.ConvertAll<ListViewItem, string>(lvt, h => { return h.SubItems[0].Text; });
				DialogResult = DialogResult.OK;
			};
		}

		//添加主机到选定列表
		void AddHostToSelectedList(ListViewItem item)
		{
			hlist.Items.Remove(item);
			this.lvSelectedHostList.Items.Add(item);
		}

		//从选定列表删除选定主机
		void RemoveHostFromSelectedList(ListViewItem item)
		{
			lvSelectedHostList.Items.Remove(item);
			this.hlist.Items.Add(item);
		}

		//手动添加IP
		void AddHostToSelectedList(string ipstr, bool verifyInput)
		{
			if (verifyInput)
			{
				IPAddress ip = null;
				if (!IPAddress.TryParse(ipstr, out ip))
				{
					Information("无法识别的IP地址，请确认您输入了正确的IP地址。");
					return;
				}
			}

			//尝试列表里面查找一下
			ListViewItem searchItem = null;
			foreach (ListViewItem item in this.hlist.Items)
			{
				if (item.SubItems[0].Text == ipstr)
				{
					searchItem = item;
					break;
				}
			}
			foreach (ListViewItem item in this.lvSelectedHostList.Items)
			{
				if (item.SubItems[0].Text == ipstr)
				{
					return;
				}
			}

			if (searchItem == null)
			{
				//当前不在线，直接添加
				Host h = Env.IPMClient.OnlineHost.GetHost(ipstr);
				if (h == null) searchItem = new ListViewItem(new string[] { ipstr, h == null ? "离线" : h.NickName + "/" + h.GroupName, Env.HostConfig.HostMemo.ContainsKey(ipstr) ? Env.HostConfig.HostMemo[ipstr] : "-" });
				else searchItem = CreateHostListViewItem(h);

				this.lvSelectedHostList.Items.Add(searchItem);
			}
			else
				this.AddHostToSelectedList(searchItem);
		}

		#endregion
	}
}
