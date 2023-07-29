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
		private bool GrocyBrowserFirstLoad = true;
		private ChromiumWebBrowser BarcodeBuddyBrowser;
		private bool BarcodeBuddyBrowserFirstLoad = true;
		private NginxServerManager NginxServer;
		private GrocyManager GrocyManager;
		private BarcodeBuddyManager BarcodeBuddyManager;
		private PhpManager BarcodeBuddyWebsocketServer;
		private UserSettings UserSettings = UserSettings.Load();

		private PhpManager PhpFastCgiServer1;
		private int PhpFastCgiServerPort1;
		private PhpManager PhpFastCgiServer2;
		private int PhpFastCgiServerPort2;
		private PhpManager PhpFastCgiServer3;
		private int PhpFastCgiServerPort3;
		private PhpManager PhpFastCgiServer4;
		private int PhpFastCgiServerPort4;
		private PhpManager PhpFastCgiServer5;
		private int PhpFastCgiServerPort5;
		private PhpManager PhpFastCgiServer6;
		private int PhpFastCgiServerPort6;
		private PhpManager PhpFastCgiServer7;
		private int PhpFastCgiServerPort7;
		private PhpManager PhpFastCgiServer8;
		private int PhpFastCgiServerPort8;

		private void SetupCef()
		{
			CefSettings cefSettings = new CefSettings();
			cefSettings.BrowserSubprocessPath = Path.Combine(GrocyDesktopDependencyManager.CefExecutingPath, @"CefSharp.BrowserSubprocess.exe");
			cefSettings.CachePath = GrocyDesktopDependencyManager.CefCachePath;
			cefSettings.UserDataPath = GrocyDesktopDependencyManager.CefUserDataPath;
			cefSettings.LogSeverity = LogSeverity.Disable;
			cefSettings.CefCommandLineArgs.Add("--enable-media-stream", "");
			cefSettings.CefCommandLineArgs.Add("--lang", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", this.GrocyManager.DesiredUrl + "," + this.BarcodeBuddyManager.DesiredUrl);
			}
			else
			{
				cefSettings.CefCommandLineArgs.Add("--unsafely-treat-insecure-origin-as-secure", this.GrocyManager.DesiredUrl);
			}

			Cef.Initialize(cefSettings, performDependencyCheck: false, browserProcessHandler: null);

			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyManager.DesiredUrl);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.TabPage_Grocy.Controls.Add(this.GrocyBrowser);
				this.GrocyBrowser.LoadingStateChanged += GrocyBrowser_LoadingStateChanged;

				this.BarcodeBuddyBrowser = new ChromiumWebBrowser(this.BarcodeBuddyManager.DesiredUrl);
				this.BarcodeBuddyBrowser.Dock = DockStyle.Fill;
				this.TabPage_BarcodeBuddy.Controls.Add(this.BarcodeBuddyBrowser);
				this.BarcodeBuddyBrowser.LoadingStateChanged += BarcodeBuddyBrowser_LoadingStateChanged;
			}
			else
			{
				this.TabControl_Main.Visible = false;
				this.ToolStripMenuItem_BarcodeBuddy.Visible = false;

				this.GrocyBrowser = new ChromiumWebBrowser(this.GrocyManager.DesiredUrl);
				this.GrocyBrowser.Dock = DockStyle.Fill;
				this.Panel_Main.Controls.Add(this.GrocyBrowser);
				this.GrocyBrowser.LoadingStateChanged += GrocyBrowser_LoadingStateChanged;
			}

			this.StatusStrip_Main.Visible = this.UserSettings.EnableExternalWebserverAccess;
		}

		private void GrocyBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
		{
			if (this.GrocyBrowser.IsBrowserInitialized && e.IsLoading == false && this.GrocyBrowserFirstLoad)
			{
				this.GrocyBrowser.SetZoomLevel(this.UserSettings.GrocyBrowserZoomLevel);
				this.GrocyBrowserFirstLoad = false;
			}
		}

		private void BarcodeBuddyBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
		{
			if (this.BarcodeBuddyBrowser.IsBrowserInitialized && e.IsLoading == false && this.BarcodeBuddyBrowserFirstLoad)
			{
				this.BarcodeBuddyBrowser.SetZoomLevel(this.UserSettings.BarcodeBuddyBrowserZoomLevel);
				this.BarcodeBuddyBrowserFirstLoad = false;
			}
		}

		private void SetupNginx()
		{
			string nginxConfFilePath = Path.Combine(GrocyDesktopDependencyManager.NginxExecutingPath, "conf", "nginx.conf");
			File.Copy(Path.Combine(GrocyDesktopDependencyManager.NginxExecutingPath, "conf", "nginx.conf.template"), nginxConfFilePath, true);

			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$GROCYPORT$", this.GrocyManager.Port.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$GROCYROOT$", Path.Combine(GrocyDesktopDependencyManager.GrocyExecutingPath, "public").Replace("\\", "/"));
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT1$", this.PhpFastCgiServerPort1.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT2$", this.PhpFastCgiServerPort2.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT3$", this.PhpFastCgiServerPort3.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT4$", this.PhpFastCgiServerPort4.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT5$", this.PhpFastCgiServerPort5.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT6$", this.PhpFastCgiServerPort6.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT7$", this.PhpFastCgiServerPort7.ToString());
			IOHelper.ReplaceInTextFile(nginxConfFilePath, "$PHPFASTCGIPORT8$", this.PhpFastCgiServerPort8.ToString());

			if (this.UserSettings.EnableExternalWebserverAccess)
			{
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "$INTERFACE$", "*");
			}
			else
			{
				IOHelper.ReplaceInTextFile(nginxConfFilePath, "$INTERFACE$", "127.0.0.1");
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
			this.PhpFastCgiServerPort1 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort2 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort3 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort4 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort5 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort6 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort7 = NetHelper.GetRandomFreePort();
			this.PhpFastCgiServerPort8 = NetHelper.GetRandomFreePort();

			Dictionary<string, string> environmentVariables = new Dictionary<string, string>();
			if (this.UserSettings.EnableBarcodeBuddyIntegration)
			{
				environmentVariables = this.BarcodeBuddyManager.GetEnvironmentVariables();
			}

			environmentVariables.Add("PHP_FCGI_MAX_REQUESTS", "0");
			this.PhpFastCgiServer1 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort1.ToString(), true, environmentVariables);
			this.PhpFastCgiServer1.Start();
			this.PhpFastCgiServer2 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort2.ToString(), true, environmentVariables);
			this.PhpFastCgiServer2.Start();
			this.PhpFastCgiServer3 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort3.ToString(), true, environmentVariables);
			this.PhpFastCgiServer3.Start();
			this.PhpFastCgiServer4 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort4.ToString(), true, environmentVariables);
			this.PhpFastCgiServer4.Start();
			this.PhpFastCgiServer5 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort5.ToString(), true, environmentVariables);
			this.PhpFastCgiServer5.Start();
			this.PhpFastCgiServer6 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort6.ToString(), true, environmentVariables);
			this.PhpFastCgiServer6.Start();
			this.PhpFastCgiServer7 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort7.ToString(), true, environmentVariables);
			this.PhpFastCgiServer7.Start();
			this.PhpFastCgiServer8 = new PhpManager(GrocyDesktopDependencyManager.PhpExecutingPath, GrocyDesktopDependencyManager.PhpExecutingPath, "-b 127.0.0.1:" + this.PhpFastCgiServerPort8.ToString(), true, environmentVariables);
			this.PhpFastCgiServer8.Start();
		}

		private void SetupGrocy()
		{
			this.GrocyManager = new GrocyManager(GrocyDesktopDependencyManager.GrocyExecutingPath, this.UserSettings.GrocyDataLocation, this.UserSettings.EnableExternalWebserverAccess, this.UserSettings.GrocyWebserverDesiredPort);
			this.GrocyManager.Setup();
		}

		private void SetupBarcodeBuddy()
		{
			this.BarcodeBuddyManager = new BarcodeBuddyManager(GrocyDesktopDependencyManager.BarcodeBuddyExecutingPath, this.UserSettings.BarcodeBuddyDataLocation, this.UserSettings.EnableExternalWebserverAccess, this.UserSettings.BarcodeBuddyWebserverDesiredPort);
			this.BarcodeBuddyManager.Setup(this.GrocyManager.DesiredUrl.TrimEnd('/') + "/api/");

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
					.Replace("%1$s", this.GrocyManager.IpUrl)
					.Replace("%2$s", this.GrocyManager.HostnameUrl)
					.Replace("%3$s", this.BarcodeBuddyManager.IpUrl)
					.Replace("%4$s", this.BarcodeBuddyManager.HostnameUrl);
			}
			else
			{
				this.ToolStripStatusLabel_ExternalAccessInfo.Text = this.ResourceManager.GetString("STRING_GrocyExternalAccessInfo.Text")
					.Replace("%1$s", this.GrocyManager.IpUrl)
					.Replace("%2$s", this.GrocyManager.HostnameUrl);
			}
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.PhpFastCgiServer1 != null)
			{
				this.PhpFastCgiServer1.Stop();
			}
			if (this.PhpFastCgiServer2 != null)
			{
				this.PhpFastCgiServer2.Stop();
			}
			if (this.PhpFastCgiServer3 != null)
			{
				this.PhpFastCgiServer3.Stop();
			}
			if (this.PhpFastCgiServer4 != null)
			{
				this.PhpFastCgiServer4.Stop();
			}
			if (this.PhpFastCgiServer5 != null)
			{
				this.PhpFastCgiServer5.Stop();
			}
			if (this.PhpFastCgiServer6 != null)
			{
				this.PhpFastCgiServer6.Stop();
			}
			if (this.PhpFastCgiServer7 != null)
			{
				this.PhpFastCgiServer7.Stop();
			}
			if (this.PhpFastCgiServer8 != null)
			{
				this.PhpFastCgiServer8.Stop();
			}

			if (this.NginxServer != null)
			{
				this.NginxServer.Stop();
			}

			if (this.UserSettings.EnableBarcodeBuddyIntegration && this.BarcodeBuddyWebsocketServer != null)
			{
				this.BarcodeBuddyWebsocketServer.Stop();
			}

			this.UserDataSyncSave();
		}

		private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ToolStripMenuItem_ShowPhpRuntimeOutput_Click(object sender, EventArgs e)
		{
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 1)", this.PhpFastCgiServer1.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 2)", this.PhpFastCgiServer2.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 3)", this.PhpFastCgiServer3.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 4)", this.PhpFastCgiServer4.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 5)", this.PhpFastCgiServer5.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 6)", this.PhpFastCgiServer6.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 7)", this.PhpFastCgiServer7.GetConsoleOutput()).Show(this);
			new FrmShowText(this.ResourceManager.GetString("STRING_PHPOutput.Text") + " (FastCGI Server 8)", this.PhpFastCgiServer8.GetConsoleOutput()).Show(this);
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
			this.FrmMain_FormClosing(null, null); // Stop runtime
			await GrocyDesktopDependencyManager.UpdateEmbeddedGrocyRelease(this);
			ApplicationHelper.RestartApp();
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
			this.FrmMain_FormClosing(null, null); // Stop runtime
			await GrocyDesktopDependencyManager.UpdateEmbeddedBarcodeBuddyRelease(this);
			ApplicationHelper.RestartApp();
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

				if (this.UserSettings.EnableBarcodeBuddyIntegration && Directory.Exists(this.UserSettings.BarcodeBuddyDataLocation))
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

		private void BrowserZoom(bool? zoomIn)
		{
			if (this.TabControl_Main.SelectedTab == TabPage_Grocy)
			{
				if (zoomIn == null)
				{
					this.UserSettings.GrocyBrowserZoomLevel = 0;
				}
				else if (zoomIn == true)
				{
					this.UserSettings.GrocyBrowserZoomLevel += 0.2;
				}
				else if (zoomIn == false)
				{
					this.UserSettings.GrocyBrowserZoomLevel -= 0.2;
				}
				this.UserSettings.Save();

				this.GrocyBrowser.SetZoomLevel(this.UserSettings.GrocyBrowserZoomLevel);
			}
			else if (this.TabControl_Main.SelectedTab == TabPage_BarcodeBuddy)
			{
				if (zoomIn == null)
				{
					this.UserSettings.BarcodeBuddyBrowserZoomLevel = 0;
				}
				else if (zoomIn == true)
				{
					this.UserSettings.BarcodeBuddyBrowserZoomLevel += 0.2;
				}
				else if (zoomIn == false)
				{
					this.UserSettings.BarcodeBuddyBrowserZoomLevel -= 0.2;
				}
				this.UserSettings.Save();

				this.BarcodeBuddyBrowser.SetZoomLevel(this.UserSettings.BarcodeBuddyBrowserZoomLevel);
			}
		}

		private void ToolStripMenuItem_ZoomIn_Click(object sender, EventArgs e)
		{
			this.BrowserZoom(true);
		}

		private void ToolStripMenuItem_ZoomOut_Click(object sender, EventArgs e)
		{
			this.BrowserZoom(false);
		}

		private void ToolStripMenuItem_ResetZoom_Click(object sender, EventArgs e)
		{
			this.BrowserZoom(null);
		}
	}
}
