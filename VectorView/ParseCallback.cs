using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

namespace Sin.VectorView
{
    public delegate bool BEFOREPARSE(ParseContext cxt, XmlNode node);
    public delegate bool AFTERPARSE(ParseContext cxt, XmlNode node, VectorObject vo);
    public delegate VectorObject EXTENSIONPARSE(ParseContext cxt, XmlNode node);

    /// <summary>
    /// 解析回调接口
    /// </summary>
    public class ParseCallback
    {
        public BEFOREPARSE BeforeParse = null;  // 解析前调用，返回false的话就取消该节点的解析
        public AFTERPARSE AfterParse = null;    // 解析后调用，返回false的话就忽略该节点的解析结果
        public EXTENSIONPARSE ExtensionParse = null;    // 用于解析扩展的图形
        public ParseCallback(BEFOREPARSE before, AFTERPARSE after, EXTENSIONPARSE ext)
        {
            this.BeforeParse = before;
            this.AfterParse = after;
            this.ExtensionParse = ext;
        }
    }
}
