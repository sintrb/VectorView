using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Reflection;

namespace Sin.VectorView
{
    /// <summary>
    /// 解析上下文
    /// </summary>
    public class ParseContext
    {
        public Hashtable Defines = new Hashtable();

        public Hashtable Values = new Hashtable();
        public Hashtable Shapes = new Hashtable();

        private float Scale = 1.0f;
        private List<String> ParseStack = new List<string>();


        public List<String> CurIgnorAttrs = null;
        public List<String> ValueStack = new List<string>();
        public String DeepValOf(String val, XmlNode node, VectorObject prt)
        {
            if (ValueStack.Contains(val))
            {
                StringBuilder sb = new StringBuilder();
                foreach(String v in ValueStack){
                    sb.Append(v);
                    sb.Append(">");
                }
                sb.Append(val);
                throw new Exception("存在值递归:" + sb.ToString());
            }
            ValueStack.Add(val);   
            String retval = val;
            if (val != null && val.Length>0)
            {
                String key = val.Substring(1);
                switch (val[0])
                {
                    case '$':
                        if (this.Values.Contains(key))
                        {
                            retval = DeepValOf(((XmlNode)this.Values[key]).InnerText, node, prt);
                        }
                        else
                        {
                            throw new Exception(String.Format("预定义值{0}不存在!", key));
                        }
                        break;
                    case '#':
                        if (node != null && node.Attributes[key] != null)
                        {
                            retval = DeepValOf(node.Attributes[key].Value, node, prt);
                        }
                        else if (prt != null && prt.Attributes!=null && prt.Attributes[key] != null)
                        {
                            retval = DeepValOf(prt.Attributes[key].ToString(), node, prt);
                        }
                        else
                        {
                            throw new Exception(String.Format("{0}的属性{1}不存在!", node!=null?node.OuterXml:"PRT", key));
                        }
                        break;
                }
            }
            ValueStack.Remove(val);
            return retval;
        }

        public String Escaped(String s)
        {
            return s.Replace("\\\\", "__\\\\__").Replace("\\n", "\n").Replace("__\\\\__", "\\");
        }

        public Object GetDefine(String name)
        {
            return Defines[name];
        }

        public String AttrOf(XmlNode node, String name)
        {
            if (node.Attributes[name] != null)
            {
                return DeepValOf(node.Attributes[name].Value, node, CurParent);
            }
            else
                throw new Exception(String.Format("节点{0}不存在属性{1}", node.OuterXml, name));
        }

        public Object DefineOf(XmlNode node, String name)
        {
            return GetDefine(AttrOf(node, name));
        }

        public float FloatAttr(XmlNode node, String name)
        {
            return float.Parse(AttrOf(node, name));
        }

        public bool BooltAttr(XmlNode node, String name)
        {
            return node.Attributes[name]!=null && bool.Parse(AttrOf(node, name));
        }

        public Color ColorAttr(XmlNode node, String name)
        {
            return Color.FromArgb(Convert.ToInt32(AttrOf(node, name), 16));
        }

        public ContextBrush BrushAttr(XmlNode node, String name)
        {
            return ((ContextBrush)GetDefine(AttrOf(node, name)));
        }

        public bool HasDefine(String name)
        {
            return this.Defines.Contains(name);
        }

        public void AddDefine(String name, Object obj)
        {
            this.Defines.Add(name, obj);
        }

        // 生成
        private static Hashtable HatchStyles = null;
        public ContextBrush ParseToBrush(XmlNode node)
        {
            Brush brush = null;
            switch (node.Attributes["type"].Value)
            {
                case "solid":
                    brush = new SolidBrush(ColorAttr(node, "color"));
                    break;
                case "hatch":
                    if (HatchStyles == null)
                    {
                        HatchStyles = new Hashtable();
                        foreach (FieldInfo fi in typeof(HatchStyle).GetFields(BindingFlags.Static | BindingFlags.Public))
                        {
                            HatchStyles.Add(fi.Name.ToLower(), fi.GetRawConstantValue());
                        }
                    }
                    String hstyle = AttrOf(node, "style").ToLower();
                    if (!HatchStyles.Contains(hstyle))
                    {
                        throw new Exception(String.Format("Hatch画刷{0}的样式{1}不存在", node.OuterXml, AttrOf(node, "style")));
                    }
                    brush = new HatchBrush((HatchStyle)HatchStyles[hstyle], ColorAttr(node, "forecolor"), ColorAttr(node, "backcolor"));
                    break;
            }

            ContextBrush cb = new ContextBrush();
            cb.Brush = brush;
            return cb;
        }

