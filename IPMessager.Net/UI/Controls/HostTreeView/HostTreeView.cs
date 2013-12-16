using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FSLib.IPMessager;
using FSLib.IPMessager.Entity;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	public class HostTreeView : TreeView
	{
		/// <summary>
		/// 创建 HostTreeView class 的新实例
		/// </summary>
		public HostTreeView()
		{
			InitializeComponent();
			this.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top;

			if (Env.ClientConfig == null)
				return;

			//初始化
			InitInterface();
			InitEvents();
		}
		#region 代码生成器生成的方法

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mChat = new System.Windows.Forms.ToolStripMenuItem();
			this.mSepChat = new System.Windows.Forms.ToolStripSeparator();
			this.mGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mBan = new System.Windows.Forms.ToolStripMenuItem();
			this.mDial = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxMenu
			// 
			this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mChat,
            this.mSepChat,
            this.mGroup,
            this.mBan,
            this.mDial});
			this.ctxMenu.Name = "ctxMenu";
			this.ctxMenu.Size = new System.Drawing.Size(190, 98);
			this.ctxMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenu_Opening);
			// 
			// mChat
			// 
			this.mChat.Name = "mChat";
			this.mChat.Size = new System.Drawing.Size(189, 22);
			this.mChat.Text = "和TA对话";
			this.mChat.ToolTipText = "同TA进入消息对话模式";
			// 
			// mSepChat
			// 
			this.mSepChat.Name = "mSepChat";
			this.mSepChat.Size = new System.Drawing.Size(178, 6);
			// 
			// mGroup
			// 
			this.mGroup.Name = "mGroup";
			this.mGroup.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.mGroup.Size = new System.Drawing.Size(189, 22);
			this.mGroup.Text = "修改备注/分组";
			this.mGroup.ToolTipText = "显示的时候以备注优先，修改备注可以更快的识别TA";
			// 
			// mBan
			// 
			this.mBan.Name = "mBan";
			this.mBan.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.mBan.Size = new System.Drawing.Size(181, 22);
			this.mBan.Text = "加入黑名单";
			this.mBan.ToolTipText = "加入黑名单，阻止所有TA的信息";
			// 
			// mDial
			// 
			this.mDial.Name = "mDial";
			this.mDial.ShortcutKeys = System.Windows.Forms.Keys.Insert;
			this.mDial.Size = new System.Drawing.Size(189, 22);
			this.mDial.Text = "加入拨号列表";
			// 
			// HostTreeView
			// 
			this.ContextMenuStrip = this.ctxMenu;
			this.LineColor = System.Drawing.Color.Black;
			this.ctxMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region 属性

		/// <summary>
		/// 当前活动主机
		/// </summary>
		public Host SelectedHost
		{
			get
			{
				if (this.SelectedNode == null) return null;

				object node = this.SelectedNode;
				if (node is HostNode)
				{
					return (node as HostNode).Host;
				}
				else if (node is HostInfoNodeBase)
				{
					return (node as HostInfoNodeBase).Host;
				}
				else
				{
					return null;
				}
			}
		}



		#endregion

		#region 状态管理

		bool isBlockHostDisabled;

		/// <summary>
		/// 初始化界面
		/// </summary>
		void InitInterface()
		{
			int rowCount;
			ImageList imglist = new ImageList()
			{
				ImageSize = new System.Drawing.Size(16, 16),
				ColorDepth = ColorDepth.Depth32Bit
			};
			imglist.Images.AddRange(ImageHelper.SplitImage(Core.ProfileManager.GetThemePicturePath<HostTreeView>(), 16, 16, out rowCount).ToArray());

			this.ImageList = imglist;

			//处理菜单图标
			List<Image> tb = ImageHelper.SplitImage(Core.ProfileManager.GetThemePicture("Toolbar", "HostTreeViewTool"), 16, 16, out rowCount);
			int startIndex = 0;
			ControlHelper.FillMenuButtonImage(this.ContextMenuStrip, tb.ToArray(), ref startIndex);

			//初始化状态
			isBlockHostDisabled = !(this.mBan.Enabled = Env.IPMClient.IsInnerServiceEnabled(FSLib.IPMessager.Services.InnerService.BlackListBlocker));
		}


		#endregion

		#region 公共方法

		/// <summary>
		/// 指定节点中删除主机
		/// </summary>
		/// <param name="host">主机</param>
		public void RemoveHost(Host host)
		{
			foreach (TreeNode node in this.Nodes)
			{
				if (node is HostCollectionBase) RemoveHost(host, node);
				else if (node is HostNode && (node as HostNode).Host == host) node.Remove();
			}
		}

		/// <summary>
		/// 指定节点中删除主机
		/// </summary>
		/// <param name="host">主机</param>
		/// <param name="parent">节点</param>
		void RemoveHost(Host host, TreeNode parent)
		{
			TreeNode n = null;
			TreeNodeCollection tnc = parent == null ? this.Nodes : parent.Nodes;

			foreach (TreeNode node in tnc)
			{
				if (node is HostCollectionBase) RemoveHost(host, node);
				else if (node is HostNode && (node as HostNode).Host == host)
				{
					n = node;
					break;
				}
			}
			if (n != null)
			{
				while (n.Parent != null && n.Parent.Nodes.Count == 1) n = n.Parent;	//向上遍历一下，以便于删除空节点
				n.Remove();
			}
		}

		/// <summary>
		/// 将指定主机添加到列表中
		/// </summary>
		/// <param name="host"></param>
		public void AddHost(Host host)
		{
			//查找父节点
			TreeNode parentNode = FindCollectionNodeForHost(host);
			//查找排序
			int index = FindIndexForHost(host, parentNode);

			//加入
			if (parentNode == null) this.Nodes.Insert(index, new HostNode(host));
			else parentNode.Nodes.Insert(index, new HostNode(host));
		}

		/// <summary>
		/// 查找主机应该位于的分组节点，如果没有找到，则返回空值
		/// </summary>
		/// <param name="host">要添加的节点</param>
		/// <returns>返回主机应该位于的分组节点，如果没有找到，则返回空值</returns>
		public TreeNode FindCollectionNodeForHost(Host host)
		{
			ListConfig cfg = Env.ClientConfig.HostListViewConfig;
			TreeNode parentNode = null;

			//查找第一级别分组
			if (cfg.FirstGroupType == ListConfig.GroupType.None) return null;
			foreach (TreeNode node in this.Nodes)
			{
				HostCollectionBase b = node as HostCollectionBase;
				if (b != null && b.IsHostInThisGroup(host))
				{
					parentNode = node;
					break;
				}
			}
			//如果没有找到，则创建
			if (parentNode == null)
			{
				switch (cfg.FirstGroupType)
				{
					case ListConfig.GroupType.UserDefine:
						parentNode = new HostUserDefineCollectionNode() { GroupLevel = 0, GroupName = HostUserDefineCollectionNode.GetHostGroupName(host) };
						break;
					case ListConfig.GroupType.IPHeader:
						parentNode = new HostIPCollectionNode() { GroupLevel = 0, IPHeader = host.HostSub.IPHeader };
						break;
					case ListConfig.GroupType.UserGroup:
						parentNode = new HostGroupCollectionNode() { GroupLevel = 0, GroupName = host.GroupName };
						break;
				}
				this.Nodes.Add(parentNode);
			}

			//查找第二级节点
			if (cfg.SecondGroupType == ListConfig.GroupType.None) return parentNode;
			TreeNode subNode = null;
			foreach (TreeNode node in parentNode.Nodes)
			{
				HostCollectionBase b = node as HostCollectionBase;
				if (b != null && b.IsHostInThisGroup(host))
				{
					subNode = node;
					break;
				}
			}
			//如果没有找到，则创建
			if (subNode == null)
			{
				switch (cfg.SecondGroupType)
				{
					case ListConfig.GroupType.UserDefine:
						subNode = new HostUserDefineCollectionNode() { GroupLevel = 1, GroupName = HostUserDefineCollectionNode.GetHostGroupName(host) };
						break;
					case ListConfig.GroupType.IPHeader:
						subNode = new HostIPCollectionNode() { GroupLevel = 1, IPHeader = host.HostSub.IPHeader };
						break;
					case ListConfig.GroupType.UserGroup:
						subNode = new HostGroupCollectionNode() { GroupLevel = 1, GroupName = host.GroupName };
						break;
				}
				parentNode.Nodes.Add(subNode);
			}

			return subNode;
		}

		private ToolStripMenuItem mChat;
		private ToolStripMenuItem mGroup;
		private ToolStripMenuItem mBan;
		private ToolStripSeparator mSepChat;
		private ToolStripMenuItem mDial;

		//排序比较器
		IComparer<Host> ipComparer = new Utility.HostSortComarer_IP();
		IComparer<Host> nameComparer = new Utility.HostSortComarer_Name();
		private ContextMenuStrip ctxMenu;
		private System.ComponentModel.IContainer components;
		IComparer<Host> statusComparer = new Utility.HostSortComarer_Status();

		/// <summary>
		/// 查找主机应该位于的索引位置
		/// </summary>
		/// <param name="host">主机</param>
		/// <param name="parentNode">父节点</param>
		/// <returns></returns>
		public int FindIndexForHost(Host host, TreeNode parentNode)
		{
			TreeNodeCollection tnc = this.Nodes;
			if (parentNode != null) tnc = parentNode.Nodes;

			ListConfig cfg = Env.ClientConfig.HostListViewConfig;

			IComparer<Host> c1 = cfg.FirstOrder == ListConfig.SortOrder.IP ? ipComparer : (cfg.FirstOrder == ListConfig.SortOrder.Name ? nameComparer : statusComparer);
			IComparer<Host> c2 = cfg.SecondOrder == ListConfig.SortOrder.IP ? ipComparer : (cfg.SecondOrder == ListConfig.SortOrder.Name ? nameComparer : statusComparer);

			int i = 0;
			while (i < tnc.Count && (c1.Compare(host, (tnc[i] as HostNode).Host) > 0 || c2.Compare(host, (tnc[i] as HostNode).Host) > 0)) i++;

			return i;
		}

		/// <summary>
		/// 将列表中的主机全部清除
		/// </summary>
		public void ClearHost()
		{
			this.Nodes.Clear();
		}

		#endregion

		#region 事件处理

		void InitEvents()
		{
			//外部事件
			Env.IPMClient.OnlineHost.HostOnline += (s, e) => { AddHost(e.Host); };
			Env.IPMClient.OnlineHost.HostOffline += (s, e) => { RemoveHost(e.Host); };
			Env.IPMClient.OnlineHost.HostCleared += (s, e) => { ClearHost(); };

			//内部事件
			this.mGroup.Click += (s, e) => { ModifyGroupAndMemo(); };
			this.mChat.Click += (s, e) => { OpenChatPage(); };
			this.DoubleClick += (s, e) => { OpenChatPage(); };
			this.mBan.Click += (s, e) => { BanHost(true); };
			this.mDial.Click += (s, e) => { DialUpHost(true); };
		}

		//判断菜单可用性
		private void ctxMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			mChat.Enabled = mGroup.Enabled = mBan.Enabled = mDial.Enabled = this.SelectedHost != null;
			if (isBlockHostDisabled) mBan.Enabled = false;
		}

		/// <summary>
		/// 修改选定主机的备注和分组
		/// </summary>
		/// <param name="tn"></param>
		public void ModifyGroupAndMemo()
		{
			Host h = this.SelectedHost;
			if (h == null) return;

			Dialogs.ModifyHostGroupAndMemo dlg = new IPMessagerNet.UI.Dialogs.ModifyHostGroupAndMemo() { Host = h };
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				this.RemoveHost(h);
				this.AddHost(h);
			}
		}

		/// <summary>
		/// 请求打开聊天页面
		/// </summary>
		public void OpenChatPage()
		{
			Host h = this.SelectedHost;

			if (h != null)
			{
				this.OnChatActionRequired(new System.EventArgs());
			}
		}

		/// <summary>
		/// 屏蔽主机
		/// </summary>
		/// <param name="prompt">是否显示提示消息</param>
		public void BanHost(bool prompt)
		{
			Host h = this.SelectedHost;
			if (h == null) return;

			if (prompt && MessageBox.Show("确定要将这个主机加入黑名单吗？如果加入了黑名单，那么它所有的消息将不会被您接受。\r\n要解除屏蔽，请在 系统设置 -> 安全设置 中解除。", "确定", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

			Env.ClientConfig.IPMClientConfig.BanHost(h);
			RemoveHost(h);
		}

		/// <summary>
		/// 将选定的主机加入保持列表
		/// </summary>
		/// <param name="prompt">是否显示提示消息</param>
		public void DialUpHost(bool prompt)
		{
			Host h = this.SelectedHost;
			if (h == null) return;

			foreach (var ip in Env.IPMClient.LocalAddresses)
			{
				if (h.HostSub.Ipv4Address.Address.IsSameIPSectionAS(ip))
				{
					if (prompt) MessageBox.Show("您选择的主机和您位于同一个子网内，可以通过广播与TA联系，不需要加入拨号列表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}

			Env.IPMClient.Config.AddHostToDialList(h.HostSub.Ipv4Address.Address);
					if (prompt) MessageBox.Show("添加成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}


		#endregion

		#region 事件

		/// <summary>
		/// 请求聊天
		/// </summary>
		public event EventHandler ChatActionRequired;

		/// <summary>
		/// Triggers the ChatActionRequired event.
		/// </summary>
		public virtual void OnChatActionRequired(EventArgs ea)
		{
			if (ChatActionRequired != null)
				ChatActionRequired(this, ea);
		}


		#endregion

	}
}
