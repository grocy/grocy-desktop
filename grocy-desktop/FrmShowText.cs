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
			this.textBox_Text.Text = text;
		}

		private void FrmShowText_Load(object sender, EventArgs e)
		{
			this.textBox_Text.SelectionStart = this.textBox_Text.TextLength;
			this.textBox_Text.ScrollToCaret();
		}
	}
}
