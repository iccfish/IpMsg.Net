using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Config
{
	[Serializable]
	public class HostInfoConfig
	{
		/// <summary>
		/// 显示模式
		/// </summary>
		public HostNameDisplayStyle DisplayStyle { get; set; }

		/// <summary>
		/// 上线提示
		/// </summary>
		public HostBallonTip HostOnlineTip { get; set; }

		/// <summary>
		/// 下线提示
		/// </summary>
		public HostBallonTip HostOfflineTip { get; set; }

		/// <summary>
		/// 备注
		/// </summary>
		public FSLib.IPMessager.Entity.SerializableDictionary<string,string> HostMemo { get; set; }

		/// <summary>
		/// 上线提示主机
		/// </summary>
		public List<string> OnlineTip { get; set; }

		/// <summary>
		/// 下线主机
		/// </summary>
		public List<string> OfflineTip { get; set; }

		/// <summary>
		/// 是否在静音模式中禁止所有上下线提示
		/// </summary>
		public bool DisableHostTipInQuite { get; set; }
	}

	public enum HostNameDisplayStyle : int
	{
		MemoOnly,
		MemoBeforeName,
		NameOnly
	}

	public enum HostBallonTip : int
	{
		None,
		All,
		Special
	}
}
