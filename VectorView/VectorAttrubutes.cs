using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Sin.VectorView
{
    /// <summary>
    /// 画刷
    /// </summary>
    public class ContextBrush
    {
        public Brush Brush = null;
        public ContextBrush()
        {
        }
        public ContextBrush(Brush brush)
        {
            this.Brush = brush;
        }
    }
    /// <summary>
    /// 画笔
    /// </summary>
    public class ContextPen
    {
        public Pen Pen = null;
        public ContextPen()
        {
        }
        public ContextPen(Pen pen)
        {
            this.Pen = pen;
        }

        public ContextPen ScaleTo(float scale)
        {
            float nw = this.Pen.Width * scale;
            if (nw < 0.1)
                nw = 0.1f;
            return new ContextPen(new Pen(this.Pen.Brush, nw));
        }
    }
    /// <summary>
    /// 字体
    /// </summary>
    public class ContextFont
    {
        public Font Font = null;

        public ContextFont()
        {
        }
        public ContextFont(Font font)
        {
            this.Font = font;
        }


        public ContextFont ScaleTo(float scale)
        {
            float fw = Font.Size * scale;
            if (fw < 1)
                fw = 1f;
            return new ContextFont(new Font(this.Font.FontFamily, fw));
        }
    }

    /// <summary>
    /// 上下文属性
    /// </summary>
    public class ContextAttributes
    {
        private Hashtable Table = new Hashtable();
        public Object this[String key]
        {
            get
            {
                return Table != null && Table.Contains(key) ? Table[key] : null;
            }
            set
            {
                if (Table == null)
                {
                    Table = new Hashtable();
                }
                Table[key] = value;
            }
        }

        public ContextBrush Brush
        {
            get
            {
                return (ContextBrush)this["brush"];
            }
        }

        public ContextFont Font
        {
            get
            {
                return (ContextFont)this["font"];
            }
        }

        public ContextPen Pen
        {
            get
            {
                return (ContextPen)this["pen"];
            }
        }

        public int Count
        {
            get
            {
                return Table != null ? Table.Count : 0;
            }
        }
    }
}
