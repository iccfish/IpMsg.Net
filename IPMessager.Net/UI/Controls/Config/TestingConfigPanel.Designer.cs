namespace IPMessagerNet.UI.Controls.Config
{
	partial class TestingConfigPanel
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
			this.chkForceOldContract = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.label1 = new System.Windows.Forms.Label();
			this.warningImg = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.warningImg)).BeginInit();
			this.SuspendLayout();
			// 
			// chkForceOldContract
			// 
			this.chkForceOldContract.AutoSize = true;
			this.chkForceOldContract.DataInstance = null;
			this.chkForceOldContract.DataMemberName = "ForceOldContract";
			this.chkForceOldContract.Location = new System.Drawing.Point(3, 41);
			this.chkForceOldContract.Name = "chkForceOldContract";
			this.chkForceOldContract.Size = new System.Drawing.Size(186, 16);
			this.chkForceOldContract.TabIndex = 0;
			this.chkForceOldContract.Text = "强行使用兼容版本的IPMSG协议";
			this.chkForceOldContract.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20, 60);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(513, 32);
			this.label1.TabIndex = 1;
			this.label1.Text = "飞鸽传书.Net支持自有的新协议，支持压缩和大数据包分包发送且不影响与其它版本飞鸽传书的通讯，但是处于试验阶段不太稳定，如果希望完全不使用，选择此选项";
			// 
			// warningImg
			// 
			this.warningImg.Location = new System.Drawing.Point(3, 3);
			this.warningImg.Name = "warningImg";
			this.warningImg.Size = new System.Drawing.Size(369, 30);
			this.warningImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.warningImg.TabIndex = 2;
			this.warningImg.TabStop = false;
			// 
			// TestingConfigPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.warningImg);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkForceOldContract);
			this.Name = "TestingConfigPanel";
			((System.ComponentModel.ISupportInitialize)(this.warningImg)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkForceOldContract;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox warningImg;
	}
}
