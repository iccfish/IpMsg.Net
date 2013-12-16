using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Services;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class PluginsConfigPanel : API.ConfigPanelBase
	{
		public PluginsConfigPanel()
		{
			InitializeComponent();

			InitEvents();
			LoadPluginsToList();

			this.Load += (s, e) => isLoad = false;
			this.VisibleChanged += (s, e) =>
			{
				if (!this.Visible) return;
				foreach (ListViewItem lvt in pluginsList.Items)
				{
					ServiceInfo info = lvt.Tag as ServiceInfo;
					lvt.Checked = info.Enabled;
				}
			};
		}

		bool isLoad = true;

		private void LoadPluginItem(ServiceInfo item)
		{
			ListViewItem lvt = new ListViewItem(item.ServiceDescription == null ? item.TypeName : item.ServiceDescription.Name);
			lvt.SubItems.Add(GetStateDescription(item.State));

			if (item.ServiceProvider == null)
			{
				lvt.SubItems.Add("");
			}
			else
			{
				lvt.SubItems.Add(item.ServiceProvider.Version);
			}

			lvt.SubItems.Add(item.ServiceDescription.Author);
			lvt.SubItems.Add(item.Assembly);
			lvt.SubItems.Add(item.TypeName);

			if (item.ServiceProvider != null && item.ServiceProvider.PluginIcon != null)
			{
				imgList.Images.Add(item.TypeName, item.ServiceProvider.PluginIcon);
				lvt.ImageKey = item.TypeName;
			}
			lvt.Checked = item.Enabled;

			pluginsList.Items.Add(lvt);
			lvt.Tag = item;
		}

		void RefreshPluginsInfo(ListViewItem lvtItem)
		{
			ServiceInfo item = lvtItem.Tag as ServiceInfo;
			lvtItem.SubItems[1].Text = GetStateDescription(item.State);

			if (string.IsNullOrEmpty(lvtItem.ImageKey) && item.ServiceProvider != null && item.ServiceProvider.PluginIcon != null)
			{
				imgList.Images.Add(item.TypeName, item.ServiceProvider.PluginIcon);
				lvtItem.ImageKey = item.TypeName;
				lvtItem.SubItems[2].Text = item.ServiceProvider.Version;
			}
		}

		string GetStateDescription(ServiceState state)
		{
			switch (state)
			{
				case ServiceState.NotInstalled:
					return "未加载";
				case ServiceState.Running:
					return "运行中";
				case ServiceState.Disabled:
					return "已禁用";
				case ServiceState.LoadingError:
					return "加载错误";
				case ServiceState.UnInitialized:
					return "未初始化";
				case ServiceState.Unload:
					return "已卸载";
				case ServiceState.TypeLoaded:
					return "类型已加载";
				default:
					return "未知";
			}
		}

		/// <summary>
		/// 加载插件到列表
		/// </summary>
		void LoadPluginsToList()
		{
			if (Env.ClientConfig == null) return;

			pluginsList.Items.Clear();

			foreach (var item in Env.ClientConfig.IPMClientConfig.Services)
			{
				LoadPluginItem(item);
			}
		}

		/// <summary>
		/// 初始化插件事件
		/// </summary>
		void InitEvents()
		{
			if (Env.ClientConfig == null) return;

			pluginsList.SelectedIndexChanged += (s, e) =>
			{
				if (pluginsList.FocusedItem == null) { txtDesc.Text = ""; btnConfig.Enabled = false; }
				ServiceInfo si = pluginsList.FocusedItem.Tag as ServiceInfo;
				txtDesc.Text = string.Format("{0}\r\n\r\n插件作者：{1}\r\n联系方式：{2}\r\n版权声明：{3}", si.ServiceDescription.Description,
					si.ServiceDescription.Author, si.ServiceDescription.Contact, si.ServiceDescription.CopyRight);
				btnConfig.Enabled = (si.ServiceProvider != null && si.ServiceProvider.SupportsConfig);
			};
			pluginsList.ItemChecked += (s, e) =>
			{
				if (isLoad) return;
				//
				if (e.Item == null || e.Item.Tag == null) return;
				ServiceInfo si = e.Item.Tag as ServiceInfo;
				if (si.Enabled == e.Item.Checked) return;

				si.Enabled = e.Item.Checked;

				if (si.Enabled)
				{
					if (!Env.StartupServiceProvider(si))
					{
						if (si.State == ServiceState.LoadingError)
						{
							MessageBox.Show("插件似乎无法加载，请重新扫描插件。", "插件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						else
						{
							MessageBox.Show("已经启用指定插件，但是由于插件报告它不支持运行中启用或状态未更改，因此还未能生效，请重启飞鸽传书.net来加载它。", "插件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					}
				}
				else
				{
					if (!Env.ShutdownServiceProvider(si)) MessageBox.Show("已经禁用指定插件，但是由于插件未能加载或报告它不支持运行中禁用或状态未更改，因此还未能生效，请重启飞鸽传书.net来加载它。", "插件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				RefreshPluginsInfo(e.Item);
			};
			btnRescan.Click += (s, e) =>
			{
				WaitingDialog wd = new WaitingDialog() { ShowLog = true };
				wd.ThreadWorker = p => { RescanServices(p); };
				wd.ShowDialog();
			};
			btnConfig.Click += (s, e) =>
			{
				if (pluginsList.FocusedItem == null) return;

				ServiceInfo si = pluginsList.FocusedItem.Tag as ServiceInfo;
				Form f = si.ServiceProvider.ConfigUI as Form;

				if (f != null) f.ShowDialog();
			};
		}

		void RescanServices(ProgressIdentifier act)
		{
			act.StateMessage = "正在从程序集中扫描插件........";

			string[] installedService = Array.ConvertAll<ServiceInfo, string>(Env.ClientConfig.IPMClientConfig.Services.ToArray(), s => s.TypeName.ToLower());
			ServiceList newservice = ServiceManager.GetServices(Program.GetAddinPath());


			int count = 0;
			int failed = 0;
			int succeed = 0;

			act.MaxValue = newservice.Count;
			act.CurrentValue = 0;

			foreach (var ns in newservice)
			{
				act.CurrentValue++;

				if (installedService.Contains(ns.TypeName.ToLower())) continue;

				//因为我们需要显示插件信息，所以需要预先加载啦。。。
				if (!ns.EnsureLoadAssembly()) continue;

				if (!ns.Enabled)
				{
					act.NotifyStateObjChanged("插件 " + ns.ServiceDescription.Name + " 默认为禁用状态");
				}

				count++;

				act.StateMessage = "正在尝试加载 " + ns.ServiceDescription.Name + " ....";


				Env.ClientConfig.IPMClientConfig.Services.Add(ns);


				if (ns.EnsureLoadAssembly() && ns.CreateProviderInstance() && ns.InitialzingServiceProvider(Env.IPMClient))
				{
					if (!ns.LoadService())
					{
						failed++;
						act.NotifyStateObjChanged("插件 " + ns.ServiceDescription.Name + " 未能成功加载，可能需要重新启动飞鸽传书");
					}
					else
					{
						succeed++;
						act.NotifyStateObjChanged("插件 " + ns.ServiceDescription.Name + " 已经成功加载");
					}
				}
				else
				{
					act.NotifyStateObjChanged("插件 " + ns.ServiceDescription.Name + " 未能成功加载，请联系插件作者");
					failed++;
				}
				LoadPluginItem(ns);
			}

			btnRescan.Enabled = true;
			MessageBox.Show(string.Format("已经重新扫描插件，已找到 {0} 个新插件，已成功加载 {1} 个，{2}。", count, succeed, failed == 0 ? "没有插件加载失败" : "有 " + failed.ToString() + " 个插件没有能成功加载，请重新启动飞鸽传书来加载它们。"), "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
