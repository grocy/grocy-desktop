using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmAbout : Form
	{
		public FrmAbout()
		{
			InitializeComponent();
		}

		private ResourceManager ResourceManager = new ResourceManager(typeof(FrmMain));

		private void FrmAbout_Load(object sender, System.EventArgs e)
		{
			this.Text = this.ResourceManager.GetString("ToolStripMenuItem_About.Text");
			this.Label_SayThanksQuestions.Text = this.ResourceManager.GetString("STRING_SayThanksQuestion.Text");
			this.LinkLabel_SayThanks.Text = this.ResourceManager.GetString("STRING_SayThanks.Text") + "❤";
			this.Label_Version.Text = this.Label_Version.Text.Replace("xxxx", Program.RunningVersion);
		}

		private void LinkLabel_SayThanks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://grocy.info/#say-thanks");
		}
	}
}
