using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.UI.Controls.HostTreeView
{
	[Serializable]
	public class ListConfig
	{
		/// <summary>
		/// 用户组分组的标识
		/// </summary>
		public enum GroupType : int
		{
			None,
			UserDefine,
			IPHeader,
			UserGroup
		}

		/// <summary>
		/// 排序方法
		/// </summary>
		public enum SortOrder : int
		{
			IP,
			State,
			Name
		}

		/// <summary>
		/// 第一分组
		/// </summary>
		public GroupType FirstGroupType { get; set; }

		/// <summary>
		/// 第二分组
		/// </summary>
		public GroupType SecondGroupType { get; set; }

		/// <summary>
		/// 第一排序
		/// </summary>
		public SortOrder FirstOrder { get; set; }

		/// <summary>
		/// 第二排序
		/// </summary>
		public SortOrder SecondOrder { get; set; }

		/// <summary>
		/// 第三排序
		/// </summary>
		public SortOrder ThirdOrder { get; set; }
	}
}
