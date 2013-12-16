using System;
using System.Linq;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;

namespace IPMessagerNet.UI.Dialogs
{
	public partial class ModifyHostGroupAndMemo : Base.DialogBase
	{
		public ModifyHostGroupAndMemo()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 绑定的主机
		/// </summary>
		public Host Host { get; set; }

		private void ModifyHostGroupAndMemo_Load(object sender, EventArgs e)
		{
			//加载自定义分组
			this.cbGroup.Items.AddRange(Cache.ControlCache.UserGroupList.ToArray());

			//初始化数据
			string key = Host.HostSub.Ipv4Address.Address.ToString();
			this.cbGroup.Text = Env.ClientConfig.HostGroupConfig.ContainsKey(key) ? Env.ClientConfig.HostGroupConfig[key] : string.Empty;
			this.txtMemo.Text = Env.ClientConfig.HostInfo.HostMemo.ContainsKey(key) ? Env.ClientConfig.HostInfo.HostMemo[key] : string.Empty;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			string key = Host.HostSub.Ipv4Address.Address.ToString();

			if (string.IsNullOrEmpty(txtMemo.Text))
			{
				if (Env.ClientConfig.HostInfo.HostMemo.ContainsKey(key)) Env.ClientConfig.HostInfo.HostMemo.Remove(key);
			}
			else
			{
				if (Env.ClientConfig.HostInfo.HostMemo.ContainsKey(key)) Env.ClientConfig.HostInfo.HostMemo[key] = txtMemo.Text;
				else Env.ClientConfig.HostInfo.HostMemo.Add(key, txtMemo.Text);
			}

			if (string.IsNullOrEmpty(cbGroup.Text))
			{
				if (Env.ClientConfig.HostGroupConfig.ContainsKey(key)) Env.ClientConfig.HostGroupConfig.Remove(key);
			}
			else
			{
				if (Env.ClientConfig.HostGroupConfig.ContainsKey(key)) Env.ClientConfig.HostGroupConfig[key] = cbGroup.Text;
				else { Env.ClientConfig.HostGroupConfig.Add(key, cbGroup.Text); }
				if (!Cache.ControlCache.UserGroupList.Contains(cbGroup.Text)) Cache.ControlCache.UserGroupList.Add(cbGroup.Text);
			}

			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
