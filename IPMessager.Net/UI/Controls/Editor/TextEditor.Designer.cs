namespace IPMessagerNet.UI.Controls.Editor
{
	partial class TextEditor
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
			this.txtContent = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.chkQuickPost = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtContent
			// 
			this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.txtContent.Location = new System.Drawing.Point(3, 3);
			this.txtContent.Multiline = true;
			this.txtContent.Name = "txtContent";
			this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtContent.Size = new System.Drawing.Size(296, 114);
			this.txtContent.TabIndex = 0;
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Location = new System.Drawing.Point(227, 121);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(72, 26);
			this.btnSend.TabIndex = 1;
			this.btnSend.Text = "确定";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// chkQuickPost
			// 
			this.chkQuickPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkQuickPost.AutoSize = true;
			this.chkQuickPost.Location = new System.Drawing.Point(3, 127);
			this.chkQuickPost.Name = "chkQuickPost";
			this.chkQuickPost.Size = new System.Drawing.Size(144, 16);
			this.chkQuickPost.TabIndex = 2;
			this.chkQuickPost.Text = "按Ctrl+Enter快速发送";
			this.chkQuickPost.UseVisualStyleBackColor = true;
			// 
			// TextEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chkQuickPost);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.txtContent);
			this.Name = "TextEditor";
			this.Size = new System.Drawing.Size(302, 150);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtContent;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.CheckBox chkQuickPost;
	}
}
