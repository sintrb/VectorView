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
        public String VERSION = "1.0";
        public FormDemo()
        {
            InitializeComponent();

            this.Text = this.Text + " " + VERSION;

            this.txtXmlFile.Text = Environment.CurrentDirectory + "\\" + "data.xml";
            ofd.FileName = this.txtXmlFile.Text;


            btnReload_Click(null, null);
            this.tmRefresh.Enabled = this.cbAutoReload.Checked;
        }

        private OpenFileDialog ofd = new OpenFileDialog();
        private void btnSetXmlFile_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.txtXmlFile.Text = ofd.FileName;
                btnReload_Click(sender, e);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (this.txtXmlFile.Text.Trim().Length == 0)
                return;
#if DEBUG
            List<VectorObject> vos = ParseUtils.ParseXMLFile(this.txtXmlFile.Text);
            this.vcMain.VectorObjects.Clear();
            this.vcMain.VectorObjects.AddRange(vos);
            this.vcMain.Invalidate();
#else
            try
            {
                List<VectorObject> vos = ParseUtils.ParseXMLFile(this.txtXmlFile.Text);
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


        private DateTime PreWriteTime;
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            if (cbAutoReload.Checked == false)
            {
                tmRefresh.Enabled = false;
                return;
            }
            lock (this.txtXmlFile)
            {
                FileInfo fi = new FileInfo(this.txtXmlFile.Text);
                if (PreWriteTime == null || PreWriteTime.CompareTo(fi.LastWriteTime) != 0)
                {
                    PreWriteTime = fi.LastWriteTime;
                    btnReload_Click(null, null);
                }
            }
        }

        private void cbAutoReload_CheckedChanged(object sender, EventArgs e)
        {
            this.tmRefresh.Enabled = this.cbAutoReload.Checked;
        }
    }
}