        public ContextPen ParseToPen(XmlNode node)
        {
            ContextBrush cb = null;
            if (node.Attributes["brush"] == null)
            {
                cb = ParseToBrush(node);
            }
            else
            {
                cb = BrushAttr(node, "brush");
            }

            Pen pen = new Pen(cb.Brush, FloatAttr(node, "width"));
            ContextPen cp = new ContextPen();
            cp.Pen = pen;
            return cp;
        }

        public ContextFont ParseToFont(XmlNode node)
        {
            Font font = new Font(AttrOf(node, "name"), FloatAttr(node, "size"));
            ContextFont cf = new ContextFont();
            cf.Font = font;
            return cf;
        }


        private VectorObject CurParent = null;
        public VectorObject ParseToVector(XmlNode node, ParseCallback cbk)
        {
            if (cbk != null && cbk.BeforeParse != null && cbk.BeforeParse(this, node) == false)
                return null;
            String tag = node.Name;
            if (ParseStack.Contains(tag))
            {
                StringBuilder sb = new StringBuilder();
                foreach (String s in ParseStack)
                {
                    sb.Append(s);
                    sb.Append(">");
                }
                sb.Append(tag);
                throw new Exception("存在图形递归:"+sb.ToString());
            }
            ParseStack.Add(tag);

            VectorObject vo = null;
            List<String> IgnorAttrs = new List<string>();
            float pscale = this.Scale;
            switch (tag)
            {
                case "line":
                    vo = new VectorLine(FloatAttr(node, "x1") * Scale, FloatAttr(node, "y1") * Scale, FloatAttr(node, "x2") * Scale, FloatAttr(node, "y2") * Scale);
                    IgnorAttrs.Add("x1");
                    IgnorAttrs.Add("y1");
                    IgnorAttrs.Add("x2");
                    IgnorAttrs.Add("y2");
                    break;
                case "box":
                    vo = new VectorBox(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale, FloatAttr(node, "w") * Scale, FloatAttr(node, "h") * Scale, BooltAttr(node, "fill"));
                    IgnorAttrs.Add("x");
                    IgnorAttrs.Add("y");
                    IgnorAttrs.Add("w");
                    IgnorAttrs.Add("h");
                    IgnorAttrs.Add("fill");
                    break;
                case "ellipse":
                case "circle":
                    if (node.Attributes["r"] != null)
                        vo = new VectorCircle(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale, FloatAttr(node, "r") * Scale, FloatAttr(node, "r") * Scale, BooltAttr(node, "fill"));
                    else
                        vo = new VectorCircle(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale, FloatAttr(node, "w") * Scale, FloatAttr(node, "h") * Scale, BooltAttr(node, "fill"));
                    IgnorAttrs.Add("x");
                    IgnorAttrs.Add("y");
                    IgnorAttrs.Add("r");
                    IgnorAttrs.Add("fill");
                    break;
                case "text":
                    vo = new VectorText(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale, Escaped(AttrOf(node, "text")));
                    IgnorAttrs.Add("x");
                    IgnorAttrs.Add("y");
                    IgnorAttrs.Add("text");
                    break;
                case "pie":
                    vo = new VectorPie(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale, FloatAttr(node, "w") * Scale, FloatAttr(node, "h") * Scale, FloatAttr(node, "sta"), FloatAttr(node, "swa"), BooltAttr(node, "fill"));
                    break;
                case "arc":
                    vo = new VectorArc(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale, FloatAttr(node, "w") * Scale, FloatAttr(node, "h") * Scale, FloatAttr(node, "sta"), FloatAttr(node, "swa"));
                    break;
                case "path":
                    if (node.HasChildNodes && node.ChildNodes.Count > 2)
                    {
                        float sx = FloatAttr(node, "x");
                        float sy = FloatAttr(node, "y");
                        List<Object> pathes = new List<Object>(node.ChildNodes.Count);
                        foreach (XmlNode ch in node.ChildNodes)
                        {
                            if (ch is System.Xml.XmlComment)
                                continue;
                            switch (ch.Name)
                            {
                                case "point":
                                    pathes.Add(new VectorPathLine(FloatAttr(ch, "x") * Scale, FloatAttr(ch, "y") * Scale, FloatAttr(ch, "x") * Scale, FloatAttr(ch, "y") * Scale));
                                    break;
                                case "line":
                                    pathes.Add(new VectorPathLine(FloatAttr(ch, "x1") * Scale, FloatAttr(ch, "y1") * Scale, FloatAttr(ch, "x2") * Scale, FloatAttr(ch, "y2") * Scale));
                                    break;
                                case "arc":
                                    pathes.Add(new VectorPathArc(FloatAttr(ch, "x") * Scale, FloatAttr(ch, "y") * Scale, FloatAttr(ch, "w") * Scale, FloatAttr(ch, "h") * Scale, FloatAttr(ch, "sta"), FloatAttr(ch, "swa")));
                                    break;
                                case "pie":
                                    pathes.Add(new VectorPathPie(FloatAttr(ch, "x") * Scale, FloatAttr(ch, "y") * Scale, FloatAttr(ch, "w") * Scale, FloatAttr(ch, "h") * Scale, FloatAttr(ch, "sta"), FloatAttr(ch, "swa")));
                                    break;
                            }
                        }
                        vo = new VectorPath(pathes, BooltAttr(node, "fill"));
                        IgnorAttrs.Add("x");
                        IgnorAttrs.Add("y");
                        IgnorAttrs.Add("fill");
                    }
                    else
                    {
                        throw new Exception(String.Format("路径节点{0}必须有至少两个子节点.", node.OuterXml));
                    }
                    break;
                default:
                    // 首先尝试使用扩展解析
                    // 如果扩展解析失败，则认为该节点为复合图形
                    List<String> oIgnorAttrs = this.CurIgnorAttrs;
                    this.CurIgnorAttrs = IgnorAttrs;
                    vo = cbk != null && cbk.ExtensionParse != null ? cbk.ExtensionParse(this, node) : null;
                    if (vo != null)
                        break;
                    this.CurIgnorAttrs = oIgnorAttrs;
                    // 扩展解析失败，判断是否存在该复合图形
                    if (!Shapes.Contains(tag))
                        throw new Exception(String.Format("未定义图形{0}", tag));

                    // 解析复合图形
                    VectorComplex vc = new VectorComplex(FloatAttr(node, "x") * Scale, FloatAttr(node, "y") * Scale);
                    IgnorAttrs.Add("x");
                    IgnorAttrs.Add("y");

                    // 对于符合图形需要先加
                    AddExtAttribute(IgnorAttrs, node, vc);

                    float scl = this.Scale;
                    
                    this.Scale *= node.Attributes["scale"] == null ? 1.0f : FloatAttr(node, "scale");
                    foreach (XmlNode cnode in ((XmlNode)Shapes[tag]).ChildNodes)
                    {
                        VectorObject ovo = CurParent;
                        CurParent = vc;
                        VectorObject cvo = this.ParseToVector(cnode, cbk);
                        if (cvo != null)
                            vc.AddChild(cvo);
                        CurParent = ovo;
                    }
                    this.Scale = scl;
                    vo = vc;
                    break;
            }
            vo.ScaleWeight = pscale;
            if (vo!=null && vo.Attributes == null)
                AddExtAttribute(IgnorAttrs, node, vo);
            ParseStack.Remove(tag);

            if (node.Attributes["display"]!=null)
                vo.Display = BooltAttr(node, "display");

            if (cbk != null && cbk.AfterParse != null && cbk.AfterParse(this, node, vo) == false)
                return null;
            else
            {
                return vo;
            }
        }

        private void AddExtAttribute(List<String> IgnorAttrs, XmlNode node, VectorObject vo)
        {
            ContextAttributes atts = new ContextAttributes();
            foreach (XmlAttribute xa in node.Attributes)
            {
                if (!IgnorAttrs.Contains(xa.Name))
                {
                    // 优先考虑定义
                    Object df = GetDefine(DeepValOf(xa.Value, node, CurParent));

                    // 如果没有定义的话在考虑使用预定义值
                    atts[xa.Name] = df != null ? df : DeepValOf(xa.Value, node, CurParent);
                }
            }
            if (atts.Count != 0)
                vo.Attributes = atts;
        }
    }
}
