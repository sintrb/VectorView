using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sin.VectorView
{
    /// <summary>
    /// 线性路径
    /// </summary>
    public class VectorPathLine
    {
        public float x1, x2, y1, y2;
        public VectorPathLine(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.y1 = y1;
            this.y2 = y2;
        }
    }
    /// <summary>
    /// 弧线路径
    /// </summary>
    public class VectorPathArc
    {
        public float x, y, w, h, startagl, sweepagl;
        public VectorPathArc(float x, float y, float w, float h, float startagl, float sweepagl)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.startagl = startagl;
            this.sweepagl = sweepagl;
        }
    }
    /// <summary>
    /// 饼图路径
    /// </summary>
    public class VectorPathPie
    {
        public float x, y, w, h, startagl, sweepagl;
        public VectorPathPie(float x, float y, float w, float h, float startagl, float sweepagl)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.startagl = startagl;
            this.sweepagl = sweepagl;
        }
    }

    /// <summary>
    /// 矢量路径
    /// </summary>
    public class VectorPath : VectorObject
    {
        private List<Object> pathes;
        private bool fill = false;
        public VectorPath(List<Object> pathes, bool fill)
        {
            this.pathes = pathes;
            this.fill = fill;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            GraphicsPath path = new GraphicsPath();
            foreach (Object ch in pathes)
            {
                if (ch is VectorPathLine)
                {
                    VectorPathLine vpl = (VectorPathLine)ch;
                    path.AddLine(dcxt.X_V2S(vpl.x1), dcxt.Y_V2S(vpl.y1), dcxt.X_V2S(vpl.x2), dcxt.Y_V2S(vpl.y2));
                }
                else if (ch is VectorPathArc)
                {
                    VectorPathArc vpa = (VectorPathArc)ch;
                    path.AddArc(dcxt.X_V2S(vpa.x - vpa.w / 2), dcxt.Y_V2S(vpa.y - vpa.h/2), dcxt.W_V2S(vpa.w), dcxt.H_V2S(vpa.h), vpa.startagl, vpa.sweepagl);
                }
                else if (ch is VectorPathPie)
                {
                    VectorPathPie vpp = (VectorPathPie)ch;
                    path.AddArc(dcxt.X_V2S(vpp.x - vpp.w / 2), dcxt.Y_V2S(vpp.y - vpp.h/2), dcxt.W_V2S(vpp.w), dcxt.H_V2S(vpp.h), vpp.startagl, vpp.sweepagl);
                }
            }

            if (fill)
                g.FillPath(BrushWhenCxt(dcxt).Brush, path);
            else
                g.DrawPath(PenWhenCxt(dcxt).Pen, path);
        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && (fill && Assert.NotNull("画笔", Pen) || !fill && Assert.NotNull("画刷", Brush));
        }
    }
}
