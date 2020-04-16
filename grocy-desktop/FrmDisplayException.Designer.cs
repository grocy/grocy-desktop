namespace GrocyDesktop
{
	partial class FrmDisplayException
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
			this.TextBox_Name = new System.Windows.Forms.TextBox();
			this.Label_Name = new System.Windows.Forms.Label();
			this.Label_Message = new System.Windows.Forms.Label();
			this.LextBox_Message = new System.Windows.Forms.TextBox();
			this.Label_Stacktrace = new System.Windows.Forms.Label();
			this.TextBox_Stacktrace = new System.Windows.Forms.TextBox();
			this.Button_ShowInnerException = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TextBox_Name
			// 
			this.TextBox_Name.Location = new System.Drawing.Point(12, 25);
			this.TextBox_Name.Name = "TextBox_Name";
			this.TextBox_Name.ReadOnly = true;
			this.TextBox_Name.Size = new System.Drawing.Size(776, 20);
			this.TextBox_Name.TabIndex = 0;
			// 
			// Label_Name
			// 
			this.Label_Name.AutoSize = true;
			this.Label_Name.Location = new System.Drawing.Point(9, 9);
			this.Label_Name.Name = "Label_Name";
			this.Label_Name.Size = new System.Drawing.Size(85, 13);
			this.Label_Name.TabIndex = 1;
			this.Label_Name.Text = "Exception Name";
			// 
			// Label_Message
			// 
			this.Label_Message.AutoSize = true;
			this.Label_Message.Location = new System.Drawing.Point(9, 59);
			this.Label_Message.Name = "Label_Message";
			this.Label_Message.Size = new System.Drawing.Size(100, 13);
			this.Label_Message.TabIndex = 2;
			this.Label_Message.Text = "Exception Message";
			// 
			// LextBox_Message
			// 
			this.LextBox_Message.Location = new System.Drawing.Point(12, 75);
			this.LextBox_Message.Multiline = true;
			this.LextBox_Message.Name = "LextBox_Message";
			this.LextBox_Message.ReadOnly = true;
			this.LextBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.LextBox_Message.Size = new System.Drawing.Size(776, 93);
			this.LextBox_Message.TabIndex = 3;
			// 
			// Label_Stacktrace
			// 
			this.Label_Stacktrace.AutoSize = true;
			this.Label_Stacktrace.Location = new System.Drawing.Point(9, 182);
			this.Label_Stacktrace.Name = "Label_Stacktrace";
			this.Label_Stacktrace.Size = new System.Drawing.Size(109, 13);
			this.Label_Stacktrace.TabIndex = 4;
			this.Label_Stacktrace.Text = "Exception Stacktrace";
			// 
			// TextBox_Stacktrace
			// 
			this.TextBox_Stacktrace.Location = new System.Drawing.Point(12, 198);
			this.TextBox_Stacktrace.Multiline = true;
			this.TextBox_Stacktrace.Name = "TextBox_Stacktrace";
			this.TextBox_Stacktrace.ReadOnly = true;
			this.TextBox_Stacktrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TextBox_Stacktrace.Size = new System.Drawing.Size(776, 240);
			this.TextBox_Stacktrace.TabIndex = 5;
			// 
			// Button_ShowInnerException
			// 
			this.Button_ShowInnerException.Location = new System.Drawing.Point(12, 444);
			this.Button_ShowInnerException.Name = "Button_ShowInnerException";
			this.Button_ShowInnerException.Size = new System.Drawing.Size(141, 23);
			this.Button_ShowInnerException.TabIndex = 6;
			this.Button_ShowInnerException.Text = "Show inner exception";
			this.Button_ShowInnerException.UseVisualStyleBackColor = true;
			this.Button_ShowInnerException.Click += new System.EventHandler(this.button_ShowInnerException_Click);
			// 
			// FrmDisplayException
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 476);
			this.Controls.Add(this.Button_ShowInnerException);
			this.Controls.Add(this.TextBox_Stacktrace);
			this.Controls.Add(this.Label_Stacktrace);
			this.Controls.Add(this.LextBox_Message);
			this.Controls.Add(this.Label_Message);
			this.Controls.Add(this.Label_Name);
			this.Controls.Add(this.TextBox_Name);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmDisplayException";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Unhandled error occurred";
			this.Load += new System.EventHandler(this.FrmDisplayException_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBox_Name;
		private System.Windows.Forms.Label Label_Name;
		private System.Windows.Forms.Label Label_Message;
		private System.Windows.Forms.TextBox LextBox_Message;
		private System.Windows.Forms.Label Label_Stacktrace;
		private System.Windows.Forms.TextBox TextBox_Stacktrace;
		private System.Windows.Forms.Button Button_ShowInnerException;
	}
}