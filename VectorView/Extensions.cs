using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
namespace Sin.VectorView
{
    /// <summary>
    /// 内部用扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 不定参数形式删除XmlNode的属性值
        /// </summary>
        /// <param name="node">需删除属性的XmlNode节点</param>
        /// <param name="names">需删除的属性名称</param>
        /// <returns>删除的属性个数</returns>
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
