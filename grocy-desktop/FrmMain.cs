using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Resources;
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
		private NginxServerManager NginxServer;
		private PhpProcessManager PhpFastCgiServer;
		private int PhpFastCgiServerPort;
		private GrocyEnvironmentManager GrocyEnvironmentManager;
		private BarcodeBuddyEnvironmentManager BarcodeBuddyEnvironmentManager;
		private PhpProcessManager BarcodeBuddyWebsocketServer;
		private UserSettings UserSettings = UserSettings.Load();

		private void SetupCef()
		{
			Cef.EnableHighDPISupport();
			
			CefSettings cefSettings = new CefSettings();
			cefSettings.BrowserSubprocessPath = Path.Combine(GrocyDesktopDependencyManager.CefExecutingPath, @"x86\CefSharp.BrowserSubprocess.exe");
			cefSettings.CachePath = GrocyDesktopDependencyManager.CefCachePath;
			cefSettings.LogFile = Path.Combine(GrocyDesktopDependencyManager.CefCachePath, "cef.log");
			cefSettings.CefCommandLineArgs.Add("--enable-media-stream", "");
			cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", this.GrocyEnvironmentManager.LocalUrl);
			cefSettings.CefCommandLineArgs.Add("--lang", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
			Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyEnvironmentManager.LocalUrl);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.TabPage_Grocy.Controls.Add(this.GrocyBrowser);

				this.BarcodeBuddyBrowser = new ChromiumWebBrowser(this.BarcodeBuddyEnvironmentManager.LocalUrl);
				this.BarcodeBuddyBrowser.Dock = DockStyle.Fill;
				this.TabPage_BarcodeBuddy.Controls.Add(this.BarcodeBuddyBrowser);
			}
			else
			{
				this.TabControl_Main.Visible = false;
				this.ToolStripMenuItem_BarcodeBuddy.Visible = false;

				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyEnvironmentManager.LocalUrl);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.Panel_Main.Controls.Add(this.GrocyBrowser);
			}

			this.StatusStrip_Main.Visible = this.UserSettings.EnableExternalWebserverAccess;
		}

		private void SetupNginx()
		{
			string nginxConfFilePath = Path.Combine(GrocyDesktopDependencyManager.NginxExecutingPath, "conf", "nginx.conf");
			File.Copy(Path.Combine(GrocyDesktopDependencyManager.NginxExecutingPath, "conf", "nginx.conf.template"), nginxConfFilePath, true);
			Extensions.ReplaceInTextFile(nginxConfFilePath, "$GROCYPORT$", this.GrocyEnvironmentManager.Port.ToString());
			Extensions.ReplaceInTextFile(nginxConfFilePath, "$GROCYROOT$", Path.Combine(GrocyDesktopDependencyManager.GrocyExecutingPath, "public").Replace("\\", "/"));
			Extensions.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT$", this.PhpFastCgiServerPort.ToString());
			if (this.UserSettings.EnableExternalWebserverAccess)
			{
				Extensions.ReplaceInTextFile(nginxConfFilePath, "$INTERFACE$", "*");
			}
			else
			{
				Extensions.ReplaceInTextFile(nginxConfFilePath, "$INTERFACE$", "localhost");
			}

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				Extensions.ReplaceInTextFile(nginxConfFilePath, "#$BARCODEBUDDYDISABLED$", string.Empty);
				Extensions.ReplaceInTextFile(nginxConfFilePath, "$BARCODEBUDDYPORT$", this.BarcodeBuddyEnvironmentManager.Port.ToString());
				Extensions.ReplaceInTextFile(nginxConfFilePath, "$BARCODEBUDDYROOT$", GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath.Replace("\\", "/"));
			}

			this.NginxServer = new NginxServerManager(GrocyDesktopDependencyManager.NginxExecutingPath);
			this.NginxServer.Start();
		}

		private void SetupPhpFastCgiServer()
		{
			this.PhpFastCgiServerPort = Extensions.GetRandomFreePort();

			Dictionary<string, string> environmentVariables = null;
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				environmentVariables = this.BarcodeBuddyEnvironmentManager.GetEnvironmentVariables();
			}

			this.PhpFastCgiServer = new PhpProcessManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort.ToString(), true, environmentVariables);
			this.PhpFastCgiServer.Start();
		}

		private void SetupGrocy()
		{
			this.GrocyEnvironmentManager = new GrocyEnvironmentManager(GrocyDesktopDependencyManager.GrocyExecutingPath, this.UserSettings.GrocyDataLocation, this.UserSettings.GrocyWebserverDesiredPort);
			this.GrocyEnvironmentManager.Setup();
		}

		private void SetupBarcodeBuddy()
		{
			this.BarcodeBuddyEnvironmentManager = new BarcodeBuddyEnvironmentManager(GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath, this.UserSettings.BarcodeBuddyDataLocation, this.UserSettings.BarcodeBuddyWebserverDesiredPort);
			this.BarcodeBuddyEnvironmentManager.Setup(this.GrocyEnvironmentManager.LocalUrl.TrimEnd('/') + "/api/");

			this.BarcodeBuddyWebsocketServer = new PhpProcessManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath, "wsserver.php", false);
			this.BarcodeBuddyWebsocketServer.Start();
		}

		private async void FrmMain_Shown(object sender, EventArgs e)
		{
			await GrocyDesktopDependencyManager.UnpackIncludedDependenciesIfNeeded(this.UserSettings, this);
			this.SetupGrocy();
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.SetupBarcodeBuddy();
			}
			this.SetupPhpFastCgiServer();
			this.SetupNginx();
			this.SetupCef();

			this.ToolStripMenuItem_EnableBarcodeBuddy.Checked = this.UserSettings.EnableBarcodeBuddyIntegration;
			this.ToolStripMenuItem_EnableExternalAccess.Checked = this.UserSettings.EnableExternalWebserverAccess;

			string externalAccessInfo = string.Empty;
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.ToolStripStatusLabel_ExternalAccessInfo.Text = this.ResourceManager.GetString("STRING_GrocyAndBarcodeBuddyExternalAccessInfo.Text")
					.Replace("%1$s", this.GrocyEnvironmentManager.HostnameUrl)
					.Replace("%2$s", this.GrocyEnvironmentManager.IpUrl)
					.Replace("%3$s", this.BarcodeBuddyEnvironmentManager.HostnameUrl)
					.Replace("%4$s", this.BarcodeBuddyEnvironmentManager.IpUrl);
			}
			else
			{
				this.ToolStripStatusLabel_ExternalAccessInfo.Text = this.ResourceManager.GetString("STRING_GrocyExternalAccessInfo.Text")
					.Replace("%1$s", this.GrocyEnvironmentManager.HostnameUrl)
					.Replace("%2$s", this.GrocyEnvironmentManager.IpUrl);
			}
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.NginxServer != null)
			{
				this.NginxServer.Stop();
			}

			if (this.PhpFastCgiServer != null)
			{
				this.PhpFastCgiServer.Stop();
			}

			this.UserSettings.Save();
		}

		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ToolStripMenuItem_ShowPhpServerOutput_Click(object sender, EventArgs e)
		{
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text"), this.PhpFastCgiServer.GetConsoleOutput()).Show(this);
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				new FrmShowText("Barcode Buddy " + this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (Websocket Server)", this.BarcodeBuddyWebsocketServer.GetConsoleOutput()).Show(this);
			}
		}

		private void ToolStripMenuItem_ShowNginxOutput_Click(object sender, EventArgs e)
		{
			new FrmShowText(this.ResourceManager.GetString("STRING_NginxOutput.Text"), this.NginxServer.GetConsoleOutput()).Show(this);
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
			await GrocyDesktopDependencyManager.UpdateEmbeddedGrocyRelease(this);
			this.GrocyEnvironmentManager.Setup();
			this.GrocyBrowser.Load(this.GrocyEnvironmentManager.LocalUrl);
		}

		private void ToolStripMenuItem_RecreateGrocyDatabase_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this.ResourceManager.GetString("STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue.Text"), this.ResourceManager.GetString("ToolStripMenuItem_RecreateGrocyDatabase.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				File.Delete(Path.Combine(this.UserSettings.GrocyDataLocation, "grocy.db"));
				Extensions.RestartApp();
			}
		}

		private async void ToolStripMenuItem_UpdateBarcodeBuddy_Click(object sender, EventArgs e)
		{
			await GrocyDesktopDependencyManager.UpdateEmbeddedBarcodeBuddyRelease(this);
			this.BarcodeBuddyEnvironmentManager.Setup(this.GrocyEnvironmentManager.LocalUrl);
			this.BarcodeBuddyBrowser.Load(this.BarcodeBuddyEnvironmentManager.LocalUrl);
		}

		private void ToolStripMenuItem_EnableBarcodeBuddy_Click(object sender, EventArgs e)
		{
			this.UserSettings.EnableBarcodeBuddyIntegration = this.ToolStripMenuItem_EnableBarcodeBuddy.Checked;
			this.UserSettings.Save();
			Extensions.RestartApp();
		}

		private void ToolStripMenuItem_BackupDataGrocy_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
				dialog.Filter = this.ResourceManager.GetString("STRING_ZipFiles.Text") + "|*.zip";
				dialog.CheckPathExists = true;
				dialog.DefaultExt = ".zip";
				dialog.FileName = "grocy-desktop-backup_grocy-data.zip";

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (File.Exists(dialog.FileName))
					{
						File.Delete(dialog.FileName);
					}

					ZipFile.CreateFromDirectory(this.UserSettings.GrocyDataLocation, dialog.FileName);
					MessageBox.Show(this.ResourceManager.GetString("STRING_BackupSuccessfullyCreated.Text"), this.ResourceManager.GetString("STRING_Backup.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void ToolStripMenuItem_RestoreDataGrocy_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
				dialog.Filter = this.ResourceManager.GetString("STRING_ZipFiles.Text") + "|*.zip";
				dialog.CheckPathExists = true;
				dialog.CheckFileExists = true;
				dialog.DefaultExt = ".zip";

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show(this.ResourceManager.GetString("STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue.Text"), this.ResourceManager.GetString("STRING_Restore.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Directory.Delete(this.UserSettings.GrocyDataLocation, true);
						Directory.CreateDirectory(this.UserSettings.GrocyDataLocation);
						ZipFile.ExtractToDirectory(dialog.FileName, this.UserSettings.GrocyDataLocation);
						Extensions.RestartApp();
					}
				}
			}
		}

		private void ToolStripMenuItem_ConfigureChangeDataLocationGrocy_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.RootFolder = Environment.SpecialFolder.Desktop;
				dialog.SelectedPath = this.UserSettings.GrocyDataLocation;

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show(this.ResourceManager.GetString("STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue.Text"), this.ResourceManager.GetString("STRING_ChangeDataLocation.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Extensions.CopyFolder(this.UserSettings.GrocyDataLocation, dialog.SelectedPath);
						Directory.Delete(this.UserSettings.GrocyDataLocation, true);
						this.UserSettings.GrocyDataLocation = dialog.SelectedPath;
						this.UserSettings.Save();
						Extensions.RestartApp();
					}
				}
			}
		}

		private void ToolStripMenuItem_BackupDataBarcodeBuddy_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog dialog = new SaveFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
				dialog.Filter = this.ResourceManager.GetString("STRING_ZipFiles.Text") + "|*.zip";
				dialog.CheckPathExists = true;
				dialog.DefaultExt = ".zip";
				dialog.FileName = "grocy-desktop-backup_barcodebuddy-data.zip";

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (File.Exists(dialog.FileName))
					{
						File.Delete(dialog.FileName);
					}

					ZipFile.CreateFromDirectory(this.UserSettings.BarcodeBuddyDataLocation, dialog.FileName);
					MessageBox.Show(this.ResourceManager.GetString("STRING_BackupSuccessfullyCreated.Text"), this.ResourceManager.GetString("STRING_Backup.Text"), MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void ToolStripMenuItem_RestoreDataBarcodeBuddy_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
				dialog.Filter = this.ResourceManager.GetString("STRING_ZipFiles.Text") + "|*.zip";
				dialog.CheckPathExists = true;
				dialog.CheckFileExists = true;
				dialog.DefaultExt = ".zip";

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show(this.ResourceManager.GetString("STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue.Text"), this.ResourceManager.GetString("STRING_Restore.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Directory.Delete(this.UserSettings.BarcodeBuddyDataLocation, true);
						Directory.CreateDirectory(this.UserSettings.BarcodeBuddyDataLocation);
						ZipFile.ExtractToDirectory(dialog.FileName, this.UserSettings.BarcodeBuddyDataLocation);
						Extensions.RestartApp();
					}
				}
			}
		}

		private void ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.RootFolder = Environment.SpecialFolder.Desktop;
				dialog.SelectedPath = this.UserSettings.BarcodeBuddyDataLocation;

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (MessageBox.Show(this.ResourceManager.GetString("STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue.Text"), this.ResourceManager.GetString("STRING_ChangeDataLocation.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Extensions.CopyFolder(this.UserSettings.BarcodeBuddyDataLocation, dialog.SelectedPath);
						Directory.Delete(this.UserSettings.BarcodeBuddyDataLocation, true);
						this.UserSettings.BarcodeBuddyDataLocation = dialog.SelectedPath;
						this.UserSettings.Save();
						Extensions.RestartApp();
					}
				}
			}
		}

		private void ToolStripMenuItem_EnableExternalAccess_Click(object sender, EventArgs e)
		{
			this.UserSettings.EnableExternalWebserverAccess = this.ToolStripMenuItem_EnableExternalAccess.Checked;
			this.UserSettings.Save();
			Extensions.RestartApp();
		}
	}
}
