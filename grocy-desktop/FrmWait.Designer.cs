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
			this.progressBar_Status = new System.Windows.Forms.ProgressBar();
			this.label_Status = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar_Status
			// 
			this.progressBar_Status.Location = new System.Drawing.Point(15, 28);
			this.progressBar_Status.Name = "progressBar_Status";
			this.progressBar_Status.Size = new System.Drawing.Size(381, 23);
			this.progressBar_Status.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar_Status.TabIndex = 0;
			// 
			// label_Status
			// 
			this.label_Status.AutoSize = true;
			this.label_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_Status.Location = new System.Drawing.Point(12, 9);
			this.label_Status.Name = "label_Status";
			this.label_Status.Size = new System.Drawing.Size(51, 16);
			this.label_Status.TabIndex = 1;
			this.label_Status.Text = "label1";
			// 
			// FrmWait
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(410, 67);
			this.Controls.Add(this.label_Status);
			this.Controls.Add(this.progressBar_Status);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "FrmWait";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmWait";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar_Status;
		private System.Windows.Forms.Label label_Status;
	}
}