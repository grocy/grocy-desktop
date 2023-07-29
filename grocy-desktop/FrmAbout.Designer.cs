namespace GrocyDesktop
{
	partial class FrmAbout
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
            this.Label_Headline = new System.Windows.Forms.Label();
            this.Label_Footer = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.Label_SayThanksQuestions = new System.Windows.Forms.Label();
            this.LinkLabel_SayThanks = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // Label_Headline
            // 
            this.Label_Headline.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Headline.Location = new System.Drawing.Point(12, 9);
            this.Label_Headline.Name = "Label_Headline";
            this.Label_Headline.Size = new System.Drawing.Size(242, 43);
            this.Label_Headline.TabIndex = 0;
            this.Label_Headline.Text = "Grocy Desktop";
            this.Label_Headline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Footer
            // 
            this.Label_Footer.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Label_Footer.Location = new System.Drawing.Point(13, 141);
            this.Label_Footer.Name = "Label_Footer";
            this.Label_Footer.Size = new System.Drawing.Size(241, 45);
            this.Label_Footer.TabIndex = 1;
            this.Label_Footer.Text = "Grocy Desktop is project by Bernd Bestel\r\nCreated with passion since 2018";
            this.Label_Footer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_Version
            // 
            this.Label_Version.Location = new System.Drawing.Point(13, 52);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(241, 23);
            this.Label_Version.TabIndex = 4;
            this.Label_Version.Text = "Version xxxx";
            this.Label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_SayThanksQuestions
            // 
            this.Label_SayThanksQuestions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_SayThanksQuestions.Location = new System.Drawing.Point(13, 85);
            this.Label_SayThanksQuestions.Name = "Label_SayThanksQuestions";
            this.Label_SayThanksQuestions.Size = new System.Drawing.Size(241, 23);
            this.Label_SayThanksQuestions.TabIndex = 5;
            this.Label_SayThanksQuestions.Text = " Do you find Grocy useful?";
            this.Label_SayThanksQuestions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LinkLabel_SayThanks
            // 
            this.LinkLabel_SayThanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LinkLabel_SayThanks.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.LinkLabel_SayThanks.Location = new System.Drawing.Point(13, 108);
            this.LinkLabel_SayThanks.Name = "LinkLabel_SayThanks";
            this.LinkLabel_SayThanks.Size = new System.Drawing.Size(241, 20);
            this.LinkLabel_SayThanks.TabIndex = 6;
            this.LinkLabel_SayThanks.TabStop = true;
            this.LinkLabel_SayThanks.Text = "Say thanks";
            this.LinkLabel_SayThanks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LinkLabel_SayThanks.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_SayThanks_LinkClicked);
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 195);
            this.Controls.Add(this.LinkLabel_SayThanks);
            this.Controls.Add(this.Label_SayThanksQuestions);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.Label_Footer);
            this.Controls.Add(this.Label_Headline);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Grocy Desktop";
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label Label_Headline;
		private System.Windows.Forms.Label Label_Footer;
		private System.Windows.Forms.Label Label_Version;
		private System.Windows.Forms.Label Label_SayThanksQuestions;
		private System.Windows.Forms.LinkLabel LinkLabel_SayThanks;
	}
}