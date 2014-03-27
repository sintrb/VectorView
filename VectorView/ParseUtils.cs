using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Sin.VectorView
{
    /// <summary>
    /// ����������
    /// </summary>
    public class ParseUtils
    {
        private static void ParseShapes(ParseContext cxt, XmlDocument dom)
        {
            foreach (XmlNode shapes in dom.GetElementsByTagName("shapes"))
            {
                if (shapes is System.Xml.XmlComment)
                    continue;
                foreach (XmlNode shape in shapes.ChildNodes)
                {
                    if (shape is System.Xml.XmlComment)
                        continue;
                    String tag = shape.Name;
                    if (cxt.Shapes.Contains(tag))
                    {
                        throw new Exception(String.Format("ͼ�νڵ�{0}��Ԥ����{1}�Ѵ���!", shape.OuterXml, tag));
                    }
                    cxt.Shapes.Add(tag, shape);
                }
            }
        }

        private static void ParseValues(ParseContext cxt, XmlDocument dom)
        {
            foreach (XmlNode values in dom.GetElementsByTagName("values"))
            {
                if (values is System.Xml.XmlComment)
                    continue;
                foreach (XmlNode val in values.ChildNodes)
                {
                    if (val is System.Xml.XmlComment)
                        continue;
                    String tag = val.Name;
                    if (cxt.Values.Contains(tag))
                    {
                        throw new Exception(String.Format("ֵ�ڵ�{0}��Ԥ����{1}�Ѵ���!", val.OuterXml, tag));
                    }
                    cxt.Values.Add(tag, val);
                }
            }
        }

        private static void ParseBrushes(ParseContext cxt, XmlDocument dom)
        {
            foreach (XmlNode brushes in dom.GetElementsByTagName("brushes"))
            {
                if (brushes is System.Xml.XmlComment)
                    continue;
                foreach (XmlNode brush in brushes.ChildNodes)
                {
                    if (brush is System.Xml.XmlComment)
                        continue;
                    String tag = brush.Name;
                    if (cxt.HasDefine(tag))
                    {
                        throw new Exception(String.Format("��ˢ�ڵ�{0}��Ԥ����{1}�Ѵ���!", brush.OuterXml, tag));
                    }
                    cxt.AddDefine(tag, cxt.ParseToBrush(brush));
                }
            }
        }

        private static void ParsePens(ParseContext cxt, XmlDocument dom)
        {
            foreach (XmlNode pens in dom.GetElementsByTagName("pens"))
            {
                if (pens is System.Xml.XmlComment)
                    continue;
                foreach (XmlNode pen in pens.ChildNodes)
                {
                    if (pen is System.Xml.XmlComment)
                        continue;
                    String tag = pen.Name;
                    if (cxt.HasDefine(tag))
                    {
                        throw new Exception(String.Format("���ʽڵ�{0}��Ԥ����{1}�Ѵ���!", pen.OuterXml, tag));
                    }
                    cxt.AddDefine(tag, cxt.ParseToPen(pen));
                }
            }
        }

        private static void ParseFonts(ParseContext cxt, XmlDocument dom)
        {
            foreach (XmlNode fonts in dom.GetElementsByTagName("fonts"))
            {
                if (fonts is System.Xml.XmlComment)
                    continue;
                foreach (XmlNode font in fonts.ChildNodes)
                {
                    if (font is System.Xml.XmlComment)
                        continue;
                    String tag = font.Name;
                    if (cxt.HasDefine(tag))
                    {
                        throw new Exception(String.Format("����{0}��Ԥ����{1}�Ѵ���!", font.OuterXml, tag));
                    }
                    cxt.AddDefine(tag, cxt.ParseToFont(font));
                }
            }
        }

        private static List<VectorObject> ParseData(ParseContext cxt, XmlDocument dom, ParseCallback cbk)
        {
            XmlNode objects = dom.GetElementsByTagName("vectors").Item(0);
            List<VectorObject> list = new List<VectorObject>(objects.ChildNodes.Count);
            foreach (XmlNode obj in objects)
            {
                if (obj is System.Xml.XmlComment)
                    continue;
                VectorObject vo = cxt.ParseToVector(obj, cbk);

                // �������֮���������Ƿ��㹻
                vo.CheckRequire();


                if (vo != null)
                    list.Add(vo);
            }
            return list;
        }

        public static List<VectorObject> ParseXMLDom(XmlDocument dom)
        {
            return ParseXMLDom(dom, null);
        }
        public static List<VectorObject> ParseXMLDom(XmlDocument dom, ParseCallback cbk)
        {
            ParseContext cxt = new ParseContext();

            // Ԥ����Ԥ����ͼ��
            ParseShapes(cxt, dom);

            // Ԥ����ֵ
            ParseValues(cxt, dom);

            // Ԥ���ڻ�ˢ
            ParseBrushes(cxt, dom);

            // Ԥ���ڻ���
            ParsePens(cxt, dom);

            // Ԥ��������
            ParseFonts(cxt, dom);

            // ��������ͼ������
            return ParseData(cxt, dom, cbk);
        }

        public static List<VectorObject> ParseXMLFile(String xmlfile)
        {
            return ParseXMLFile(xmlfile, null);
        }
        public static List<VectorObject> ParseXMLFile(String xmlfile, ParseCallback cbk)
        {
            XmlDocument dom = new XmlDocument();
            dom.Load(xmlfile);
            return ParseXMLDom(dom,cbk);
        }

        public static List<VectorObject> ParseXMLString(String xml)
        {
            return ParseXMLString(xml, null);
        }
        public static List<VectorObject> ParseXMLString(String xml, ParseCallback cbk)
        {
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(xml);
            return ParseXMLDom(dom, cbk);
        }
    }
}
