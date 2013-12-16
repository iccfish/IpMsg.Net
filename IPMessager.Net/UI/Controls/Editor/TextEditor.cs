using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPMessagerNet.API;

namespace IPMessagerNet.UI.Controls.Editor
{
	public partial class TextEditor : UserControl, IEditorService
	{
		public TextEditor()
		{
			InitializeComponent();

			if (Env.ClientConfig != null)
			{
				this.Dock = DockStyle.Fill;
				InitEvents();
			}
		}

		#region 内部信息



		#endregion

		#region IEditorService 成员

		public event EventHandler TextMessageSendRequired;

		protected virtual void OnTextMessageSendRequired(EventArgs e)
		{
			if (TextMessageSendRequired != null) TextMessageSendRequired(this, e);
		}

		public bool IsHtml
		{
			get { return false; }
		}

		public bool IsRTF
		{
			get { return false; }
		}



		public string Content
		{
			get
			{
				return txtContent.Text.TrimEnd('\n');
			}
			set
			{
				txtContent.Text = value;
			}
		}

		#endregion

		#region 内部事件

		void InitEvents()
		{
			chkQuickPost.Checked = Env.ChatConfig.EnableCtrlEnterShortKey;
			chkQuickPost.CheckedChanged += (s, e) => { Env.ChatConfig.EnableCtrlEnterShortKey = chkQuickPost.Checked; };

			txtContent.KeyDown += txtContent_KeyUp;
			txtContent.KeyPress += txtContent_KeyPress;
		}

		bool lastKeyHandled = false;

		void txtContent_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (lastKeyHandled)
			{
				e.Handled = true;
				lastKeyHandled = false;
				txtContent.Text = "";
			}
		}

		void txtContent_KeyUp(object sender, KeyEventArgs e)
		{
			if (!chkQuickPost.Checked || string.IsNullOrEmpty(txtContent.Text) || !e.Control || e.KeyCode != Keys.Enter) return;
			else
			{
				e.Handled = true;
				lastKeyHandled = true;
				OnTextMessageSendRequired(new EventArgs());
			}
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtContent.Text))
			{
				OnTextMessageSendRequired(new EventArgs());
				txtContent.Text = "";
			}
		}

		#endregion
	}
}
