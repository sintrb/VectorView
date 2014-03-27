using System;
using System.Collections.Generic;
using System.Text;

namespace Sin.VectorView
{
    /// <summary>
    /// ╩Фм╪иообнд
    /// </summary>
    public class DrawContext
    {
        public float Scale = 1.0f;
        public float OffsetX = 0.0f;
        public float OffsetY = 0.0f;

        public DrawContext()
        {

        }
        public DrawContext(float scale, float ofx, float ofy)
        {
            this.Scale = scale;
            this.OffsetX = ofx;
            this.OffsetY = ofy;
        }

        public float X_V2S(float x)
        {
            return OffsetX + x * Scale;
        }
        public float Y_V2S(float y)
        {
            return OffsetY + y * Scale;
        }
        public float W_V2S(float w)
        {
            return w * Scale;
        }
        public float H_V2S(float h)
        {
            return h * Scale;
        }
        public float X_S2V(float x)
        {
            return (x - OffsetX) / Scale;
        }
        public float Y_S2V(float y)
        {
            return (y - OffsetY) / Scale;
        }
    }
}
