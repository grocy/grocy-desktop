using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;

namespace GrocyDesktop
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();
		}

		private ChromiumWebBrowser GrocyBrowser;
		private ChromiumWebBrowser BarcodeBuddyBrowser;
		private PhpDevelopmentServerManager GrocyPhpServer;
		private PhpDevelopmentServerManager BarcodeBuddyPhpServer;
		private GrocyEnvironmentManager GrocyEnvironmentManager;
		private BarcodeBuddyEnvironmentManager BarcodeBuddyEnvironmentManager;
		private UserSettings UserSettings = UserSettings.Load();

		private void SetupCef()
		{
			Cef.EnableHighDPISupport();

			CefSettings cefSettings = new CefSettings();
			cefSettings.BrowserSubprocessPath = Path.Combine(GrocyDesktopDependencyManager.CefExecutingPath, @"x86\CefSharp.BrowserSubprocess.exe");
			cefSettings.CachePath = GrocyDesktopDependencyManager.CefCachePath;
			cefSettings.LogFile = Path.Combine(GrocyDesktopDependencyManager.CefCachePath, "cef.log");
			cefSettings.CefCommandLineArgs.Add("--enable-media-stream", "");
			cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", this.GrocyPhpServer.Url);
			Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);

			this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyPhpServer.Url);
			this.GrocyBrowser.Dock = DockStyle.Fill;
			this.tabPage_grocy.Controls.Add(this.GrocyBrowser);

			this.BarcodeBuddyBrowser = new ChromiumWebBrowser(this.BarcodeBuddyPhpServer.Url);
			this.BarcodeBuddyBrowser.Dock = DockStyle.Fill;
			this.tabPage_BarcodeBuddy.Controls.Add(this.BarcodeBuddyBrowser);
		}

		private void SetupPhpServer()
		{
			this.GrocyPhpServer = new PhpDevelopmentServerManager(GrocyDesktopDependencyManager.PhpExecutingPath, Path.Combine(GrocyDesktopDependencyManager.GrocyExecutingPath, "public"));
			this.GrocyPhpServer.StartServer();

			this.BarcodeBuddyPhpServer = new PhpDevelopmentServerManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath);
			this.BarcodeBuddyPhpServer.StartServer();
		}

		private void SetupGrocy()
		{
			this.GrocyEnvironmentManager = new GrocyEnvironmentManager(GrocyDesktopDependencyManager.GrocyExecutingPath, this.UserSettings.GrocyDataLocation);
			this.GrocyEnvironmentManager.Setup(this.GrocyPhpServer.Url);
		}

		private void SetupBarcodeBuddy()
		{
			this.BarcodeBuddyEnvironmentManager = new BarcodeBuddyEnvironmentManager(GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath);
			this.BarcodeBuddyEnvironmentManager.Setup(this.GrocyPhpServer.Url.TrimEnd('/') + "/api");
		}

		private async void FrmMain_Shown(object sender, EventArgs e)
		{
			await GrocyDesktopDependencyManager.UnpackIncludedDependenciesIfNeeded(this);
			this.SetupPhpServer();
			this.SetupGrocy();
			this.SetupBarcodeBuddy();
			this.SetupCef();
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.GrocyPhpServer != null)
			{
				this.GrocyPhpServer.StopServer();
			}

			if (this.BarcodeBuddyPhpServer != null)
			{
				this.BarcodeBuddyPhpServer.StopServer();
			}

			this.UserSettings.Save();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void showPHPServerOutputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new FrmShowText("grocy PHP server output", this.GrocyPhpServer.GetConsoleOutput()).ShowDialog(this);
			new FrmShowText("BarcodeBuddy PHP server output", this.BarcodeBuddyPhpServer.GetConsoleOutput()).ShowDialog(this);
		}

		private void showBrowserDeveloperToolsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.GrocyBrowser.ShowDevTools();
			this.BarcodeBuddyBrowser.ShowDevTools();
		}

		private void aboutGrocydesktopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new FrmAbout().ShowDialog(this);
		}

		private async void updateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.GrocyPhpServer.StopServer();
			await GrocyDesktopDependencyManager.UpdateEmbeddedGrocyRelease(this);
			this.GrocyPhpServer.StartServer();
			this.GrocyEnvironmentManager.Setup(this.GrocyPhpServer.Url);
			this.GrocyBrowser.Load(this.GrocyPhpServer.Url);
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
						this.GrocyPhpServer.StopServer();
						Extensions.CopyFolder(this.UserSettings.GrocyDataLocation, dialog.SelectedPath);
						Directory.Delete(this.UserSettings.GrocyDataLocation, true);
						this.UserSettings.GrocyDataLocation = dialog.SelectedPath;
						this.UserSettings.Save();
						Extensions.RestartApp();
					}
				}
			}
		}

		private void backupDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
				dialog.Filter = "ZIP files|*.zip";
				dialog.CheckPathExists = true;
				dialog.DefaultExt = ".zip";
				dialog.FileName = "grocy-desktop-backup.zip";

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					ZipFile.CreateFromDirectory(this.UserSettings.GrocyDataLocation, dialog.FileName);
					MessageBox.Show("Backup successfully created.", "grocy-desktop Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void restoreDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
				dialog.Filter = "ZIP files|*.zip";
				dialog.CheckPathExists = true;
				dialog.CheckFileExists = true;
				dialog.DefaultExt = ".zip";

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show("The current data will be overwritten and grocy-desktop will restart, continue?", "Restore grocy data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.GrocyPhpServer.StopServer();
						Thread.Sleep(2000); // Just give php.exe some time to stop...
						Directory.Delete(this.UserSettings.GrocyDataLocation, true);
						Directory.CreateDirectory(this.UserSettings.GrocyDataLocation);
						ZipFile.ExtractToDirectory(dialog.FileName, this.UserSettings.GrocyDataLocation);
						Extensions.RestartApp();
					}
				}
			}
		}

		private void recreateDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("This will delete and recreate the grocy database, means all your data will be wiped, really continue?", "Recreate grocy database", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				this.GrocyPhpServer.StopServer();
				Thread.Sleep(2000); // Just give php.exe some time to stop...
				File.Delete(Path.Combine(this.UserSettings.GrocyDataLocation, "grocy.db"));
				Extensions.RestartApp();
			}
		}
	}
}
