namespace GrocyDesktop
{
	partial class FrmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.MenuStrip_Main = new System.Windows.Forms.MenuStrip();
			this.ToolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_EnableBarcodeBuddy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_grocy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_UpdateGrocy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem_RecreateGrocyDatabase = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_BarcodeBuddy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_UpdateBarcodeBuddy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_Debug = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_ShowPhpServerOutput = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_ShowBrowserDeveloperTools = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem_About = new System.Windows.Forms.ToolStripMenuItem();
			this.Panel_Main = new System.Windows.Forms.Panel();
			this.TabControl_Main = new System.Windows.Forms.TabControl();
			this.TabPage_Grocy = new System.Windows.Forms.TabPage();
			this.TabPage_BarcodeBuddy = new System.Windows.Forms.TabPage();
			this.ContextMenuStrip_DummyForResxStrings = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.STRING_PHPServerOutput = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_ChangeDataLocation = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_Backup = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_BackupSuccessfullyCreated = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_Restore = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_ConfigureChangeDataLocationGrocy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_BackupDataGrocy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_RestoreDataGrocy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_BackupDataBarcodeBuddy = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItem_RestoreDataBarcodeBuddy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.STRING_ZipFiles = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_SayThanks = new System.Windows.Forms.ToolStripMenuItem();
			this.STRING_SayThanksQuestion = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuStrip_Main.SuspendLayout();
			this.Panel_Main.SuspendLayout();
			this.TabControl_Main.SuspendLayout();
			this.ContextMenuStrip_DummyForResxStrings.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuStrip_Main
			// 
			this.MenuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_File,
            this.ToolStripMenuItem_grocy,
            this.ToolStripMenuItem_BarcodeBuddy,
            this.ToolStripMenuItem_Help});
			resources.ApplyResources(this.MenuStrip_Main, "MenuStrip_Main");
			this.MenuStrip_Main.Name = "MenuStrip_Main";
			// 
			// ToolStripMenuItem_File
			// 
			this.ToolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_EnableBarcodeBuddy,
            this.toolStripSeparator5,
            this.ToolStripMenuItem_Exit});
			this.ToolStripMenuItem_File.Name = "ToolStripMenuItem_File";
			resources.ApplyResources(this.ToolStripMenuItem_File, "ToolStripMenuItem_File");
			// 
			// ToolStripMenuItem_EnableBarcodeBuddy
			// 
			this.ToolStripMenuItem_EnableBarcodeBuddy.CheckOnClick = true;
			this.ToolStripMenuItem_EnableBarcodeBuddy.Name = "ToolStripMenuItem_EnableBarcodeBuddy";
			resources.ApplyResources(this.ToolStripMenuItem_EnableBarcodeBuddy, "ToolStripMenuItem_EnableBarcodeBuddy");
			this.ToolStripMenuItem_EnableBarcodeBuddy.Click += new System.EventHandler(this.ToolStripMenuItem_EnableBarcodeBuddy_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			// 
			// ToolStripMenuItem_Exit
			// 
			this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
			resources.ApplyResources(this.ToolStripMenuItem_Exit, "ToolStripMenuItem_Exit");
			this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
			// 
			// ToolStripMenuItem_grocy
			// 
			this.ToolStripMenuItem_grocy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_UpdateGrocy,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_BackupDataGrocy,
            this.ToolStripMenuItem_RestoreDataGrocy,
            this.ToolStripMenuItem_ConfigureChangeDataLocationGrocy,
            this.toolStripSeparator4,
            this.ToolStripMenuItem_RecreateGrocyDatabase});
			this.ToolStripMenuItem_grocy.Name = "ToolStripMenuItem_grocy";
			resources.ApplyResources(this.ToolStripMenuItem_grocy, "ToolStripMenuItem_grocy");
			// 
			// ToolStripMenuItem_UpdateGrocy
			// 
			this.ToolStripMenuItem_UpdateGrocy.Name = "ToolStripMenuItem_UpdateGrocy";
			resources.ApplyResources(this.ToolStripMenuItem_UpdateGrocy, "ToolStripMenuItem_UpdateGrocy");
			this.ToolStripMenuItem_UpdateGrocy.Click += new System.EventHandler(this.ToolStripMenuItem_UpdateGrocy_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// ToolStripMenuItem_RecreateGrocyDatabase
			// 
			this.ToolStripMenuItem_RecreateGrocyDatabase.Name = "ToolStripMenuItem_RecreateGrocyDatabase";
			resources.ApplyResources(this.ToolStripMenuItem_RecreateGrocyDatabase, "ToolStripMenuItem_RecreateGrocyDatabase");
			this.ToolStripMenuItem_RecreateGrocyDatabase.Click += new System.EventHandler(this.ToolStripMenuItem_RecreateGrocyDatabase_Click);
			// 
			// ToolStripMenuItem_BarcodeBuddy
			// 
			this.ToolStripMenuItem_BarcodeBuddy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_UpdateBarcodeBuddy,
            this.toolStripSeparator3,
            this.ToolStripMenuItem_BackupDataBarcodeBuddy,
            this.ToolStripMenuItem_RestoreDataBarcodeBuddy,
            this.ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy});
			this.ToolStripMenuItem_BarcodeBuddy.Name = "ToolStripMenuItem_BarcodeBuddy";
			resources.ApplyResources(this.ToolStripMenuItem_BarcodeBuddy, "ToolStripMenuItem_BarcodeBuddy");
			// 
			// ToolStripMenuItem_UpdateBarcodeBuddy
			// 
			this.ToolStripMenuItem_UpdateBarcodeBuddy.Name = "ToolStripMenuItem_UpdateBarcodeBuddy";
			resources.ApplyResources(this.ToolStripMenuItem_UpdateBarcodeBuddy, "ToolStripMenuItem_UpdateBarcodeBuddy");
			this.ToolStripMenuItem_UpdateBarcodeBuddy.Click += new System.EventHandler(this.ToolStripMenuItem_UpdateBarcodeBuddy_Click);
			// 
			// ToolStripMenuItem_Help
			// 
			this.ToolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Debug,
            this.toolStripSeparator2,
            this.ToolStripMenuItem_About});
			this.ToolStripMenuItem_Help.Name = "ToolStripMenuItem_Help";
			resources.ApplyResources(this.ToolStripMenuItem_Help, "ToolStripMenuItem_Help");
			// 
			// ToolStripMenuItem_Debug
			// 
			this.ToolStripMenuItem_Debug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ShowPhpServerOutput,
            this.ToolStripMenuItem_ShowBrowserDeveloperTools});
			this.ToolStripMenuItem_Debug.Name = "ToolStripMenuItem_Debug";
			resources.ApplyResources(this.ToolStripMenuItem_Debug, "ToolStripMenuItem_Debug");
			// 
			// ToolStripMenuItem_ShowPhpServerOutput
			// 
			this.ToolStripMenuItem_ShowPhpServerOutput.Name = "ToolStripMenuItem_ShowPhpServerOutput";
			resources.ApplyResources(this.ToolStripMenuItem_ShowPhpServerOutput, "ToolStripMenuItem_ShowPhpServerOutput");
			this.ToolStripMenuItem_ShowPhpServerOutput.Click += new System.EventHandler(this.ToolStripMenuItem_ShowPhpServerOutput_Click);
			// 
			// ToolStripMenuItem_ShowBrowserDeveloperTools
			// 
			this.ToolStripMenuItem_ShowBrowserDeveloperTools.Name = "ToolStripMenuItem_ShowBrowserDeveloperTools";
			resources.ApplyResources(this.ToolStripMenuItem_ShowBrowserDeveloperTools, "ToolStripMenuItem_ShowBrowserDeveloperTools");
			this.ToolStripMenuItem_ShowBrowserDeveloperTools.Click += new System.EventHandler(this.ToolStripMenuItem_ShowBrowserDeveloperTools_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// ToolStripMenuItem_About
			// 
			this.ToolStripMenuItem_About.Name = "ToolStripMenuItem_About";
			resources.ApplyResources(this.ToolStripMenuItem_About, "ToolStripMenuItem_About");
			this.ToolStripMenuItem_About.Click += new System.EventHandler(this.ToolStripMenuItem_About_Click);
			// 
			// Panel_Main
			// 
			this.Panel_Main.Controls.Add(this.TabControl_Main);
			resources.ApplyResources(this.Panel_Main, "Panel_Main");
			this.Panel_Main.Name = "Panel_Main";
			// 
			// TabControl_Main
			// 
			this.TabControl_Main.Controls.Add(this.TabPage_Grocy);
			this.TabControl_Main.Controls.Add(this.TabPage_BarcodeBuddy);
			resources.ApplyResources(this.TabControl_Main, "TabControl_Main");
			this.TabControl_Main.Name = "TabControl_Main";
			this.TabControl_Main.SelectedIndex = 0;
			// 
			// TabPage_Grocy
			// 
			resources.ApplyResources(this.TabPage_Grocy, "TabPage_Grocy");
			this.TabPage_Grocy.Name = "TabPage_Grocy";
			this.TabPage_Grocy.UseVisualStyleBackColor = true;
			// 
			// TabPage_BarcodeBuddy
			// 
			resources.ApplyResources(this.TabPage_BarcodeBuddy, "TabPage_BarcodeBuddy");
			this.TabPage_BarcodeBuddy.Name = "TabPage_BarcodeBuddy";
			this.TabPage_BarcodeBuddy.UseVisualStyleBackColor = true;
			// 
			// ContextMenuStrip_DummyForResxStrings
			// 
			this.ContextMenuStrip_DummyForResxStrings.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.STRING_PHPServerOutput,
            this.STRING_ChangeDataLocation,
            this.STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue,
            this.STRING_Backup,
            this.STRING_BackupSuccessfullyCreated,
            this.STRING_Restore,
            this.STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue,
            this.STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue,
            this.STRING_ZipFiles,
            this.STRING_SayThanks,
            this.STRING_SayThanksQuestion});
			this.ContextMenuStrip_DummyForResxStrings.Name = "ContextMenuStrip_DummyForResxStrings";
			resources.ApplyResources(this.ContextMenuStrip_DummyForResxStrings, "ContextMenuStrip_DummyForResxStrings");
			// 
			// STRING_PHPServerOutput
			// 
			this.STRING_PHPServerOutput.Name = "STRING_PHPServerOutput";
			resources.ApplyResources(this.STRING_PHPServerOutput, "STRING_PHPServerOutput");
			// 
			// STRING_ChangeDataLocation
			// 
			this.STRING_ChangeDataLocation.Name = "STRING_ChangeDataLocation";
			resources.ApplyResources(this.STRING_ChangeDataLocation, "STRING_ChangeDataLocation");
			// 
			// STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue
			// 
			this.STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue.Name = "STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue";
			resources.ApplyResources(this.STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue, "STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue");
			// 
			// STRING_Backup
			// 
			this.STRING_Backup.Name = "STRING_Backup";
			resources.ApplyResources(this.STRING_Backup, "STRING_Backup");
			// 
			// STRING_BackupSuccessfullyCreated
			// 
			this.STRING_BackupSuccessfullyCreated.Name = "STRING_BackupSuccessfullyCreated";
			resources.ApplyResources(this.STRING_BackupSuccessfullyCreated, "STRING_BackupSuccessfullyCreated");
			// 
			// STRING_Restore
			// 
			this.STRING_Restore.Name = "STRING_Restore";
			resources.ApplyResources(this.STRING_Restore, "STRING_Restore");
			// 
			// STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue
			// 
			this.STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue.Name = "STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue";
			resources.ApplyResources(this.STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue, "STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue");
			// 
			// STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue
			// 
			this.STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue.Name = "STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReally" +
    "Continue";
			resources.ApplyResources(this.STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue, "STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReally" +
        "Continue");
			// 
			// ToolStripMenuItem_ConfigureChangeDataLocationGrocy
			// 
			this.ToolStripMenuItem_ConfigureChangeDataLocationGrocy.Name = "ToolStripMenuItem_ConfigureChangeDataLocationGrocy";
			resources.ApplyResources(this.ToolStripMenuItem_ConfigureChangeDataLocationGrocy, "ToolStripMenuItem_ConfigureChangeDataLocationGrocy");
			this.ToolStripMenuItem_ConfigureChangeDataLocationGrocy.Click += new System.EventHandler(this.ToolStripMenuItem_ConfigureChangeDataLocationGrocy_Click);
			// 
			// ToolStripMenuItem_BackupDataGrocy
			// 
			this.ToolStripMenuItem_BackupDataGrocy.Name = "ToolStripMenuItem_BackupDataGrocy";
			resources.ApplyResources(this.ToolStripMenuItem_BackupDataGrocy, "ToolStripMenuItem_BackupDataGrocy");
			this.ToolStripMenuItem_BackupDataGrocy.Click += new System.EventHandler(this.ToolStripMenuItem_BackupDataGrocy_Click);
			// 
			// ToolStripMenuItem_RestoreDataGrocy
			// 
			this.ToolStripMenuItem_RestoreDataGrocy.Name = "ToolStripMenuItem_RestoreDataGrocy";
			resources.ApplyResources(this.ToolStripMenuItem_RestoreDataGrocy, "ToolStripMenuItem_RestoreDataGrocy");
			this.ToolStripMenuItem_RestoreDataGrocy.Click += new System.EventHandler(this.ToolStripMenuItem_RestoreDataGrocy_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy
			// 
			this.ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy.Name = "ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy";
			resources.ApplyResources(this.ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy, "ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy");
			this.ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy.Click += new System.EventHandler(this.ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy_Click);
			// 
			// ToolStripMenuItem_BackupDataBarcodeBuddy
			// 
			this.ToolStripMenuItem_BackupDataBarcodeBuddy.Name = "ToolStripMenuItem_BackupDataBarcodeBuddy";
			resources.ApplyResources(this.ToolStripMenuItem_BackupDataBarcodeBuddy, "ToolStripMenuItem_BackupDataBarcodeBuddy");
			this.ToolStripMenuItem_BackupDataBarcodeBuddy.Click += new System.EventHandler(this.ToolStripMenuItem_BackupDataBarcodeBuddy_Click);
			// 
			// ToolStripMenuItem_RestoreDataBarcodeBuddy
			// 
			this.ToolStripMenuItem_RestoreDataBarcodeBuddy.Name = "ToolStripMenuItem_RestoreDataBarcodeBuddy";
			resources.ApplyResources(this.ToolStripMenuItem_RestoreDataBarcodeBuddy, "ToolStripMenuItem_RestoreDataBarcodeBuddy");
			this.ToolStripMenuItem_RestoreDataBarcodeBuddy.Click += new System.EventHandler(this.ToolStripMenuItem_RestoreDataBarcodeBuddy_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// STRING_ZipFiles
			// 
			this.STRING_ZipFiles.Name = "STRING_ZipFiles";
			resources.ApplyResources(this.STRING_ZipFiles, "STRING_ZipFiles");
			// 
			// STRING_SayThanks
			// 
			this.STRING_SayThanks.Name = "STRING_SayThanks";
			resources.ApplyResources(this.STRING_SayThanks, "STRING_SayThanks");
			// 
			// STRING_SayThanksQuestion
			// 
			this.STRING_SayThanksQuestion.Name = "STRING_SayThanksQuestion";
			resources.ApplyResources(this.STRING_SayThanksQuestion, "STRING_SayThanksQuestion");
			// 
			// FrmMain
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Panel_Main);
			this.Controls.Add(this.MenuStrip_Main);
			this.MainMenuStrip = this.MenuStrip_Main;
			this.Name = "FrmMain";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.Shown += new System.EventHandler(this.FrmMain_Shown);
			this.MenuStrip_Main.ResumeLayout(false);
			this.MenuStrip_Main.PerformLayout();
			this.Panel_Main.ResumeLayout(false);
			this.TabControl_Main.ResumeLayout(false);
			this.ContextMenuStrip_DummyForResxStrings.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip MenuStrip_Main;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
		private System.Windows.Forms.Panel Panel_Main;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_grocy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Debug;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_About;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowBrowserDeveloperTools;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_UpdateGrocy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RecreateGrocyDatabase;
		private System.Windows.Forms.TabControl TabControl_Main;
		private System.Windows.Forms.TabPage TabPage_Grocy;
		private System.Windows.Forms.TabPage TabPage_BarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_UpdateBarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_EnableBarcodeBuddy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ShowPhpServerOutput;
		private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_DummyForResxStrings;
		private System.Windows.Forms.ToolStripMenuItem STRING_PHPServerOutput;
		private System.Windows.Forms.ToolStripMenuItem STRING_ChangeDataLocation;
		private System.Windows.Forms.ToolStripMenuItem STRING_GrocyDesktopWillRestartToApplyTheChangedSettingsContinue;
		private System.Windows.Forms.ToolStripMenuItem STRING_Backup;
		private System.Windows.Forms.ToolStripMenuItem STRING_BackupSuccessfullyCreated;
		private System.Windows.Forms.ToolStripMenuItem STRING_Restore;
		private System.Windows.Forms.ToolStripMenuItem STRING_TheCurrentDataWillBeOverwrittenAndGrocydesktopWillRestartContinue;
		private System.Windows.Forms.ToolStripMenuItem STRING_ThisWillDeleteAndRecreateTheGrocyDatabaseMeansAllYourDataWillBeWipedReallyContinue;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BackupDataGrocy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RestoreDataGrocy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ConfigureChangeDataLocationGrocy;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_BackupDataBarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_RestoreDataBarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ConfigureChangeDataLocationBarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem STRING_ZipFiles;
		private System.Windows.Forms.ToolStripMenuItem STRING_SayThanks;
		private System.Windows.Forms.ToolStripMenuItem STRING_SayThanksQuestion;
	}
}

