using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Sin.VectorView;
namespace VectorViewDemo
{
    public partial class FormDemo : Form
    {
        public String VERSION = "1.1";

        public FormDemo()
        {
            InitializeComponent();

            this.Text = this.Text + " " + VERSION;

            this.tmRefresh.Enabled = this.tsmiControlAutoReload.Checked;
        }

        private OpenFileDialog ofd = new OpenFileDialog();

        private DateTime PreWriteTime;
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            if (tsmiControlAutoReload.Checked == false)
            {
                tmRefresh.Enabled = false;
                return;
            }
            if (ofd.FileName == null || ofd.FileName.Length == 0)
                return;
            lock (ofd)
            {
                FileInfo fi = new FileInfo(ofd.FileName);
                if (PreWriteTime == null || PreWriteTime.CompareTo(fi.LastWriteTime) != 0)
                {
                    PreWriteTime = fi.LastWriteTime;
                    tsmiControlReload_Click(null, null);
                }
            }
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tsmiControlReload_Click(null, null);
            }
        }

        private void tsmiFileExportSVG_Click(object sender, EventArgs e)
        {

        }

        private void tsmiFileExit_Click(object sender, EventArgs e)
        {

        }

        private void tsmiControlReload_Click(object sender, EventArgs e)
        {
            if (ofd.FileName==null || ofd.FileName.Length == 0)
                return;
            this.Text = ofd.FileName;
#if DEBUG
            List<VectorObject> vos = ParseUtils.ParseXMLFile(ofd.FileName);
            this.vcMain.VectorObjects.Clear();
            this.vcMain.VectorObjects.AddRange(vos);
            this.vcMain.Invalidate();
#else
            try
            {
                List<VectorObject> vos = ParseUtils.ParseXMLFile(ofd.FileName);
                this.vcMain.VectorObjects.Clear();
                this.vcMain.VectorObjects.AddRange(vos);
                this.vcMain.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
#endif
        }

        private void tsmiControlAutoReload_Click(object sender, EventArgs e)
        {
            tsmiControlAutoReload.Checked = !tsmiControlAutoReload.Checked;
            this.tmRefresh.Enabled = this.tsmiControlAutoReload.Checked;
        }

        private void tsmiHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
    }
}