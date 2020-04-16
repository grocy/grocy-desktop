using System;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmShowText : Form
	{
		public FrmShowText(string title, string text)
		{
			InitializeComponent();

			this.Text = title;
			this.TextBox_Text.Text = text;
		}

		private void FrmShowText_Load(object sender, EventArgs e)
		{
			this.TextBox_Text.SelectionStart = this.TextBox_Text.TextLength;
			this.TextBox_Text.ScrollToCaret();
		}
	}
}
