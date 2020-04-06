using CefSharp;
using CefSharp.WinForms;
using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Resources;
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

		private ResourceManager ResourceManager = new ResourceManager(typeof(FrmMain));
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
			cefSettings.CefCommandLineArgs.Add("--lang", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
			Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyPhpServer.Url);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.TabPage_Grocy.Controls.Add(this.GrocyBrowser);

				this.BarcodeBuddyBrowser = new ChromiumWebBrowser(this.BarcodeBuddyPhpServer.Url);
				this.BarcodeBuddyBrowser.Dock = DockStyle.Fill;
				this.TabPage_BarcodeBuddy.Controls.Add(this.BarcodeBuddyBrowser);
			}
			else
			{
				this.TabControl_Main.Visible = false;
				this.ToolStripMenuItem_BarcodeBuddy.Visible = false;

				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyPhpServer.Url);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.Panel_Main.Controls.Add(this.GrocyBrowser);
			}
		}

		private void SetupGrocy()
		{
			this.GrocyPhpServer = new PhpDevelopmentServerManager(GrocyDesktopDependencyManager.PhpExecutingPath, Path.Combine(GrocyDesktopDependencyManager.GrocyExecutingPath, "public"));
			this.GrocyPhpServer.StartServer();
			this.GrocyEnvironmentManager = new GrocyEnvironmentManager(GrocyDesktopDependencyManager.GrocyExecutingPath, this.UserSettings.GrocyDataLocation);
			this.GrocyEnvironmentManager.Setup(this.GrocyPhpServer.Url);
		}

		private void SetupBarcodeBuddy()
		{
			this.BarcodeBuddyEnvironmentManager = new BarcodeBuddyEnvironmentManager(GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath, this.UserSettings.BarcodeBuddyDataLocation);
			this.BarcodeBuddyPhpServer = new PhpDevelopmentServerManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath);
			this.BarcodeBuddyEnvironmentManager.Setup(this.GrocyPhpServer.Url.TrimEnd('/') + "/api/");
			this.BarcodeBuddyPhpServer.SetEnvironmenVariables(this.BarcodeBuddyEnvironmentManager.GetEnvironmentVariables());
			this.BarcodeBuddyPhpServer.StartServer();
		}

		private async void FrmMain_Shown(object sender, EventArgs e)
		{
			await GrocyDesktopDependencyManager.UnpackIncludedDependenciesIfNeeded(this.UserSettings, this);
			this.SetupGrocy();
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.SetupBarcodeBuddy();
			}
			this.SetupCef();

			this.ToolStripMenuItem_EnableBarcodeBuddy.Checked = this.UserSettings.EnableBarcodeBuddyIntegration;
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.GrocyPhpServer != null)
			{
				this.GrocyPhpServer.StopServer();
			}

			if (this.UserSettings.EnableBarcodeBuddyIntegration && this.BarcodeBuddyPhpServer != null)
			{
				this.BarcodeBuddyPhpServer.StopServer();
			}

			this.UserSettings.Save();
		}

		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ToolStripMenuItem_ShowPhpServerOutput_Click(object sender, EventArgs e)
		{
			new FrmShowText("grocy " + this.ResourceManager.GetString("STRING_PHPServerOutput"), this.GrocyPhpServer.GetConsoleOutput()).Show(this);
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				new FrmShowText("Barcode Buddy " + this.ResourceManager.GetString("STRING_PHPServerOutput"), this.BarcodeBuddyPhpServer.GetConsoleOutput()).Show(this);
			}
		}

		private void ToolStripMenuItem_ShowBrowserDeveloperTools_Click(object sender, EventArgs e)
		{
			this.GrocyBrowser.ShowDevTools();

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.BarcodeBuddyBrowser.ShowDevTools();
			}
		}

		private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
		{
			new FrmAbout().ShowDialog(this);
		}

		private async void ToolStripMenuItem_UpdateGrocy_Click(object sender, EventArgs e)
		{
			this.GrocyPhpServer.StopServer();
			Thread.Sleep(2000); // Just give php.exe some time to stop...
			await GrocyDesktopDependencyManager.UpdateEmbeddedGrocyRelease(this);
			this.GrocyPhpServer.StartServer();
			this.GrocyEnvironmentManager.Setup(this.GrocyPhpServer.Url);
			this.GrocyBrowser.Load(this.GrocyPhpServer.Url);
		}

		private void ToolStripMenuItem_ConfigureChangeDataLocation_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.RootFolder = Environment.SpecialFolder.Desktop;
				dialog.SelectedPath = this.UserSettings.GrocyDataLocation;
				
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show(this.ResourceManager.GetString("STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue"), this.ResourceManager.GetString("STRING_ChangeDataLocation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

		private void ToolStripMenuItem_BackupData_Click(object sender, EventArgs e)
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
					MessageBox.Show(this.ResourceManager.GetString("STRING_BackupSuccessfullyCreated"), this.ResourceManager.GetString("STRING_Backup"), MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void ToolStripMenuItem_RestoreData_Click(object sender, EventArgs e)
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
					if (MessageBox.Show(this.ResourceManager.GetString("STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue"), this.ResourceManager.GetString("STRING_Restore"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

		private void ToolStripMenuItem_RecreateGrocyDatabase_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this.ResourceManager.GetString("STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue"), this.ResourceManager.GetString("ToolStripMenuItem_RecreateGrocyDatabase.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				this.GrocyPhpServer.StopServer();
				Thread.Sleep(2000); // Just give php.exe some time to stop...
				File.Delete(Path.Combine(this.UserSettings.GrocyDataLocation, "grocy.db"));
				Extensions.RestartApp();
			}
		}

		private async void ToolStripMenuItem_UpdateBarcodeBuddy_Click(object sender, EventArgs e)
		{
			this.BarcodeBuddyPhpServer.StopServer();
			Thread.Sleep(2000); // Just give php.exe some time to stop...
			await GrocyDesktopDependencyManager.UpdateEmbeddedBarcodeBuddyRelease(this);
			this.BarcodeBuddyPhpServer.StartServer();
			this.BarcodeBuddyEnvironmentManager.Setup(this.BarcodeBuddyPhpServer.Url);
			this.BarcodeBuddyBrowser.Load(this.BarcodeBuddyPhpServer.Url);
		}

		private void ToolStripMenuItem_EnableBarcodeBuddy_Click(object sender, EventArgs e)
		{
			this.UserSettings.EnableBarcodeBuddyIntegration = this.ToolStripMenuItem_EnableBarcodeBuddy.Checked;
			this.UserSettings.Save();
			Extensions.RestartApp();
		}
	}
}
