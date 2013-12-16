using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Forms
{
	public partial class ControlContainer : Base.DialogBase
	{
		public ControlContainer()
		{
			InitializeComponent();
			this.tolTopMost.Image = Core.ProfileManager.GetThemePicture("Icons", "TopMost");
		}

		#region 事件

		/// <summary>
		/// 置顶状态切换
		/// </summary>
		public event EventHandler TopMostChanged;

		/// <summary>
		/// 触发置顶状态切换事件
		/// </summary>
		/// <param name="e">参数</param>
		protected virtual void OnTopMostChanged(EventArgs e)
		{
			if (TopMostChanged != null) TopMostChanged(this, e);
		}

		#endregion

		#region UI界面处理

		private void tolTopMost_CheckedChanged(object sender, EventArgs e)
		{
			OnTopMostChanged(e);
			TopMost = tolTopMost.Checked;
		}

		/// <summary>
		/// 初始化事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ControlContainer_Load(object sender, EventArgs e)
		{
			tolTopMost.Checked = this.TopMost;
		}

		#endregion

		#region 公共事件

		/// <summary>
		/// 将控件放入容器中
		/// </summary>
		/// <param name="uc">控件</param>
		public void EmbedControl(UserControl uc)
		{
			uc.Parent = container;
		}

		public void ClearControl()
		{
			container.Controls.Clear();
		}

		#endregion
	}
}
