using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using FSLib.IPMessager;
using FSLib.IPMessager.Services;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class NetworkConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public NetworkConfigPanel()
		{
			InitializeComponent();
		}

		private void NetworkConfigPanel_Load(object sender, EventArgs e)
		{
			tcpTimeout.DataInstance = Env.IPMClient.Config;
			nudPort.Value = Env.IPMClient.Config.Port;
			//绑定iP
			cbBip.Items.Add("不绑定任何IP");
			cbBip.SelectedIndex = 0;
			foreach (var ip in Env.IPMClient.LocalAddresses)
			{
				if (ip.IsIPv6LinkLocal) continue;	//跳过本地地址
				cbBip.Items.Add(ip.ToString());
				if (ip.ToString() == Env.IPMClient.Config.BindedIPString) cbBip.SelectedIndex = cbBip.Items.Count - 1;
			}

			LoadDialIP();

			this.VisibleChanged += (s, f) =>
			{
				if (this.Visible) LoadAdvanceOptions();
			};
		}

		//加载拨号IP和初始化事件
		void LoadDialIP()
		{
			lstDialup.Items.AddRange(Env.IPMClient.Config.KeepedHostList.ToArray());

			btnDialAdd.Click += (s, e) =>
			{
				string v = txtDialIp.Text;
				IPAddress ip = null;

				if (string.IsNullOrEmpty(v) || !IPAddress.TryParse(v, out ip)) { Information("请输入正确的IP地址"); return; }
				if (Env.IPMClient.Config.KeepedHostList.Contains(v)) return;

				foreach (var _ip in Env.IPMClient.LocalAddresses)
				{
					if (ip.IsSameIPSectionAS(_ip))
					{
						MessageBox.Show("您选择的主机和您位于同一个子网内，可以通过广播与TA联系，不需要加入拨号列表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
						return;
					}
				}


				Env.IPMClient.Config.AddHostToDialList(ip);
				lstDialup.Items.Add(v);
			};
			btnDialDelete.Click += (s, e) =>
			{
				if (lstDialup.SelectedIndex == -1) return;
				int[] idxlist = new int[lstDialup.SelectedIndices.Count];
				for (int i = 0; i < lstDialup.SelectedIndices.Count; i++)
				{
					idxlist[i] = lstDialup.SelectedIndices[i];
					//删除
					Env.IPMClient.Config.RemoveHostFromDialList(lstDialup.SelectedItems[i].ToString());
				}
				Array.ForEach(idxlist, i => lstDialup.Items.RemoveAt(i));
			};

			lstDialup.SelectionMode = SelectionMode.MultiExtended;
		}

		//高级选项
		void LoadAdvanceOptions()
		{
			ServiceInfo iflp = Env.IPMClient.Config.Services.Find(s => s.TypeName == ServiceManager.InnerServiceTypeList[InnerService.RemoveLoopBackMessage]);
			if (iflp == null) chkFilterLocalMsg.Enabled = false;
			else
			{
				chkFilterLocalMsg.Checked = iflp.Enabled;
			}
			chkFilterLocalMsg.CheckedChanged += (s, e) =>
			{
				iflp.Enabled = chkFilterLocalMsg.Checked;
				if (chkFilterLocalMsg.Checked)
				{
					if (iflp.EnsureLoadAssembly() && iflp.CreateProviderInstance() && iflp.InitialzingServiceProvider(Env.IPMClient)) iflp.LoadService();
				}
				else
					iflp.ShutDown();
			};

		}
	}
}
