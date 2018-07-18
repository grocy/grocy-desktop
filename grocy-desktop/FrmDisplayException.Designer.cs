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
			this.textBox_Name = new System.Windows.Forms.TextBox();
			this.label_Name = new System.Windows.Forms.Label();
			this.label_Message = new System.Windows.Forms.Label();
			this.textBox_Message = new System.Windows.Forms.TextBox();
			this.label_Stacktrace = new System.Windows.Forms.Label();
			this.textBox_Stacktrace = new System.Windows.Forms.TextBox();
			this.button_ShowInnerException = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox_Name
			// 
			this.textBox_Name.Location = new System.Drawing.Point(12, 25);
			this.textBox_Name.Name = "textBox_Name";
			this.textBox_Name.ReadOnly = true;
			this.textBox_Name.Size = new System.Drawing.Size(776, 20);
			this.textBox_Name.TabIndex = 0;
			// 
			// label_Name
			// 
			this.label_Name.AutoSize = true;
			this.label_Name.Location = new System.Drawing.Point(9, 9);
			this.label_Name.Name = "label_Name";
			this.label_Name.Size = new System.Drawing.Size(85, 13);
			this.label_Name.TabIndex = 1;
			this.label_Name.Text = "Exception Name";
			// 
			// label_Message
			// 
			this.label_Message.AutoSize = true;
			this.label_Message.Location = new System.Drawing.Point(9, 59);
			this.label_Message.Name = "label_Message";
			this.label_Message.Size = new System.Drawing.Size(100, 13);
			this.label_Message.TabIndex = 2;
			this.label_Message.Text = "Exception Message";
			// 
			// textBox_Message
			// 
			this.textBox_Message.Location = new System.Drawing.Point(12, 75);
			this.textBox_Message.Multiline = true;
			this.textBox_Message.Name = "textBox_Message";
			this.textBox_Message.ReadOnly = true;
			this.textBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Message.Size = new System.Drawing.Size(776, 93);
			this.textBox_Message.TabIndex = 3;
			// 
			// label_Stacktrace
			// 
			this.label_Stacktrace.AutoSize = true;
			this.label_Stacktrace.Location = new System.Drawing.Point(9, 182);
			this.label_Stacktrace.Name = "label_Stacktrace";
			this.label_Stacktrace.Size = new System.Drawing.Size(109, 13);
			this.label_Stacktrace.TabIndex = 4;
			this.label_Stacktrace.Text = "Exception Stacktrace";
			// 
			// textBox_Stacktrace
			// 
			this.textBox_Stacktrace.Location = new System.Drawing.Point(12, 198);
			this.textBox_Stacktrace.Multiline = true;
			this.textBox_Stacktrace.Name = "textBox_Stacktrace";
			this.textBox_Stacktrace.ReadOnly = true;
			this.textBox_Stacktrace.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox_Stacktrace.Size = new System.Drawing.Size(776, 240);
			this.textBox_Stacktrace.TabIndex = 5;
			// 
			// button_ShowInnerException
			// 
			this.button_ShowInnerException.Location = new System.Drawing.Point(12, 444);
			this.button_ShowInnerException.Name = "button_ShowInnerException";
			this.button_ShowInnerException.Size = new System.Drawing.Size(141, 23);
			this.button_ShowInnerException.TabIndex = 6;
			this.button_ShowInnerException.Text = "Show inner exception";
			this.button_ShowInnerException.UseVisualStyleBackColor = true;
			this.button_ShowInnerException.Click += new System.EventHandler(this.button_ShowInnerException_Click);
			// 
			// FrmDisplayException
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 476);
			this.Controls.Add(this.button_ShowInnerException);
			this.Controls.Add(this.textBox_Stacktrace);
			this.Controls.Add(this.label_Stacktrace);
			this.Controls.Add(this.textBox_Message);
			this.Controls.Add(this.label_Message);
			this.Controls.Add(this.label_Name);
			this.Controls.Add(this.textBox_Name);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmDisplayException";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Unhandled error occurred";
			this.Load += new System.EventHandler(this.FrmDisplayException_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_Name;
		private System.Windows.Forms.Label label_Name;
		private System.Windows.Forms.Label label_Message;
		private System.Windows.Forms.TextBox textBox_Message;
		private System.Windows.Forms.Label label_Stacktrace;
		private System.Windows.Forms.TextBox textBox_Stacktrace;
		private System.Windows.Forms.Button button_ShowInnerException;
	}
}