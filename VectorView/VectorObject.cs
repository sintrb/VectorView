using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sin.VectorView
{
    /// <summary>
    /// Ê¸Á¿Í¼»ùÀà
    /// </summary>
    public class VectorObject
    {
        public VectorObject Parent = null;
        public ContextAttributes Attributes = null;
        public float ScaleWeight = 1.0f;
        public bool Display = true;
        virtual public void RenderObject(Graphics g, DrawContext dcxt)
        {
            throw new Exception("VectorObject»ùÀàÎ´ÊµÏÖäÖÈ¾");
        }

        public ContextBrush Brush
        {
            get
            {
                if(this.Attributes != null && this.Attributes.Brush!=null)
                    return this.Attributes.Brush;
                else
                    return this.Parent == null ? null : this.Parent.Brush;
            }
        }

        public ContextFont Font
        {
            get
            {
                if (this.Attributes != null && this.Attributes.Font != null)
                    return this.Attributes.Font;
                else
                    return this.Parent == null ? null : this.Parent.Font;
            }
        }

        public ContextPen Pen
        {
            get
            {
                if (this.Attributes != null && this.Attributes.Pen != null)
                    return this.Attributes.Pen;
                else
                    return this.Parent == null ? null : this.Parent.Pen;
            }
        }

        public ContextBrush BrushWhenCxt(DrawContext dcxt)
        {
            return this.Brush;
        }

        public ContextFont FontWhenCxt(DrawContext dcxt)
        {
            return Font.ScaleTo(dcxt.Scale * ScaleWeight);
        }

        public ContextPen PenWhenCxt(DrawContext dcxt)
        {
            return Pen.ScaleTo(dcxt.Scale * ScaleWeight);
        }

        virtual public bool CheckRequire()
        {
            return true;
        }
    }

    /// <summary>
    /// Ê¸Á¿Ïß¶Î
    /// </summary>
    public class VectorLine : VectorObject
    {
        private float x1,x2,y1,y2;
        public VectorLine(float x1, float y1, float x2, float y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            g.DrawLine(PenWhenCxt(dcxt).Pen, dcxt.X_V2S(x1), dcxt.Y_V2S(y1), dcxt.X_V2S(x2), dcxt.Y_V2S(y2));
        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && Assert.NotNull("»­±Ê", Pen);
        }
    }

    /// <summary>
    /// Ê¸Á¿Ô²
    /// </summary>
    public class VectorCircle : VectorObject
    {
        private float x, y, w, h;
        private bool fill = false;
        public VectorCircle(float x, float y, float w, float h, bool fill)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.fill = fill;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            if (fill)
                g.FillEllipse(BrushWhenCxt(dcxt).Brush, dcxt.X_V2S(x - w / 2), dcxt.Y_V2S(y - h / 2), dcxt.W_V2S(w), dcxt.W_V2S(h));
            else
                g.DrawEllipse(PenWhenCxt(dcxt).Pen, dcxt.X_V2S(x - w / 2), dcxt.Y_V2S(y - h / 2), dcxt.W_V2S(w), dcxt.W_V2S(h));
        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && (fill && Assert.NotNull("»­±Ê", Pen) || !fill && Assert.NotNull("»­Ë¢", Brush));
        }
    }

    /// <summary>
    /// Ê¸Á¿¾ØÐÎ¿ò
    /// </summary>
    public class VectorBox : VectorObject
    {
        private float x, y, w, h;
        private bool fill = false;
        public VectorBox(float x, float y, float w, float h, bool fill)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.fill = fill;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            if (fill)
                g.FillRectangle(BrushWhenCxt(dcxt).Brush, dcxt.X_V2S(x), dcxt.Y_V2S(y), dcxt.W_V2S(w), dcxt.W_V2S(h));
            else
                g.DrawRectangle(PenWhenCxt(dcxt).Pen, dcxt.X_V2S(x), dcxt.Y_V2S(y), dcxt.W_V2S(w), dcxt.W_V2S(h));

        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && (fill && Assert.NotNull("»­±Ê", Pen) || !fill && Assert.NotNull("»­Ë¢", Brush));
        }
    }

    /// <summary>
    /// Ê¸Á¿ÎÄ×Ö
    /// </summary>
    public class VectorText : VectorObject
    {
        private float x, y;
        private String text;
        public VectorText(float x, float y, String text)
        {
            this.x = x;
            this.y = y;
            this.text = text;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            g.DrawString(text, FontWhenCxt(dcxt).Font, BrushWhenCxt(dcxt).Brush, dcxt.X_V2S(x), dcxt.Y_V2S(y));
        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && Assert.NotNull("×ÖÌå", Font) && Assert.NotNull("»­Ë¢", Brush);
        }
    }


    /// <summary>
    /// Ê¸Á¿ÈÝÆ÷£¨×éºÏÍ¼ÐÎ£©
    /// </summary>
    public class VectorComplex : VectorObject
    {
        private float x, y;
        private List<VectorObject> Children = null;
        public VectorComplex(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.Children = new List<VectorObject>();
        }
        public override void RenderObject(Graphics g, DrawContext dcxt)
        {
            DrawContext ndcxt = new DrawContext(dcxt.Scale, dcxt.X_V2S(x), dcxt.Y_V2S(y));
            foreach (VectorObject vo in Children)
            {
                if(vo.Display)
                    vo.RenderObject(g, ndcxt);
            }
        }
        public void AddChild(VectorObject vo)
        {
            vo.Parent = this;
            this.Children.Add(vo);
        }

        public override bool CheckRequire()
        {
            bool flag = base.CheckRequire();
            if (flag)
            {
                foreach (VectorObject vo in Children)
                {
                    flag = vo.CheckRequire();
                    if (!flag)
                        break;
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Ê¸Á¿±ýÍ¼
    /// </summary>
    public class VectorPie : VectorObject
    {
        private float x, y, w, h, startagl, sweepagl;
        private bool fill = false;
        public VectorPie(float x, float y, float w, float h, float startagl, float sweepagl, bool fill)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.startagl = startagl;
            this.sweepagl = sweepagl;
            this.fill = fill;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            if (fill)
                g.FillPie(BrushWhenCxt(dcxt).Brush, dcxt.X_V2S(x-w/2), dcxt.Y_V2S(y-h/2), dcxt.W_V2S(w), dcxt.W_V2S(h), startagl, sweepagl);
            else
                g.DrawPie(PenWhenCxt(dcxt).Pen, dcxt.X_V2S(x - w / 2), dcxt.Y_V2S(y - h / 2), dcxt.W_V2S(w), dcxt.W_V2S(h), startagl, sweepagl);
        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && (fill && Assert.NotNull("»­±Ê", Pen) || !fill && Assert.NotNull("»­Ë¢", Brush));
        }
    }

    /// <summary>
    /// Ê¸Á¿»¡Ïß
    /// </summary>
    public class VectorArc : VectorObject
    {
        private float x, y, w, h, startagl, sweepagl;
        public VectorArc(float x, float y, float w, float h, float startagl, float sweepagl)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.startagl = startagl;
            this.sweepagl = sweepagl;
        }
        override public void RenderObject(Graphics g, DrawContext dcxt)
        {
            g.DrawArc(PenWhenCxt(dcxt).Pen, dcxt.X_V2S(x - w / 2), dcxt.Y_V2S(y - h / 2), dcxt.W_V2S(w), dcxt.W_V2S(h), startagl, sweepagl);
        }
        public override bool CheckRequire()
        {
            return base.CheckRequire() && Assert.NotNull("»­±Ê", Pen);
        }
    }
}

