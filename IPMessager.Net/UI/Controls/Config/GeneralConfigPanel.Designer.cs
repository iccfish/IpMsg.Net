using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls.Config
{
	partial class GeneralConfigPanel
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.horizontalLine1 = new HorizontalLine();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtNickName = new IPMessagerNet.UI.EditorControls.TextBoxEditor();
			this.txtGroupName = new IPMessagerNet.UI.EditorControls.TextBoxEditor();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// horizontalLine1
			// 
			this.horizontalLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine1.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine1.LineColor = System.Drawing.SystemColors.WindowFrame;
			this.horizontalLine1.Location = new System.Drawing.Point(3, 3);
			this.horizontalLine1.Name = "horizontalLine1";
			this.horizontalLine1.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine1.Size = new System.Drawing.Size(544, 16);
			this.horizontalLine1.TabIndex = 0;
			this.horizontalLine1.Text = "个人资料设置";
			this.horizontalLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.horizontalLine1.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "昵称：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(261, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "主机组：";
			// 
			// txtNickName
			// 
			this.txtNickName.AllowBlank = false;
			this.txtNickName.DataInstance = null;
			this.txtNickName.DataMemberName = "NickName";
			this.txtNickName.Location = new System.Drawing.Point(59, 32);
			this.txtNickName.Name = "txtNickName";
			this.txtNickName.Size = new System.Drawing.Size(155, 21);
			this.txtNickName.TabIndex = 0;
			// 
			// txtGroupName
			// 
			this.txtGroupName.AllowBlank = false;
			this.txtGroupName.DataInstance = null;
			this.txtGroupName.DataMemberName = "GroupName";
			this.txtGroupName.Location = new System.Drawing.Point(320, 32);
			this.txtGroupName.Name = "txtGroupName";
			this.txtGroupName.Size = new System.Drawing.Size(204, 21);
			this.txtGroupName.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(57, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(353, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "（留空将默认使用您当前登录系统的用户名和电脑设置的主机名）";
			// 
			// GeneralConfigPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtGroupName);
			this.Controls.Add(this.txtNickName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.horizontalLine1);
			this.Name = "GeneralConfigPanel";
			this.Load += new System.EventHandler(this.GeneralConfigPanel_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private HorizontalLine horizontalLine1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private IPMessagerNet.UI.EditorControls.TextBoxEditor txtNickName;
		private IPMessagerNet.UI.EditorControls.TextBoxEditor txtGroupName;
		private System.Windows.Forms.Label label3;
	}
}
