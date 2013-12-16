using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	class HostIPCollectionNode : HostCollectionBase
	{
		private uint _iPHeader;
		/// <summary>
		/// 显示的iP段
		/// </summary>
		public uint IPHeader
		{
			get
			{
				return _iPHeader;
			}
			set
			{
				_iPHeader = value;
				this.Text = FSLib.IPMessager.Helper.IpDisGen(value);
			}
		}

		public override bool IsHostInThisGroup(FSLib.IPMessager.Entity.Host host)
		{
			return host.HostSub.IPHeader == _iPHeader;
		}
	}
}
