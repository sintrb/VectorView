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
    /// �����ص��ӿ�
    /// </summary>
    public class ParseCallback
    {
        public BEFOREPARSE BeforeParse = null;  // ����ǰ���ã�����false�Ļ���ȡ���ýڵ�Ľ���
        public AFTERPARSE AfterParse = null;    // ��������ã�����false�Ļ��ͺ��Ըýڵ�Ľ������
        public EXTENSIONPARSE ExtensionParse = null;    // ���ڽ�����չ��ͼ��
        public ParseCallback(BEFOREPARSE before, AFTERPARSE after, EXTENSIONPARSE ext)
        {
            this.BeforeParse = before;
            this.AfterParse = after;
            this.ExtensionParse = ext;
        }
    }
}
