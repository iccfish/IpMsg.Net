using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPMessagerNet.Config
{
	[Serializable]
	public class ClientConfig
	{
		/// <summary>
		/// 飞鸽客户端用到的设置
		/// </summary>
		public FSLib.IPMessager.Entity.Config IPMClientConfig { get; set; }

		#region UI 设置

		/// <summary>
		/// 主题设置
		/// </summary>
		public string Themes { get; set; }

		/// <summary>
		/// 主机列表控件视图
		/// </summary>
		public UI.Controls.HostTreeView.ListConfig HostListViewConfig { get; set; }

		/// <summary>
		/// 主窗口设置
		/// </summary>
		public UI.Forms.FrameContainerConfig FrameContainerConfig { get; set; }

		/// <summary>
		/// 离开状态信息
		/// </summary>
		public List<string> AbsenceMessage { get; set; }

		/// <summary>
		/// 主机组设置
		/// </summary>
		public FSLib.IPMessager.Entity.SerializableDictionary<string, string> HostGroupConfig { get; set; }

		/// <summary>
		/// 聊天区设置
		/// </summary>
		public ChatAreaConfig ChatConfig { get; set; }


		#endregion

		#region 功能设置

		/// <summary>
		/// 主机功能设置
		/// </summary>
		public HostInfoConfig HostInfo { get; set; }

		/// <summary>
		/// 客户端功能设置
		/// </summary>
		public ClientFunctionConfig FunctionConfig { get; set; }

		private SoundConfig _sound;
		/// <summary>
		/// 声音设置
		/// </summary>
		public SoundConfig Sound
		{
			get
			{
				if (_sound == null) _sound = new SoundConfig();

				return _sound;
			}
			set
			{
				_sound = value;
			}
		}

		#endregion

		#region 对象操作

		/// <summary>
		/// 保存配置更改
		/// </summary>
		public void Save()
		{
			Core.ProfileManager.SaveConfig(this);
		}


		#endregion
	}
}
