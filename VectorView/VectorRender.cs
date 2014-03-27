using System;
using System.Collections.Generic;
using System.Text;


using System.Drawing;
namespace Sin.VectorView
{
    /// <summary>
    /// 矢量渲染
    /// </summary>
    public class VectorRender
    {
        private DrawContext dcxt = new DrawContext();
        public List<VectorObject> VectorObjects = null;

        public VectorRender()
        {
            VectorObjects = new List<VectorObject>();
        }


        /// <summary>
        /// 缩放比例
        /// </summary>
        public float Scale
        {
            get
            {
                return this.dcxt.Scale;
            }
            set
            {
                if (value < 0.1 || value > 100)
                    return;
                this.dcxt.Scale = value;
            }
        }

        /// <summary>
        /// 横坐标偏移
        /// </summary>
        public float OffsetX
        {
            get
            {
                return this.dcxt.OffsetX;
            }
            set
            {
                this.dcxt.OffsetX = value;
            }
        }

        /// <summary>
        /// 纵坐标偏移
        /// </summary>
        public float OffsetY
        {
            get
            {
                return this.dcxt.OffsetY;
            }
            set
            {
                this.dcxt.OffsetY = value;
            }
        }

        // 内部用字体
        private Font font = new Font(new FontFamily("微软雅黑"), 14);

        /// <summary>
        /// 将图形渲染到绘图图面上
        /// </summary>
        /// <param name="g">绘图图面</param>
        /// /// <param name="size">画布尺寸</param>
        public void RenderGraphics(Graphics g, SizeF size)
        {
            if (VectorObjects.Count > 0)
            {
                foreach (VectorObject vo in this.VectorObjects)
                {
                    if(vo.Display)
                        vo.RenderObject(g, this.dcxt);
                }
                g.DrawString(String.Format("S:{0:0.0} X:{1:0} Y:{2:0}", this.Scale, this.OffsetX, this.OffsetY), font, Brushes.Red, 0, 0);
            }
            else
            {
                String s = "VectorView " + VectorView.VERSIONSTRING;
                SizeF sz = g.MeasureString(s, font);
                g.DrawString(s, font, Brushes.Red, (size.Width - sz.Width) / 2, (size.Height - sz.Height) / 2);
            }
        }


        /// <summary>
        /// 以(x,y)为定点，调节到当前缩放比例的k倍。调节成功返回true，调节失败返回false（可能是已经到调节极限）
        /// </summary>
        /// <param name="k">调节系数</param>
        /// <param name="x">定点横坐标</param>
        /// <param name="y">定点纵坐标</param>
        public bool AjustScale(float k, float x, float y)
        {
            float os = this.Scale;
            float ns = os * k;
            this.Scale = ns;

            if (this.Scale == os)
                return false;

            ns = this.Scale;

            float ox = x - this.OffsetX;
            float oy = y - this.OffsetY;
            float nx = ox * k;
            float ny = oy * k;
            this.OffsetX += (ox - nx);
            this.OffsetY += (oy - ny);

            return true;
        }
    }
}
