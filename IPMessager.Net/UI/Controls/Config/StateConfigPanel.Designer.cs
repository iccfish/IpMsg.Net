using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls.Config
{
	partial class StateConfigPanel
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.horizontalLine1 = new HorizontalLine();
			this.horizontalLine2 = new HorizontalLine();
			this.txtAbsenceSuffix = new IPMessagerNet.UI.EditorControls.TextBoxEditor();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.txtAbsenceAutoReply = new IPMessagerNet.UI.EditorControls.TextBoxEditor();
			this.txtAutoReplyMessage = new IPMessagerNet.UI.EditorControls.TextBoxEditor();
			this.chkAutoReply = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.ckAutoReply = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.lstLeaveMessage = new System.Windows.Forms.ListBox();
			this.lnkLMAdd = new System.Windows.Forms.LinkLabel();
			this.lnkLMRemove = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// horizontalLine1
			// 
			this.horizontalLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine1.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine1.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.horizontalLine1.LineColor = System.Drawing.SystemColors.WindowFrame;
			this.horizontalLine1.Location = new System.Drawing.Point(0, 3);
			this.horizontalLine1.Name = "horizontalLine1";
			this.horizontalLine1.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine1.Size = new System.Drawing.Size(547, 19);
			this.horizontalLine1.TabIndex = 0;
			this.horizontalLine1.Text = "状态功能设置";
			this.horizontalLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.horizontalLine1.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// horizontalLine2
			// 
			this.horizontalLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine2.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine2.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine2.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.horizontalLine2.LineColor = System.Drawing.SystemColors.WindowFrame;
			this.horizontalLine2.Location = new System.Drawing.Point(0, 180);
			this.horizontalLine2.Name = "horizontalLine2";
			this.horizontalLine2.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine2.Size = new System.Drawing.Size(242, 19);
			this.horizontalLine2.TabIndex = 0;
			this.horizontalLine2.Text = "离开信息设置";
			this.horizontalLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine2.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// txtAbsenceSuffix
			// 
			this.txtAbsenceSuffix.AllowBlank = false;
			this.txtAbsenceSuffix.DataInstance = null;
			this.txtAbsenceSuffix.DataMemberName = "AbsenceSuffix";
			this.txtAbsenceSuffix.Location = new System.Drawing.Point(108, 41);
			this.txtAbsenceSuffix.Name = "txtAbsenceSuffix";
			this.txtAbsenceSuffix.Size = new System.Drawing.Size(134, 21);
			this.txtAbsenceSuffix.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtAbsenceSuffix, "当您处于离开状态时，这个信息将会附加在您的昵称后面");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "离开状态后缀";
			// 
			// txtAbsenceAutoReply
			// 
			this.txtAbsenceAutoReply.AllowBlank = false;
			this.txtAbsenceAutoReply.DataInstance = null;
			this.txtAbsenceAutoReply.DataMemberName = "AbsenceMessage";
			this.txtAbsenceAutoReply.Location = new System.Drawing.Point(108, 77);
			this.txtAbsenceAutoReply.Name = "txtAbsenceAutoReply";
			this.txtAbsenceAutoReply.Size = new System.Drawing.Size(439, 21);
			this.txtAbsenceAutoReply.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtAbsenceAutoReply, "当处于离开模式时，如果收到了信息，将自动回复此消息");
			// 
			// txtAutoReplyMessage
			// 
			this.txtAutoReplyMessage.AllowBlank = false;
			this.txtAutoReplyMessage.DataInstance = null;
			this.txtAutoReplyMessage.DataMemberName = "AutoReplyMessage";
			this.txtAutoReplyMessage.Location = new System.Drawing.Point(108, 109);
			this.txtAutoReplyMessage.Name = "txtAutoReplyMessage";
			this.txtAutoReplyMessage.Size = new System.Drawing.Size(439, 21);
			this.txtAutoReplyMessage.TabIndex = 1;
			this.toolTip1.SetToolTip(this.txtAutoReplyMessage, "如果设置，收到任何消息时都会自动回复此消息（不建议设置）");
			// 
			// chkAutoReply
			// 
			this.chkAutoReply.AutoSize = true;
			this.chkAutoReply.DataInstance = null;
			this.chkAutoReply.DataMemberName = "AutoReplyWhenAbsence";
			this.chkAutoReply.Location = new System.Drawing.Point(5, 81);
			this.chkAutoReply.Name = "chkAutoReply";
			this.chkAutoReply.Size = new System.Drawing.Size(96, 16);
			this.chkAutoReply.TabIndex = 3;
			this.chkAutoReply.Text = "离开自动回复";
			this.chkAutoReply.UseVisualStyleBackColor = true;
			// 
			// ckAutoReply
			// 
			this.ckAutoReply.AutoSize = true;
			this.ckAutoReply.DataInstance = null;
			this.ckAutoReply.DataMemberName = "AutoReply";
			this.ckAutoReply.Location = new System.Drawing.Point(5, 113);
			this.ckAutoReply.Name = "ckAutoReply";
			this.ckAutoReply.Size = new System.Drawing.Size(96, 16);
			this.ckAutoReply.TabIndex = 3;
			this.ckAutoReply.Text = "自动回复信息";
			this.ckAutoReply.UseVisualStyleBackColor = true;
			// 
			// lstLeaveMessage
			// 
			this.lstLeaveMessage.FormattingEnabled = true;
			this.lstLeaveMessage.ItemHeight = 12;
			this.lstLeaveMessage.Location = new System.Drawing.Point(5, 205);
			this.lstLeaveMessage.Name = "lstLeaveMessage";
			this.lstLeaveMessage.Size = new System.Drawing.Size(237, 148);
			this.lstLeaveMessage.TabIndex = 4;
			// 
			// lnkLMAdd
			// 
			this.lnkLMAdd.AutoSize = true;
			this.lnkLMAdd.Location = new System.Drawing.Point(154, 356);
			this.lnkLMAdd.Name = "lnkLMAdd";
			this.lnkLMAdd.Size = new System.Drawing.Size(41, 12);
			this.lnkLMAdd.TabIndex = 5;
			this.lnkLMAdd.TabStop = true;
			this.lnkLMAdd.Text = "+ 添加";
			// 
			// lnkLMRemove
			// 
			this.lnkLMRemove.AutoSize = true;
			this.lnkLMRemove.Location = new System.Drawing.Point(201, 356);
			this.lnkLMRemove.Name = "lnkLMRemove";
			this.lnkLMRemove.Size = new System.Drawing.Size(41, 12);
			this.lnkLMRemove.TabIndex = 5;
			this.lnkLMRemove.TabStop = true;
			this.lnkLMRemove.Text = "- 删除";
			// 
			// StateConfigPanel
			// 
			this.Controls.Add(this.lnkLMRemove);
			this.Controls.Add(this.lnkLMAdd);
			this.Controls.Add(this.lstLeaveMessage);
			this.Controls.Add(this.ckAutoReply);
			this.Controls.Add(this.chkAutoReply);
			this.Controls.Add(this.txtAutoReplyMessage);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtAbsenceAutoReply);
			this.Controls.Add(this.txtAbsenceSuffix);
			this.Controls.Add(this.horizontalLine2);
			this.Controls.Add(this.horizontalLine1);
			this.Name = "StateConfigPanel";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private HorizontalLine horizontalLine1;
		private HorizontalLine horizontalLine2;
		private IPMessagerNet.UI.EditorControls.TextBoxEditor txtAbsenceSuffix;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label1;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkAutoReply;
		private IPMessagerNet.UI.EditorControls.TextBoxEditor txtAbsenceAutoReply;
		private IPMessagerNet.UI.EditorControls.TextBoxEditor txtAutoReplyMessage;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor ckAutoReply;
		private System.Windows.Forms.ListBox lstLeaveMessage;
		private System.Windows.Forms.LinkLabel lnkLMAdd;
		private System.Windows.Forms.LinkLabel lnkLMRemove;
	}
}
