using System;
using System.Collections.Generic;
using System.Text;


using System.Drawing;
namespace Sin.VectorView
{
    /// <summary>
    /// ʸ����Ⱦ
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
        /// ���ű���
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
        /// ������ƫ��
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
        /// ������ƫ��
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

        // �ڲ�������
        private Font font = new Font(new FontFamily("΢���ź�"), 14);

        /// <summary>
        /// ��ͼ����Ⱦ����ͼͼ����
        /// </summary>
        /// <param name="g">��ͼͼ��</param>
        /// /// <param name="size">�����ߴ�</param>
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
        /// ��(x,y)Ϊ���㣬���ڵ���ǰ���ű�����k�������ڳɹ�����true������ʧ�ܷ���false���������Ѿ������ڼ��ޣ�
        /// </summary>
        /// <param name="k">����ϵ��</param>
        /// <param name="x">���������</param>
        /// <param name="y">����������</param>
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
