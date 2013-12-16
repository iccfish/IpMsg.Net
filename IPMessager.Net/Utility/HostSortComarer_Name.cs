using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Entity;


namespace IPMessagerNet.Utility
{
	/// <summary>
	/// 比较器-用于对用户名进行排序
	/// </summary>
	class HostSortComarer_Name : IComparer<Host>
	{
		#region IComparer<Host> 成员

		public int Compare(Host x, Host y)
		{
			return string.Compare(x.NickName, y.NickName);
		}

		#endregion
	}
}
