namespace VectorViewDemo
{
    partial class FormDemo
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDemo));
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.vcMain = new Sin.VectorView.VectorControl();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileExportSVG = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiControl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiControlReload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiControlAutoReload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmRefresh
            // 
            this.tmRefresh.Interval = 1000;
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // vcMain
            // 
            this.vcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vcMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vcMain.Location = new System.Drawing.Point(12, 27);
            this.vcMain.Name = "vcMain";
            this.vcMain.Size = new System.Drawing.Size(706, 446);
            this.vcMain.TabIndex = 3;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiControl,
            this.tsmiHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(730, 24);
            this.msMain.TabIndex = 5;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileOpen,
            this.tsmiFileExportSVG,
            this.toolStripSeparator1,
            this.tsmiFileExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(43, 20);
            this.tsmiFile.Text = "文件";
            // 
            // tsmiFileOpen
            // 
            this.tsmiFileOpen.Name = "tsmiFileOpen";
            this.tsmiFileOpen.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileOpen.Text = "打开...";
            this.tsmiFileOpen.Click += new System.EventHandler(this.tsmiFileOpen_Click);
            // 
            // tsmiFileExportSVG
            // 
            this.tsmiFileExportSVG.Name = "tsmiFileExportSVG";
            this.tsmiFileExportSVG.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileExportSVG.Text = "导出SVG...";
            this.tsmiFileExportSVG.Visible = false;
            this.tsmiFileExportSVG.Click += new System.EventHandler(this.tsmiFileExportSVG_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiFileExit
            // 
            this.tsmiFileExit.Name = "tsmiFileExit";
            this.tsmiFileExit.Size = new System.Drawing.Size(152, 22);
            this.tsmiFileExit.Text = "退出";
            this.tsmiFileExit.Click += new System.EventHandler(this.tsmiFileExit_Click);
            // 
            // tsmiControl
            // 
            this.tsmiControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiControlReload,
            this.tsmiControlAutoReload});
            this.tsmiControl.Name = "tsmiControl";
            this.tsmiControl.Size = new System.Drawing.Size(43, 20);
            this.tsmiControl.Text = "控制";
            // 
            // tsmiControlReload
            // 
            this.tsmiControlReload.Name = "tsmiControlReload";
            this.tsmiControlReload.Size = new System.Drawing.Size(122, 22);
            this.tsmiControlReload.Text = "重新加载";
            this.tsmiControlReload.Click += new System.EventHandler(this.tsmiControlReload_Click);
            // 
            // tsmiControlAutoReload
            // 
            this.tsmiControlAutoReload.Checked = true;
            this.tsmiControlAutoReload.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiControlAutoReload.Name = "tsmiControlAutoReload";
            this.tsmiControlAutoReload.Size = new System.Drawing.Size(122, 22);
            this.tsmiControlAutoReload.Text = "自动加载";
            this.tsmiControlAutoReload.Click += new System.EventHandler(this.tsmiControlAutoReload_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelpAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(43, 20);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiHelpAbout
            // 
            this.tsmiHelpAbout.Name = "tsmiHelpAbout";
            this.tsmiHelpAbout.Size = new System.Drawing.Size(98, 22);
            this.tsmiHelpAbout.Text = "关于";
            this.tsmiHelpAbout.Click += new System.EventHandler(this.tsmiHelpAbout_Click);
            // 
            // FormDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 485);
            this.Controls.Add(this.vcMain);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "FormDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VectorViewDemo";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sin.VectorView.VectorControl vcMain;
        private System.Windows.Forms.Timer tmRefresh;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileExportSVG;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiFileExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiControl;
        private System.Windows.Forms.ToolStripMenuItem tsmiControlReload;
        private System.Windows.Forms.ToolStripMenuItem tsmiControlAutoReload;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpAbout;
    }
}

