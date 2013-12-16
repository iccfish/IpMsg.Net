namespace IPMessagerNet.UI.Forms
{
	partial class ControlContainer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlContainer));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tolTopMost = new System.Windows.Forms.ToolStripButton();
			this.container = new System.Windows.Forms.Panel();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolTopMost});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(298, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tolTopMost
			// 
			this.tolTopMost.CheckOnClick = true;
			this.tolTopMost.Image = ((System.Drawing.Image)(resources.GetObject("tolTopMost.Image")));
			this.tolTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tolTopMost.Name = "tolTopMost";
			this.tolTopMost.Size = new System.Drawing.Size(52, 22);
			this.tolTopMost.Text = "置顶";
			this.tolTopMost.CheckedChanged += new System.EventHandler(this.tolTopMost_CheckedChanged);
			// 
			// container
			// 
			this.container.Dock = System.Windows.Forms.DockStyle.Fill;
			this.container.Location = new System.Drawing.Point(0, 25);
			this.container.Name = "container";
			this.container.Size = new System.Drawing.Size(298, 363);
			this.container.TabIndex = 1;
			// 
			// ControlContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 388);
			this.Controls.Add(this.container);
			this.Controls.Add(this.toolStrip1);
			this.MaximizeBox = false;
			this.Name = "ControlContainer";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "在线列表 - 飞鸽传书.Net";
			this.Load += new System.EventHandler(this.ControlContainer_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tolTopMost;
		private System.Windows.Forms.Panel container;
	}
}