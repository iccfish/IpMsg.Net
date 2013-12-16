using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet.API;

namespace IPMessagerNet.UI.Forms
{
	public partial class SysConfig : Base.FormBase
	{
		public SysConfig()
		{
			InitializeComponent();

			//
			if (Env.ClientConfig != null)
			{
				LoadMenu();
				InitEvents();

				this.panConfig.Controls.Add(new Controls.Config.ConfigPanelHome());
			}
		}

		/// <summary>
		/// 加载菜单
		/// </summary>
		void LoadMenu()
		{
			lstMenu.Items.Add(new Controls.Config.GeneralConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.HostConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.StateConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.AudioConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.NotifyConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.SecurityConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.FileConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.NetworkConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.ThemeConfigMenuItem());
			//lstMenu.Items.Add(new Controls.Config.AdvancedConfigMenuItem());

			//加载插件的设置页面
			foreach (var p in Env.ClientConfig.IPMClientConfig.Services)
			{
				if (!p.Enabled || p.ServiceProvider == null || !p.ServiceProvider.SupportControlPanel) continue;
				lstMenu.Items.Add(new Controls.Config.PluginConfigMenuItem(p));
			}

			lstMenu.Items.Add(new Controls.Config.PluginsConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.TestingConfigMenuItem());
			lstMenu.Items.Add(new Controls.Config.AutoUpdateConfigMenuItem());

		}

		/// <summary>
		/// 初始化事件
		/// </summary>
		void InitEvents()
		{
			lstMenu.SelectedIndexChanged += (s, e) =>
			{
				if (lstMenu.SelectedIndex == -1) return;

				panConfig.Controls.Clear();

				Control uc = (lstMenu.SelectedItem as IConfigMenuItem).UserControl;
				if (uc == null) return;
				panConfig.Controls.Add(uc);
			};

			this.FormClosing += (s, e) =>
			{
				NotifyChanged();
			};
		}

		/// <summary>
		/// 通知当前配置的变化
		/// </summary>
		void NotifyChanged()
		{
			//用户名组名?
			Env.IPMClient.Commander.SendAbsenceMessage();
			//重新绑定离开菜单
			Forms.FrameContainer.ContainerForm.HostView.BindAbsenceMessageMenu();
		}

		/// <summary>
		/// 显示关于窗口
		/// </summary>
		private void btnAbout_Click(object sender, EventArgs e)
		{
			new Dialogs.Notify.AboutIPM().ShowDialog();
		}

		/// <summary>
		/// 重启客户端
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRestart_Click(object sender, EventArgs e)
		{
			Program.Restart();
		}
	}
}
