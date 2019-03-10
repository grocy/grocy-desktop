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
			this.label_Headline = new System.Windows.Forms.Label();
			this.label_Text = new System.Windows.Forms.Label();
			this.label_MainProject = new System.Windows.Forms.Label();
			this.linkLabel_MainProjectLink = new System.Windows.Forms.LinkLabel();
			this.label_Version = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label_Headline
			// 
			this.label_Headline.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_Headline.Location = new System.Drawing.Point(12, 9);
			this.label_Headline.Name = "label_Headline";
			this.label_Headline.Size = new System.Drawing.Size(242, 43);
			this.label_Headline.TabIndex = 0;
			this.label_Headline.Text = "grocy-desktop";
			this.label_Headline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_Text
			// 
			this.label_Text.Location = new System.Drawing.Point(13, 52);
			this.label_Text.Name = "label_Text";
			this.label_Text.Size = new System.Drawing.Size(241, 58);
			this.label_Text.TabIndex = 1;
			this.label_Text.Text = "grocy-desktop is project by Bernd Bestel\r\nCreated with passion since 2018";
			this.label_Text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_MainProject
			// 
			this.label_MainProject.Location = new System.Drawing.Point(13, 110);
			this.label_MainProject.Name = "label_MainProject";
			this.label_MainProject.Size = new System.Drawing.Size(241, 23);
			this.label_MainProject.TabIndex = 2;
			this.label_MainProject.Text = "Main project";
			this.label_MainProject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// linkLabel_MainProjectLink
			// 
			this.linkLabel_MainProjectLink.Location = new System.Drawing.Point(12, 133);
			this.linkLabel_MainProjectLink.Name = "linkLabel_MainProjectLink";
			this.linkLabel_MainProjectLink.Size = new System.Drawing.Size(242, 17);
			this.linkLabel_MainProjectLink.TabIndex = 3;
			this.linkLabel_MainProjectLink.TabStop = true;
			this.linkLabel_MainProjectLink.Text = "https://grocy.info";
			this.linkLabel_MainProjectLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel_MainProjectLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_MainProjectLink_LinkClicked);
			// 
			// label_Version
			// 
			this.label_Version.Location = new System.Drawing.Point(13, 162);
			this.label_Version.Name = "label_Version";
			this.label_Version.Size = new System.Drawing.Size(241, 23);
			this.label_Version.TabIndex = 4;
			this.label_Version.Text = "Version xxxx";
			this.label_Version.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(266, 195);
			this.Controls.Add(this.label_Version);
			this.Controls.Add(this.linkLabel_MainProjectLink);
			this.Controls.Add(this.label_MainProject);
			this.Controls.Add(this.label_Text);
			this.Controls.Add(this.label_Headline);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmAbout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About grocy-desktop";
			this.Load += new System.EventHandler(this.FrmAbout_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label_Headline;
		private System.Windows.Forms.Label label_Text;
		private System.Windows.Forms.Label label_MainProject;
		private System.Windows.Forms.LinkLabel linkLabel_MainProjectLink;
		private System.Windows.Forms.Label label_Version;
	}
}