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
		private UserSettings UserSettings = UserSettings.Load();

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
			this.GrocyEnvironmentManager = new GrocyEnvironmentManager(Path.Combine(Program.BaseExecutingPath, @"grocy"), this.UserSettings.GrocyDataLocation);
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

			this.UserSettings.Save();
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

		private void configurechangeDataLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.RootFolder = Environment.SpecialFolder.Desktop;
				dialog.SelectedPath = this.UserSettings.GrocyDataLocation;
				
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show("grocy-desktop will restart to apply the changed settings, continue?", "Change grocy data location", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.PhpServer.StopServer();
						Extensions.CopyFolder(this.UserSettings.GrocyDataLocation, dialog.SelectedPath);
						Directory.Delete(this.UserSettings.GrocyDataLocation, true);
						this.UserSettings.GrocyDataLocation = dialog.SelectedPath;
						this.UserSettings.Save();
						Extensions.RestartApp();
					}
				}
			}
		}
	}
}
