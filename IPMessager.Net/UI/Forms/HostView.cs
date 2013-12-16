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
	public partial class HostView : Form
	{
		public HostView()
		{
			InitializeComponent();

			this.Shown += HostView_Shown;
		}

		#region UI事件处理

		/// <summary>
		/// 界面显示出来，这时候初始化在线事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void HostView_Shown(object sender, EventArgs e)
		{
			Env.IPMClient.Online();
		}


		#endregion



	}
}
