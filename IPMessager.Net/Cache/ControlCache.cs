using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Cache
{
	public class ControlCache
	{
		private static List<string> _userGroupList;
		/// <summary>
		/// 用户设置分组
		/// </summary>
		public static List<string> UserGroupList
		{
			get
			{
				if (_userGroupList == null)
					_userGroupList = new List<string>();
				return _userGroupList;
			}
		}

		/// <summary>
		/// 重新加载用户分组列表
		/// </summary>
		public static void ReloadUserGroup()
		{
			_userGroupList = Env.ClientConfig.HostGroupConfig.Values.Distinct().ToList();
		}

		/// <summary>
		/// 删除用户分组
		/// </summary>
		public static void RemoveUserGroup(string groupName)
		{
			if (UserGroupList != null) UserGroupList.Remove(groupName);
		}
	}
}
