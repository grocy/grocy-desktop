namespace GrocyDesktop
{
	partial class FrmShowText
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowText));
			this.textBox_Text = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox_Text
			// 
			this.textBox_Text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox_Text.Location = new System.Drawing.Point(12, 12);
			this.textBox_Text.Multiline = true;
			this.textBox_Text.Name = "textBox_Text";
			this.textBox_Text.ReadOnly = true;
			this.textBox_Text.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Text.Size = new System.Drawing.Size(776, 426);
			this.textBox_Text.TabIndex = 0;
			// 
			// FrmShowText
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.textBox_Text);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmShowText";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmShowText";
			this.Load += new System.EventHandler(this.FrmShowText_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_Text;
	}
}