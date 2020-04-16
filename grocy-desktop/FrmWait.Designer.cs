namespace GrocyDesktop
{
	partial class FrmWait
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
			this.ProgressBar_Status = new System.Windows.Forms.ProgressBar();
			this.Label_Status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ProgressBar_Status
			// 
			this.ProgressBar_Status.Location = new System.Drawing.Point(15, 28);
			this.ProgressBar_Status.Name = "ProgressBar_Status";
			this.ProgressBar_Status.Size = new System.Drawing.Size(412, 23);
			this.ProgressBar_Status.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.ProgressBar_Status.TabIndex = 0;
			// 
			// Label_Status
			// 
			this.Label_Status.AutoSize = true;
			this.Label_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label_Status.Location = new System.Drawing.Point(12, 9);
			this.Label_Status.Name = "Label_Status";
			this.Label_Status.Size = new System.Drawing.Size(98, 16);
			this.Label_Status.TabIndex = 1;
			this.Label_Status.Text = "Label_Status";
			// 
			// FrmWait
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 67);
			this.Controls.Add(this.Label_Status);
			this.Controls.Add(this.ProgressBar_Status);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmWait";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmWait";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar ProgressBar_Status;
		private System.Windows.Forms.Label Label_Status;
	}
}