using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet._Embed;

namespace IPMessagerNet.API
{
	public class ConfigPanelBase : FunctionalUserControl
	{
		/// <summary>
		/// 创建 ConfigPanelBase class 的新实例
		/// </summary>
		public ConfigPanelBase()
		{
			//设置默认的设置
			if (Env.ClientConfig != null)
			{
				this.Dock = DockStyle.Fill;
				this.Load += (s, e) => InitEditor();
			}
			this.Size = new System.Drawing.Size(550, 371);
		}

		/// <summary>
		/// 初始化状态
		/// </summary>
		protected virtual void InitEditor()
		{
		}

		#region 供重载的默认设置

		/// <summary>
		/// 是否支持重置默认设置
		/// </summary>
		public virtual bool SupportsRestoreDefault { get { return false; } }

		/// <summary>
		/// 是否可以卸载标签(切换)
		/// </summary>
		/// <returns></returns>
		public virtual bool CanUnload() { return true; }

		/// <summary>
		/// 重置为默认设置
		/// </summary>
		public virtual void RestoreDefault() { }

		/// <summary>
		/// 是否需要重启软件
		/// </summary>
		public virtual bool RequireRestart { get; set; }

		#endregion
	}
}
