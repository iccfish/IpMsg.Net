using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostGroupCollectionNode : HostCollectionBase
	{
		public override bool IsHostInThisGroup(FSLib.IPMessager.Entity.Host host)
		{
			return string.Compare(host.GroupName, this.Text, true) == 0;
		}

		/// <summary>
		/// 主机组的名字
		/// </summary>
		public string GroupName
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}
	}
}
