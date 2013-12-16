namespace IPMessagerNet._Embed
{
	partial class WaitingDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitingDialog));
			this.lblOperation = new System.Windows.Forms.Label();
			this.pgUpdate = new System.Windows.Forms.ProgressBar();
			this.lblMessage = new System.Windows.Forms.Label();
			this.logList = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// lblOperation
			// 
			resources.ApplyResources(this.lblOperation, "lblOperation");
			this.lblOperation.Name = "lblOperation";
			// 
			// pgUpdate
			// 
			resources.ApplyResources(this.pgUpdate, "pgUpdate");
			this.pgUpdate.Name = "pgUpdate";
			// 
			// lblMessage
			// 
			resources.ApplyResources(this.lblMessage, "lblMessage");
			this.lblMessage.Name = "lblMessage";
			// 
			// logList
			// 
			this.logList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.logList.FullRowSelect = true;
			this.logList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			resources.ApplyResources(this.logList, "logList");
			this.logList.Name = "logList";
			this.logList.UseCompatibleStateImageBehavior = false;
			this.logList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			resources.ApplyResources(this.columnHeader1, "columnHeader1");
			// 
			// columnHeader2
			// 
			resources.ApplyResources(this.columnHeader2, "columnHeader2");
			// 
			// WaitingDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.logList);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.pgUpdate);
			this.Controls.Add(this.lblOperation);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WaitingDialog";
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblOperation;
		private System.Windows.Forms.ProgressBar pgUpdate;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.ListView logList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
	}
}