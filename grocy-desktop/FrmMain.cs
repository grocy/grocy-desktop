using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
		}

		private ChromiumWebBrowser Browser;
		private PhpDevelopmentServerManager PhpServer;
		private GrocyEnvironmentManager GrocyEnvironmentManager;

		private void SetupCef()
		{
			Cef.EnableHighDPISupport();

			CefSettings cefSettings = new CefSettings();
			cefSettings.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";
			Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);

			this.Browser = new ChromiumWebBrowser(this.PhpServer.Url);
			this.Browser.Dock = DockStyle.Fill;
			this.panel_Main.Controls.Add(this.Browser);
		}

		private void SetupPhpServer()
		{
			this.PhpServer = new PhpDevelopmentServerManager(Path.Combine(Program.BaseExecutingPath, "php"), Path.Combine(Program.BaseExecutingPath, @"grocy\public"));
			this.PhpServer.StartServer();
		}

		private void SetupGrocy()
		{
			this.GrocyEnvironmentManager = new GrocyEnvironmentManager(Path.Combine(Program.BaseExecutingPath, @"grocy"));
			this.GrocyEnvironmentManager.Setup(this.PhpServer.Url);
		}

		private async void FrmMain_Shown(object sender, EventArgs e)
		{
			await GrocyDesktopDependencyManager.UnpackIncludedDependenciesIfNeeded(this);
			this.SetupPhpServer();
			this.SetupGrocy();
			this.SetupCef();
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.PhpServer != null)
			{
				this.PhpServer.StopServer();
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void showPHPServerOutputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new FrmShowText("PHP server output", this.PhpServer.GetConsoleOutput()).ShowDialog(this);
		}

		private void showBrowserDeveloperToolsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Browser.ShowDevTools();
		}

		private void aboutGrocydesktopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new FrmAbout().ShowDialog(this);
		}

		private async void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.PhpServer.StopServer();
			await GrocyDesktopDependencyManager.UpdateEmbeddedGrocyRelease(this);
			this.PhpServer.StartServer();
			this.GrocyEnvironmentManager.Setup(this.PhpServer.Url);
			this.Browser.Load(this.PhpServer.Url);
		}
	}
}
