using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Xml;

using Sin.VectorView;
namespace Sin.VectorView
{
    /// <summary>
    /// 矢量图绘制控件，自带缩放拖动
    /// </summary>
    public partial class VectorControl : UserControl
    {
        private VectorRender render = new VectorRender();
        public VectorControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            this.MouseWheel += new MouseEventHandler(VectorView_MouseWheel);
            this.MouseDown += new MouseEventHandler(VectorView_MouseDown);
            this.MouseMove += new MouseEventHandler(VectorView_MouseMove);
            this.MouseUp += new MouseEventHandler(VectorView_MouseUp);
            this.KeyDown += new KeyEventHandler(VectorView_KeyDown);
            this.KeyUp += new KeyEventHandler(VectorView_KeyUp);
        }

        private bool IsCtrlDown = false;
        void VectorView_KeyUp(object sender, KeyEventArgs e)
        {
            IsCtrlDown = e.Control;
        }

        void VectorView_KeyDown(object sender, KeyEventArgs e)
        {
            IsCtrlDown = e.Control;
        }

        void VectorView_MouseUp(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
        }

        void VectorView_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                this.PlusOffset(e.X - MouseDownX, e.Y - MouseDownY);
                MouseDownX = e.X;
                MouseDownY = e.Y;
            }
        }


        private bool IsMouseDown = false;
        private float MouseDownX = 0f;
        private float MouseDownY = 0f;
        void VectorView_MouseDown(object sender, MouseEventArgs e)
        {
            IsMouseDown = true;
            MouseDownX = e.X;
            MouseDownY = e.Y;
        }

        void VectorView_MouseWheel(object sender, MouseEventArgs e)
        {
            if (IsCtrlDown)
            {
                float k = 1.0f;
                if (e.Delta > 0)
                {
                    // ++
                    k = (e.Delta / 120 + 1) * 0.6f;
                }
                else if (e.Delta < 0)
                {
                    // --
                    k = 1 / ((-e.Delta / 120 + 1) * 0.6f);
                }
                // 调节
                this.AjustScale(k, e.X, e.Y);
            }
        }



        private void PlusOffset(float ox, float oy)
        {
            this.render.OffsetX += ox;
            this.render.OffsetY += oy;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.render.RenderGraphics(e.Graphics, new SizeF(Width, Height));
        }



        // 控件属性
        [Category("外观"), Description("缩放比例"), DefaultValue(1.0f)]
        public float ViewScale
        {
            get
            {
                return render.Scale;
            }
            set
            {
                render.Scale = value;
                this.Invalidate();
            }
        }

        [Category("外观"), Description("横坐标偏移"), DefaultValue(0.0f)]
        public float OffsetX
        {
            get
            {
                return render.OffsetX;
            }
            set
            {
                render.OffsetX = value;
            }
        }

        [Category("外观"), Description("纵坐标偏移"), DefaultValue(0.0f)]
        public float OffsetY
        {
            get
            {
                return render.OffsetY;
            }
            set
            {
                render.OffsetY = value;
            }
        }

        [Category("数据"), Description("向量集合")]
        public List<VectorObject> VectorObjects
        {
            get
            {
                return this.render.VectorObjects;
            }
        }


        public void AjustScale(float k, float x, float y)
        {
            if(this.render.AjustScale(k, x, y))
                this.Invalidate();
        }
        private void VectorView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                AjustScale(2.0f, e.X, e.Y); // 放大两倍
            else if (e.Button == MouseButtons.Right)
                AjustScale(0.5f, e.X, e.Y); // 缩小到一半
        }

        public XmlElement GenSVG(String filename)
        {
            XmlDocument dom = new XmlDocument();
            XmlElement svg = dom.CreateElement("svg");
            svg.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
            // dom.AppendChild(dom);

            // DrawContext dcxt = new DrawContext(1, 0, 0);

            foreach (VectorObject vo in this.VectorObjects)
            {
                XmlElement ele = vo.ExportSvg(dom, this.render.DrawContext);
                if (ele != null)
                    svg.AppendChild(ele);
            }
            dom.AppendChild(svg);
            dom.Save(filename);
            return svg;
        }
    }
}
