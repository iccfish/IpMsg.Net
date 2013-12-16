using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Entity;




namespace IPMessagerNet.Utility
{
	/// <summary>
	/// 比较器-用于对主机状态进行排序
	/// </summary>
	class HostSortComarer_Status : IComparer<Host>
	{
		#region IComparer<Host> 成员

		public int Compare(Host x, Host y)
		{
			int xi = x.IsInAbsenceMode ? 1 : 0;
			int yi = y.IsInAbsenceMode ? 1 : 0;

			return xi - yi;
		}

		#endregion
	}
}
