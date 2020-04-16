using CefSharp;
using CefSharp.WinForms;
using GrocyDesktop.Helpers;
using GrocyDesktop.Management;
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
		private PhpManager PhpFastCgiServer;
		private int PhpFastCgiServerPort;
		private GrocyManager GrocyManager;
		private BarcodeBuddyManager BarcodeBuddyManager;
		private PhpManager BarcodeBuddyWebsocketServer;
		private UserSettings UserSettings = UserSettings.Load();

		private void SetupCef()
		{
			Cef.EnableHighDPISupport();
			
			CefSettings cefSettings = new CefSettings();
			cefSettings.BrowserSubprocessPath = Path.Combine(GrocyDesktopDependencyManager.CefExecutingPath, @"x86\CefSharp.BrowserSubprocess.exe");
			cefSettings.CachePath = GrocyDesktopDependencyManager.CefCachePath;
			cefSettings.LogFile = Path.Combine(GrocyDesktopDependencyManager.CefCachePath, "cef.log");
			cefSettings.CefCommandLineArgs.Add("--enable-media-stream", "");
			cefSettings.CefCommandLineArgs.Add("--lang", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", this.GrocyManager.LocalUrl + "," + this.BarcodeBuddyManager.LocalUrl);
			}
			else
			{
				cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", this.GrocyManager.LocalUrl);
			}
			
			Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyManager.LocalUrl);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.TabPage_Grocy.Controls.Add(this.GrocyBrowser);

				this.BarcodeBuddyBrowser = new ChromiumWebBrowser(this.BarcodeBuddyManager.LocalUrl);
				this.BarcodeBuddyBrowser.Dock = DockStyle.Fill;
				this.TabPage_BarcodeBuddy.Controls.Add(this.BarcodeBuddyBrowser);
			}
			else
			{
				this.TabControl_Main.Visible = false;
				this.ToolStripMenuItem_BarcodeBuddy.Visible = false;

				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyManager.LocalUrl);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.Panel_Main.Controls.Add(this.GrocyBrowser);
			}

			this.StatusStrip_Main.Visible = this.UserSettings.EnableExternalWebserverAccess;
		}

		private void SetupNginx()
		{
			string nginxConfFilePath = Path.Combine(GrocyDesktopDependencyManager.NginxExecutingPath, "conf", "nginx.conf");
			File.Copy(Path.Combine(GrocyDesktopDependencyManager.NginxExecutingPath, "conf", "nginx.conf.template"), nginxConfFilePath, true);

			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$GROCYPORT$", this.GrocyManager.Port.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$GROCYROOT$", Path.Combine(GrocyDesktopDependencyManager.GrocyExecutingPath, "public").Replace("\\", "/"));
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT$", this.PhpFastCgiServerPort.ToString());

			if (this.UserSettings.EnableExternalWebserverAccess)
			{
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "$INTERFACE$", "*");
			}
			else
			{
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "$INTERFACE$", "localhost");
			}

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "#$BARCODEBUDDYDISABLED$", string.Empty);
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "$BARCODEBUDDYPORT$", this.BarcodeBuddyManager.Port.ToString());
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "$BARCODEBUDDYROOT$", GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath.Replace("\\", "/"));
			}

			this.NginxServer = new NginxServerManager(GrocyDesktopDependencyManager.NginxExecutingPath);
			this.NginxServer.Start();
		}

		private void SetupPhpFastCgiServer()
		{
			this.PhpFastCgiServerPort = NetHelper.GetRandomFreePort();

			Dictionary<string, string> environmentVariables = null;
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				environmentVariables = this.BarcodeBuddyManager.GetEnvironmentVariables();
			}

			this.PhpFastCgiServer = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort.ToString(), true, environmentVariables);
			this.PhpFastCgiServer.Start();
		}

		private void SetupGrocy()
		{
			this.GrocyManager = new GrocyManager(GrocyDesktopDependencyManager.GrocyExecutingPath, this.UserSettings.GrocyDataLocation, this.UserSettings.GrocyWebserverDesiredPort);
			this.GrocyManager.Setup();
		}

		private void SetupBarcodeBuddy()
		{
			this.BarcodeBuddyManager = new BarcodeBuddyManager(GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath, this.UserSettings.BarcodeBuddyDataLocation, this.UserSettings.BarcodeBuddyWebserverDesiredPort);
			this.BarcodeBuddyManager.Setup(this.GrocyManager.LocalUrl.TrimEnd('/') + "/api/");

			this.BarcodeBuddyWebsocketServer = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath, "wsserver.php", false);
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
			this.UserDataSyncRestore();
			this.SetupPhpFastCgiServer();
			this.SetupNginx();
			this.SetupCef();

			this.ToolStripMenuItem_EnableBarcodeBuddy.Checked = this.UserSettings.EnableBarcodeBuddyIntegration;
			this.ToolStripMenuItem_EnableExternalAccess.Checked = this.UserSettings.EnableExternalWebserverAccess;
			this.ToolStripMenuItem_EnableUserDataSync.Checked = this.UserSettings.EnableUserDataSync;

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.ToolStripStatusLabel_ExternalAccessInfo.Text = this.ResourceManager.GetString("STRING_GrocyAndBarcodeBuddyExternalAccessInfo.Text")
					.Replace("%1$s", this.GrocyManager.HostnameUrl)
					.Replace("%2$s", this.GrocyManager.IpUrl)
					.Replace("%3$s", this.BarcodeBuddyManager.HostnameUrl)
					.Replace("%4$s", this.BarcodeBuddyManager.IpUrl);
			}
			else
			{
				this.ToolStripStatusLabel_ExternalAccessInfo.Text = this.ResourceManager.GetString("STRING_GrocyExternalAccessInfo.Text")
					.Replace("%1$s", this.GrocyManager.HostnameUrl)
					.Replace("%2$s", this.GrocyManager.IpUrl);
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

			this.UserDataSyncSave();
		}

		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ToolStripMenuItem_ShowPhpRuntimeOutput_Click(object sender, EventArgs e)
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
			this.GrocyManager.Setup();
			this.GrocyBrowser.Load(this.GrocyManager.LocalUrl);
		}

		private void ToolStripMenuItem_RecreateGrocyDatabase_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this.ResourceManager.GetString("STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue.Text"), this.ResourceManager.GetString("ToolStripMenuItem_RecreateGrocyDatabase.Text"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
			{
				File.Delete(Path.Combine(this.UserSettings.GrocyDataLocation, "grocy.db"));
				ApplicationHelper.RestartApp();
			}
		}

		private async void ToolStripMenuItem_UpdateBarcodeBuddy_Click(object sender, EventArgs e)
		{
			await GrocyDesktopDependencyManager.UpdateEmbeddedBarcodeBuddyRelease(this);
			this.BarcodeBuddyManager.Setup(this.GrocyManager.LocalUrl);
			this.BarcodeBuddyBrowser.Load(this.BarcodeBuddyManager.LocalUrl);
		}

		private void ToolStripMenuItem_EnableBarcodeBuddy_Click(object sender, EventArgs e)
		{
			this.UserSettings.EnableBarcodeBuddyIntegration = this.ToolStripMenuItem_EnableBarcodeBuddy.Checked;
			this.UserSettings.Save();
			ApplicationHelper.RestartApp();
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
						ApplicationHelper.RestartApp();
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
						IOHelper.CopyFolder(this.UserSettings.GrocyDataLocation, dialog.SelectedPath);
						Directory.Delete(this.UserSettings.GrocyDataLocation, true);
						this.UserSettings.GrocyDataLocation = dialog.SelectedPath;
						this.UserSettings.Save();
						ApplicationHelper.RestartApp();
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
						ApplicationHelper.RestartApp();
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
						IOHelper.CopyFolder(this.UserSettings.BarcodeBuddyDataLocation, dialog.SelectedPath);
						Directory.Delete(this.UserSettings.BarcodeBuddyDataLocation, true);
						this.UserSettings.BarcodeBuddyDataLocation = dialog.SelectedPath;
						this.UserSettings.Save();
						ApplicationHelper.RestartApp();
					}
				}
			}
		}

		private void ToolStripMenuItem_EnableExternalAccess_Click(object sender, EventArgs e)
		{
			this.UserSettings.EnableExternalWebserverAccess = this.ToolStripMenuItem_EnableExternalAccess.Checked;
			this.UserSettings.Save();
			ApplicationHelper.RestartApp();
		}

		private void ToolStripMenuItem_EnableUserDataSync_Click(object sender, EventArgs e)
		{
			this.UserSettings.EnableUserDataSync = this.ToolStripMenuItem_EnableUserDataSync.Checked;

			if (this.ToolStripMenuItem_EnableUserDataSync.Checked)
			{
				using (FolderBrowserDialog dialog = new FolderBrowserDialog())
				{
					dialog.RootFolder = Environment.SpecialFolder.Desktop;
					dialog.SelectedPath = this.UserSettings.UserDataSyncFolderPath;

					if (dialog.ShowDialog() == DialogResult.OK)
					{
						this.UserSettings.UserDataSyncFolderPath = dialog.SelectedPath;
					}
					else
					{
						this.UserSettings.UserDataSyncFolderPath = string.Empty;
					}
				}
			}

			this.UserSettings.Save();
		}

		private void UserDataSyncSave()
		{
			if (this.UserSettings.EnableUserDataSync && !string.IsNullOrEmpty(this.UserSettings.UserDataSyncFolderPath) && Directory.Exists(this.UserSettings.UserDataSyncFolderPath))
			{
				string grocySyncZipPath = Path.Combine(this.UserSettings.UserDataSyncFolderPath, "grocy-data.zip");
				if (File.Exists(grocySyncZipPath))
				{
					File.Delete(grocySyncZipPath);
				}
				ZipFile.CreateFromDirectory(this.UserSettings.GrocyDataLocation, grocySyncZipPath);

				if (this.UserSettings.EnableBarcodeBuddyIntegration)
				{
					string barcodeBuddySyncZipPath = Path.Combine(this.UserSettings.UserDataSyncFolderPath, "barcodebuddy-data.zip");
					if (File.Exists(barcodeBuddySyncZipPath))
					{
						File.Delete(barcodeBuddySyncZipPath);
					}
					ZipFile.CreateFromDirectory(this.UserSettings.BarcodeBuddyDataLocation, barcodeBuddySyncZipPath);
				}
			}
		}

		private void UserDataSyncRestore()
		{
			if (this.UserSettings.EnableUserDataSync && !string.IsNullOrEmpty(this.UserSettings.UserDataSyncFolderPath) && Directory.Exists(this.UserSettings.UserDataSyncFolderPath))
			{
				string grocySyncZipPath = Path.Combine(this.UserSettings.UserDataSyncFolderPath, "grocy-data.zip");
				if (File.Exists(grocySyncZipPath))
				{
					Directory.Delete(this.UserSettings.GrocyDataLocation, true);
					Directory.CreateDirectory(this.UserSettings.GrocyDataLocation);
					ZipFile.ExtractToDirectory(grocySyncZipPath, this.UserSettings.GrocyDataLocation);
				}
				
				if (this.UserSettings.EnableBarcodeBuddyIntegration)
				{
					string barcodeBuddySyncZipPath = Path.Combine(this.UserSettings.UserDataSyncFolderPath, "barcodebuddy-data.zip");
					if (File.Exists(barcodeBuddySyncZipPath))
					{
						Directory.Delete(this.UserSettings.BarcodeBuddyDataLocation, true);
						Directory.CreateDirectory(this.UserSettings.BarcodeBuddyDataLocation);
						ZipFile.ExtractToDirectory(barcodeBuddySyncZipPath, this.UserSettings.BarcodeBuddyDataLocation);
					}
				}
			}
		}
	}
}
