using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;

namespace Sin.VectorView
{
    public class VectorSvgUtils
    {
        public static String Pen2Style(Pen pen)
        {
            return String.Format("stroke:rgb({0},{1},{2});stroke-width:{3};", 0x00ff & pen.Color.R, 0x00ff & pen.Color.G, 0x00ff & pen.Color.B, pen.Width);
        }

        public static String Brush2Style(Brush brush)
        {
            if (brush is SolidBrush)
            {
                SolidBrush sb = (SolidBrush)brush;
                return String.Format("fill:rgb({0},{1},{2});", 0x00ff & sb.Color.R, 0x00ff & sb.Color.G, 0x00ff & sb.Color.B);
            }
            else
            {
                return "fill:none;";
            }
        }
    }
}
