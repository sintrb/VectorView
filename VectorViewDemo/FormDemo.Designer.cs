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
            this.txtXmlFile = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSetXmlFile = new System.Windows.Forms.Button();
            this.vcMain = new Sin.VectorView.VectorControl();
            this.cbAutoReload = new System.Windows.Forms.CheckBox();
            this.tmRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtXmlFile
            // 
            this.txtXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXmlFile.Location = new System.Drawing.Point(12, 12);
            this.txtXmlFile.Name = "txtXmlFile";
            this.txtXmlFile.Size = new System.Drawing.Size(452, 21);
            this.txtXmlFile.TabIndex = 0;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(546, 10);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 1;
            this.btnReload.Text = "重新加载";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSetXmlFile
            // 
            this.btnSetXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetXmlFile.Location = new System.Drawing.Point(470, 10);
            this.btnSetXmlFile.Name = "btnSetXmlFile";
            this.btnSetXmlFile.Size = new System.Drawing.Size(70, 23);
            this.btnSetXmlFile.TabIndex = 2;
            this.btnSetXmlFile.Text = "浏览...";
            this.btnSetXmlFile.UseVisualStyleBackColor = true;
            this.btnSetXmlFile.Click += new System.EventHandler(this.btnSetXmlFile_Click);
            // 
            // vcMain
            // 
            this.vcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vcMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.vcMain.Location = new System.Drawing.Point(12, 39);
            this.vcMain.Name = "vcMain";
            this.vcMain.Size = new System.Drawing.Size(706, 434);
            this.vcMain.TabIndex = 3;
            // 
            // cbAutoReload
            // 
            this.cbAutoReload.AutoSize = true;
            this.cbAutoReload.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbAutoReload.Location = new System.Drawing.Point(627, 13);
            this.cbAutoReload.Name = "cbAutoReload";
            this.cbAutoReload.Size = new System.Drawing.Size(91, 20);
            this.cbAutoReload.TabIndex = 4;
            this.cbAutoReload.Text = "自动加载";
            this.cbAutoReload.UseVisualStyleBackColor = true;
            this.cbAutoReload.CheckedChanged += new System.EventHandler(this.cbAutoReload_CheckedChanged);
            // 
            // tmRefresh
            // 
            this.tmRefresh.Interval = 1000;
            this.tmRefresh.Tick += new System.EventHandler(this.tmRefresh_Tick);
            // 
            // FormDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 485);
            this.Controls.Add(this.cbAutoReload);
            this.Controls.Add(this.vcMain);
            this.Controls.Add(this.btnSetXmlFile);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.txtXmlFile);
            this.Name = "FormDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VectorViewDemo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXmlFile;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSetXmlFile;
        private Sin.VectorView.VectorControl vcMain;
        private System.Windows.Forms.CheckBox cbAutoReload;
        private System.Windows.Forms.Timer tmRefresh;
    }
}

