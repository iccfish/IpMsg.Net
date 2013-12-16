using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Controls.Config
{
	public partial class StateConfigPanel : IPMessagerNet.API.ConfigPanelBase
	{
		public StateConfigPanel()
		{
			InitializeComponent();
		}

		//初始化
		protected override void InitEditor()
		{
			chkAutoReply.CheckedChanged += (s, e) => txtAbsenceAutoReply.Enabled = chkAutoReply.Checked;
			ckAutoReply.CheckedChanged += (s, e) => txtAutoReplyMessage.Enabled = ckAutoReply.Checked;

			chkAutoReply.DataInstance = ckAutoReply.DataInstance = txtAbsenceAutoReply.DataInstance = txtAbsenceSuffix.DataInstance = txtAutoReplyMessage.DataInstance = Env.IPMClient.Config;

			//离开状态信息
			lstLeaveMessage.Items.AddRange(Env.ClientConfig.AbsenceMessage.ToArray());
			lnkLMAdd.Click += lnkLMAdd_Click;
			lnkLMRemove.Click += (s, e) =>
			{
				if (lstLeaveMessage.SelectedIndex == -1) return;
				Env.ClientConfig.AbsenceMessage.Remove(lstLeaveMessage.SelectedItem.ToString());
				lstLeaveMessage.Items.RemoveAt(lstLeaveMessage.SelectedIndex);
			};
		}

		void lnkLMAdd_Click(object sender, EventArgs e)
		{
			var box = CreateInputBox("输入", "请输入新的离开状态消息，不能为空", false);

			if (box.ShowDialog() == DialogResult.OK)
			{
				string v = box.InputedText;
				if (Env.ClientConfig.AbsenceMessage.Contains(v)) Information("看起来输入重复了.....");
				else
				{
					Env.ClientConfig.AbsenceMessage.Add(v);
					lstLeaveMessage.Items.Add(v);
				}
			}
		}
	}
}
