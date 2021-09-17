using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wynncs
{
    public class XmlUtil
    {
        public static XmlDocument Open(string filePath = "", bool nullIfNotExist = false)
        {
           XmlDocument doc = new XmlDocument();
           try
            {
                doc.Load(filePath);
                return doc;
            }
            catch (Exception)
            {
                return nullIfNotExist ? null : doc;
            }
        }

        public static XmlElement GetElement(XmlNode node, string elementName)
        {
            return node?[elementName];
        }

        public static XmlElement GetElement(XmlNode node, int elementIndex)
        {
            return node?.ChildNodes[elementIndex] as XmlElement;
        }

        public static XmlAttribute GetAttribute(XmlNode node, string attrName)
        {
            return node?.Attributes?[attrName];
        }

        public static string GetAttributeString(XmlNode node, string attrName,string defaultValue = "")
        {
            var attr = GetAttribute(node, attrName);
            string value = attr == null ? defaultValue : attr.Value;
            return value;
        }

        public static int GetAttributeInt(XmlNode node, string attrName, int defaultValue = 0)
        {
            var attr = GetAttribute(node, attrName);
            if (attr == null || !Int32.TryParse(attr.Value, out int value)) value = defaultValue;
            return value;
        }

        public static float GetAttributeFloat(XmlNode node, string attrName, float defaultValue = 0f)
        {
            var attr = GetAttribute(node, attrName);
            if (attr == null || !Single.TryParse(attr.Value, out float value)) value = defaultValue;
            return value;
        }

        public static double GetAttributeDouble(XmlNode node, string attrName, double defaultValue = 0d)
        {
            var attr = GetAttribute(node, attrName);
            if (attr == null || !Double.TryParse(attr.Value, out double value)) value = defaultValue;
            return value;
        }

        public static bool GetAttributeBool(XmlNode node, string attrName, bool defaultValue = false)
        {
            var attr = GetAttribute(node, attrName);
            if (attr == null || !Boolean.TryParse(attr.Value, out bool value)) value = defaultValue;
            return value;
        }

        public static XmlElement AddElement(XmlNode node, string elementName)
        {
            var doc = node as XmlDocument ?? node.OwnerDocument;
            XmlElement element = doc.CreateElement(elementName);
            node.AppendChild(element);
            return element;
        }

        public static XmlAttribute SetAttribute(XmlNode node, string attrName, ValueType attrValue)
        {
            var doc = node as XmlDocument ?? node.OwnerDocument;
            XmlAttribute attr = doc.CreateAttribute(attrName);
            attr.Value = attrValue.ToString();
            node.Attributes.SetNamedItem(attr);
            return attr;
        }

        public static XmlAttribute SetAttribute(XmlNode node, string attrName, string attrValue = "")
        {
            var doc = node as XmlDocument ?? node.OwnerDocument;
            XmlAttribute attr = doc.CreateAttribute(attrName);
            attr.Value = attrValue;
            node.Attributes.SetNamedItem(attr);
            return attr;
        }

        public static void Statistics(XmlNode sourse, XmlDocument log)
        {
            var root = GetElement(log, "Elements") ?? AddElement(log, "Elements");
            if (sourse is XmlElement)
            {
                var element = GetElement(root, sourse.Name) ?? AddElement(root, sourse.Name);
                if (sourse.Attributes != null && sourse.Attributes.Count > 0)
                {
                    foreach (XmlAttribute attr in sourse.Attributes)
                    {
                        SetAttribute(element, attr.Name, GetAttributeInt(element, attr.Name) + 1);
                    }
                }
                foreach (var _child in sourse)
                {
                    var child = _child as XmlElement;
                    if (child != null)
                    {
                        var node = GetElement(element, child.Name) ?? AddElement(element, child.Name);
                        SetAttribute(node, "Count", GetAttributeInt(node, "Count") + 1);
                        Statistics(child, log);
                    }
                }
            }
            else if (sourse is XmlDocument)
            {
                foreach (var _child in sourse)
                {
                    var child = _child as XmlElement;
                    if (child != null)
                    {
                        SetAttribute(root, child.Name, GetAttributeInt(root, child.Name) + 1);
                        Statistics(child, log);
                    }
                }
            }
        }

        public static void Statistics(string sourseFilePath, string logFilePath, bool isAppend = false)
        {
            Directory.CreateDirectory(Path.GetFullPath(Path.GetDirectoryName(logFilePath)));
            var sourse = Open(sourseFilePath);
            var log = isAppend ? Open(logFilePath) : Open();
            Statistics(sourse, log);
            log.Save(logFilePath);
        }
    }

}
