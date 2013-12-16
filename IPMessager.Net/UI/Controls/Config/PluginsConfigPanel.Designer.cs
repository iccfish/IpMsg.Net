namespace IPMessagerNet.UI.Controls.Config
{
	partial class PluginsConfigPanel
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
			this.label1 = new System.Windows.Forms.Label();
			this.pluginsList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.状态 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imgList = new System.Windows.Forms.ImageList();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.btnRescan = new System.Windows.Forms.Button();
			this.btnConfig = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(529, 66);
			this.label1.TabIndex = 0;
			this.label1.Text = "插件是在飞鸽传书.Net基础上提供额外功能（如安全、功能、增强协议）等应用组件，可能是官方或第三方发布。在这里可以设置是否启用它们。\r\n\r\n要启用指定的插件，请勾" +
				"中前面的复选框，否则，请取消勾选。改变插件的启用状态需要重新启动飞鸽传书.Net。";
			// 
			// pluginsList
			// 
			this.pluginsList.CheckBoxes = true;
			this.pluginsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.状态,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.pluginsList.FullRowSelect = true;
			this.pluginsList.Location = new System.Drawing.Point(11, 96);
			this.pluginsList.Name = "pluginsList";
			this.pluginsList.Size = new System.Drawing.Size(527, 198);
			this.pluginsList.SmallImageList = this.imgList;
			this.pluginsList.TabIndex = 1;
			this.pluginsList.UseCompatibleStateImageBehavior = false;
			this.pluginsList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 150;
			// 
			// 状态
			// 
			this.状态.Text = "状态";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "版本";
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "作者";
			this.columnHeader3.Width = 100;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "程序集";
			this.columnHeader4.Width = 150;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "加载类名";
			this.columnHeader5.Width = 200;
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgList.ImageSize = new System.Drawing.Size(16, 16);
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// txtDesc
			// 
			this.txtDesc.Location = new System.Drawing.Point(11, 300);
			this.txtDesc.Multiline = true;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.ReadOnly = true;
			this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtDesc.Size = new System.Drawing.Size(432, 68);
			this.txtDesc.TabIndex = 2;
			this.txtDesc.Text = "选中查看对应插件的详细信息。";
			// 
			// btnRescan
			// 
			this.btnRescan.Location = new System.Drawing.Point(449, 335);
			this.btnRescan.Name = "btnRescan";
			this.btnRescan.Size = new System.Drawing.Size(88, 28);
			this.btnRescan.TabIndex = 3;
			this.btnRescan.Text = "重新扫描";
			this.btnRescan.UseVisualStyleBackColor = true;
			// 
			// btnConfig
			// 
			this.btnConfig.Enabled = false;
			this.btnConfig.Location = new System.Drawing.Point(449, 304);
			this.btnConfig.Name = "btnConfig";
			this.btnConfig.Size = new System.Drawing.Size(88, 28);
			this.btnConfig.TabIndex = 4;
			this.btnConfig.Text = "设置...(&S)";
			this.btnConfig.UseVisualStyleBackColor = true;
			// 
			// PluginsConfigPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnConfig);
			this.Controls.Add(this.btnRescan);
			this.Controls.Add(this.txtDesc);
			this.Controls.Add(this.pluginsList);
			this.Controls.Add(this.label1);
			this.Name = "PluginsConfigPanel";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView pluginsList;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.Button btnRescan;
		private System.Windows.Forms.ColumnHeader 状态;
		private System.Windows.Forms.Button btnConfig;
	}
}
