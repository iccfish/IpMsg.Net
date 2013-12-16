using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	abstract class HostCollectionBase : TreeNode
	{
		/// <summary>
		/// 当前的组层次
		/// </summary>
		public int GroupLevel { get; set; }

		#region 虚函数-由上层改写

		/// <summary>
		/// 确定这个主机是否在这个组中
		/// </summary>
		/// <param name="host"></param>
		/// <returns></returns>
		public abstract bool IsHostInThisGroup(FSLib.IPMessager.Entity.Host host);

		#endregion



	}
}
