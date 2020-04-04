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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
			this.menuStrip_Main = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableBarcodeBuddytoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.backupDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restoreDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configurechangeDataLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.grocyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.recreateDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.barcodeBuddyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.updateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showBrowserDeveloperToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutGrocydesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel_Main = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage_grocy = new System.Windows.Forms.TabPage();
			this.tabPage_BarcodeBuddy = new System.Windows.Forms.TabPage();
			this.showPHPServerOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip_Main.SuspendLayout();
			this.panel_Main.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip_Main
			// 
			this.menuStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.grocyToolStripMenuItem,
            this.barcodeBuddyToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip_Main.Location = new System.Drawing.Point(0, 0);
			this.menuStrip_Main.Name = "menuStrip_Main";
			this.menuStrip_Main.Size = new System.Drawing.Size(600, 24);
			this.menuStrip_Main.TabIndex = 0;
			this.menuStrip_Main.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableBarcodeBuddytoolStripMenuItem,
            this.toolStripSeparator5,
            this.backupDataToolStripMenuItem,
            this.restoreDataToolStripMenuItem,
            this.configurechangeDataLocationToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// enableBarcodeBuddytoolStripMenuItem
			// 
			this.enableBarcodeBuddytoolStripMenuItem.CheckOnClick = true;
			this.enableBarcodeBuddytoolStripMenuItem.Name = "enableBarcodeBuddytoolStripMenuItem";
			this.enableBarcodeBuddytoolStripMenuItem.Size = new System.Drawing.Size(243, 22);
			this.enableBarcodeBuddytoolStripMenuItem.Text = "Enable Barcode Buddy";
			this.enableBarcodeBuddytoolStripMenuItem.Click += new System.EventHandler(this.enableBarcodeBuddytoolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(240, 6);
			// 
			// backupDataToolStripMenuItem
			// 
			this.backupDataToolStripMenuItem.Name = "backupDataToolStripMenuItem";
			this.backupDataToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
			this.backupDataToolStripMenuItem.Text = "Backup data";
			this.backupDataToolStripMenuItem.Click += new System.EventHandler(this.backupDataToolStripMenuItem_Click);
			// 
			// restoreDataToolStripMenuItem
			// 
			this.restoreDataToolStripMenuItem.Name = "restoreDataToolStripMenuItem";
			this.restoreDataToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
			this.restoreDataToolStripMenuItem.Text = "Restore data";
			this.restoreDataToolStripMenuItem.Click += new System.EventHandler(this.restoreDataToolStripMenuItem_Click);
			// 
			// configurechangeDataLocationToolStripMenuItem
			// 
			this.configurechangeDataLocationToolStripMenuItem.Name = "configurechangeDataLocationToolStripMenuItem";
			this.configurechangeDataLocationToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
			this.configurechangeDataLocationToolStripMenuItem.Text = "Configure/change data location";
			this.configurechangeDataLocationToolStripMenuItem.Click += new System.EventHandler(this.configurechangeDataLocationToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(240, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// grocyToolStripMenuItem
			// 
			this.grocyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.toolStripSeparator4,
            this.recreateDatabaseToolStripMenuItem});
			this.grocyToolStripMenuItem.Name = "grocyToolStripMenuItem";
			this.grocyToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
			this.grocyToolStripMenuItem.Text = "grocy";
			// 
			// updateToolStripMenuItem
			// 
			this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
			this.updateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.updateToolStripMenuItem.Text = "Update";
			this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
			// 
			// recreateDatabaseToolStripMenuItem
			// 
			this.recreateDatabaseToolStripMenuItem.Name = "recreateDatabaseToolStripMenuItem";
			this.recreateDatabaseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.recreateDatabaseToolStripMenuItem.Text = "Recreate database";
			this.recreateDatabaseToolStripMenuItem.Click += new System.EventHandler(this.recreateDatabaseToolStripMenuItem_Click);
			// 
			// barcodeBuddyToolStripMenuItem
			// 
			this.barcodeBuddyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem1});
			this.barcodeBuddyToolStripMenuItem.Name = "barcodeBuddyToolStripMenuItem";
			this.barcodeBuddyToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
			this.barcodeBuddyToolStripMenuItem.Text = "Barcode Buddy";
			// 
			// updateToolStripMenuItem1
			// 
			this.updateToolStripMenuItem1.Name = "updateToolStripMenuItem1";
			this.updateToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			this.updateToolStripMenuItem1.Text = "Update";
			this.updateToolStripMenuItem1.Click += new System.EventHandler(this.updateToolStripMenuItem1_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutGrocydesktopToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// debugToolStripMenuItem
			// 
			this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPHPServerOutputToolStripMenuItem,
            this.showBrowserDeveloperToolsToolStripMenuItem});
			this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
			this.debugToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.debugToolStripMenuItem.Text = "Debug";
			// 
			// showBrowserDeveloperToolsToolStripMenuItem
			// 
			this.showBrowserDeveloperToolsToolStripMenuItem.Name = "showBrowserDeveloperToolsToolStripMenuItem";
			this.showBrowserDeveloperToolsToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.showBrowserDeveloperToolsToolStripMenuItem.Text = "Show browser developer tools";
			this.showBrowserDeveloperToolsToolStripMenuItem.Click += new System.EventHandler(this.showBrowserDeveloperToolsToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
			// 
			// aboutGrocydesktopToolStripMenuItem
			// 
			this.aboutGrocydesktopToolStripMenuItem.Name = "aboutGrocydesktopToolStripMenuItem";
			this.aboutGrocydesktopToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
			this.aboutGrocydesktopToolStripMenuItem.Text = "About grocy-desktop";
			this.aboutGrocydesktopToolStripMenuItem.Click += new System.EventHandler(this.aboutGrocydesktopToolStripMenuItem_Click);
			// 
			// panel_Main
			// 
			this.panel_Main.Controls.Add(this.tabControl1);
			this.panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_Main.Location = new System.Drawing.Point(0, 24);
			this.panel_Main.Name = "panel_Main";
			this.panel_Main.Size = new System.Drawing.Size(600, 342);
			this.panel_Main.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage_grocy);
			this.tabControl1.Controls.Add(this.tabPage_BarcodeBuddy);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(0, 0);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(600, 342);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage_grocy
			// 
			this.tabPage_grocy.Location = new System.Drawing.Point(4, 22);
			this.tabPage_grocy.Name = "tabPage_grocy";
			this.tabPage_grocy.Size = new System.Drawing.Size(592, 316);
			this.tabPage_grocy.TabIndex = 0;
			this.tabPage_grocy.Text = "grocy";
			this.tabPage_grocy.UseVisualStyleBackColor = true;
			// 
			// tabPage_BarcodeBuddy
			// 
			this.tabPage_BarcodeBuddy.Location = new System.Drawing.Point(4, 22);
			this.tabPage_BarcodeBuddy.Name = "tabPage_BarcodeBuddy";
			this.tabPage_BarcodeBuddy.Size = new System.Drawing.Size(592, 316);
			this.tabPage_BarcodeBuddy.TabIndex = 1;
			this.tabPage_BarcodeBuddy.Text = "Barcode Buddy";
			this.tabPage_BarcodeBuddy.UseVisualStyleBackColor = true;
			// 
			// showPHPServerOutputToolStripMenuItem
			// 
			this.showPHPServerOutputToolStripMenuItem.Name = "showPHPServerOutputToolStripMenuItem";
			this.showPHPServerOutputToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
			this.showPHPServerOutputToolStripMenuItem.Text = "Show PHP server output";
			this.showPHPServerOutputToolStripMenuItem.Click += new System.EventHandler(this.showPHPServerOutputToolStripMenuItem_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(600, 366);
			this.Controls.Add(this.panel_Main);
			this.Controls.Add(this.menuStrip_Main);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip_Main;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "FrmMain";
			this.Text = "grocy";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.Shown += new System.EventHandler(this.FrmMain_Shown);
			this.menuStrip_Main.ResumeLayout(false);
			this.menuStrip_Main.PerformLayout();
			this.panel_Main.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip_Main;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.Panel panel_Main;
		private System.Windows.Forms.ToolStripMenuItem grocyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem aboutGrocydesktopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem showBrowserDeveloperToolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configurechangeDataLocationToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem backupDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restoreDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem recreateDatabaseToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage_grocy;
		private System.Windows.Forms.TabPage tabPage_BarcodeBuddy;
		private System.Windows.Forms.ToolStripMenuItem barcodeBuddyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem enableBarcodeBuddytoolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem showPHPServerOutputToolStripMenuItem;
	}
}

