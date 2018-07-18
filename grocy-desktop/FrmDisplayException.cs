using System;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmDisplayException : Form
	{
		public FrmDisplayException(Exception ex)
		{
			InitializeComponent();
			this.Exception = ex;
		}

		private Exception Exception;

		private void FrmDisplayException_Load(object sender, EventArgs e)
		{
			this.textBox_Name.Text = this.Exception.GetType().Name;
			this.textBox_Message.Text = this.Exception.Message;
			this.textBox_Stacktrace.Text = this.Exception.StackTrace;
			this.button_ShowInnerException.Enabled = this.Exception.InnerException != null;
		}

		private void button_ShowInnerException_Click(object sender, EventArgs e)
		{
			new FrmDisplayException(this.Exception.InnerException).Show();
		}
	}
}
