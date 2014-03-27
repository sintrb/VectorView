using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
namespace Sin.VectorView
{
    /// <summary>
    /// �ڲ�����չ
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ����������ʽɾ��XmlNode������ֵ
        /// </summary>
        /// <param name="node">��ɾ�����Ե�XmlNode�ڵ�</param>
        /// <param name="names">��ɾ������������</param>
        /// <returns>ɾ�������Ը���</returns>
        public static int XmlNoeRemoveAttributes(XmlNode node, params String[] names)
        {
            int count = 0;
            foreach (String name in names)
            {
                XmlAttribute xa = node.Attributes[name];
                if (xa != null)
                {
                    node.Attributes.Remove(xa);
                    ++count;
                }
            }
            return count;
        }
    }
}
