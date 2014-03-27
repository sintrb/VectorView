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
    /// 解析工具类
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
                        throw new Exception(String.Format("图形节点{0}的预定义{1}已存在!", shape.OuterXml, tag));
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
                        throw new Exception(String.Format("值节点{0}的预定义{1}已存在!", val.OuterXml, tag));
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
                        throw new Exception(String.Format("画刷节点{0}的预定义{1}已存在!", brush.OuterXml, tag));
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
                        throw new Exception(String.Format("画笔节点{0}的预定义{1}已存在!", pen.OuterXml, tag));
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
                        throw new Exception(String.Format("字体{0}的预定义{1}已存在!", font.OuterXml, tag));
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

                // 解析完毕之后检查属性是否足够
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

            // 预定义预定义图形
            ParseShapes(cxt, dom);

            // 预定于值
            ParseValues(cxt, dom);

            // 预定于画刷
            ParseBrushes(cxt, dom);

            // 预定于画笔
            ParsePens(cxt, dom);

            // 预定于字体
            ParseFonts(cxt, dom);

            // 解析所有图形数据
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
