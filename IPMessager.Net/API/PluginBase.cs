using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.API
{
	/// <summary>
	/// 插件UI界面基类,提供对部分方法的默认实现
	/// </summary>
	public abstract class PluginUIBase
	{
		#region 菜单/工具栏注册部分

		/// <summary>
		/// 注册主菜单
		/// </summary>
		/// <param name="parent">主菜单</param>
		public virtual void RegisterMainMenu(MenuStrip parent)
		{
		}

		/// <summary>
		/// 注册主机列表的工具栏菜单
		/// </summary>
		/// <param name="parent">工具栏</param>
		public virtual void RegisterHostViewToolbar(ToolStrip parent)
		{
		}

		/// <summary>
		/// 注册主机列表的上下文菜单
		/// </summary>
		/// <param name="parent"></param>
		public virtual void RegisterHostListContextMenu(ContextMenuStrip parent)
		{
		}

		/// <summary>
		/// 注册聊天区域工具栏
		/// </summary>
		/// <param name="parent">工具栏</param>
		public virtual void RegisterChatAreaToolbar(ToolStrip parent)
		{
		}

		/// <summary>
		/// 添加主机时注册信息节点
		/// </summary>
		/// <param name="parent">主机节点</param>
		/// <returns>节点对象</returns>
		public virtual UI.Controls.HostTreeView.HostInfoNodeBase RegisterHostInfoNode(UI.Controls.HostTreeView.HostNode parent)
		{
			return null;
		}

		#endregion
	}
}
