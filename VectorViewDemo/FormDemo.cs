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
        public String VERSION = "1.2";

        public FormDemo(String[] args)
        {
            InitializeComponent();

            this.Text = this.Text + " " + VERSION;

            this.tmRefresh.Enabled = this.tsmiControlAutoReload.Checked;

            if (args != null && args.Length > 0)
            {
                ofd.FileName = args[0];
                efd.FileName = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf('.')) + ".svg";
            }
            else
            {
                //ofd.FileName = "D:\\MyDoc\\DO\\VectorView\\VectorViewDemo\\data.xml";
                //efd.FileName = "D:\\MyDoc\\DO\\VectorView\\VectorViewDemo\\data.svg";
            }
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
                    System.Threading.Thread.Sleep(300);
                    tsmiControlReload_Click(null, null);
                }
            }
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                efd.FileName = ofd.FileName.Substring(0, ofd.FileName.LastIndexOf('.')) + ".svg";
                tsmiControlReload_Click(null, null);
            }
        }

        private OpenFileDialog efd = new OpenFileDialog();
        private void tsmiFileExportSVG_Click(object sender, EventArgs e)
        {
            System.Xml.XmlElement svg = vcMain.GenSVG(efd.FileName);
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
                
            //}
        }

        private void tsmiFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

            this.tsmiFileExportSVG_Click(null, null);
#else
            try
            {
                List<VectorObject> vos = ParseUtils.ParseXMLFile(ofd.FileName);
                this.vcMain.VectorObjects.Clear();
                this.vcMain.VectorObjects.AddRange(vos);
                this.vcMain.Invalidate();

                this.tsmiFileExportSVG_Click(null, null);
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