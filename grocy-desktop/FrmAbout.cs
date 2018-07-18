using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmAbout : Form
	{
		public FrmAbout()
		{
			InitializeComponent();
		}

		private void linkLabel_MainProjectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(this.linkLabel_MainProjectLink.Text);
		}

		private void FrmAbout_Load(object sender, System.EventArgs e)
		{
			this.label_Version.Text = this.label_Version.Text.Replace("xxxx", FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion);
		}
	}
}
