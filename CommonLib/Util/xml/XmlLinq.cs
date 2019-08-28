using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using CommonLib.Util.log;

namespace CommonLib.Util.xml
{
    public class XmlLinq : IXml
    {
        private readonly string _xmlFullPath;
        private readonly XElement _xElement;
        public XmlLinq(string xmlFullPath)
        {
            _xmlFullPath = xmlFullPath;
            _xElement = XElement.Load(xmlFullPath);
            //Logger.LogThrowException(String.Format("Failed to get xml from [{0}].", xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
        }
        public XElement GetXElement()
        {
            return _xElement;
        }
        public T GetXmlLoad<T>(string xmlFullPath = "")
        {
            try
            {
                //T _T = default(T);
                //this.xmlFullPath = xmlFullPath;
                //var o = XDocument.Load(xmlFullPath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
                //_T = (T)Convert.ChangeType(o, typeof(T));
                //return _T; 
                return (T)Convert.ChangeType(XDocument.Load(xmlFullPath, LoadOptions.SetBaseUri | LoadOptions.SetLineInfo), typeof(T));
            }
            catch (Exception)
            {
                return default(T);
            }
            
            //return null;
        }
        public void CreateXmlWithSameNodeNameList(string rootName, string nodeName, IEnumerable<string> nodeValueList)
        {
            var xElementRoot = new XElement(rootName);
            foreach (var item in nodeValueList)
            {
                xElementRoot.Add(new XElement(nodeName, item));
            }
            xElementRoot.Save(_xmlFullPath);
        }
        public string GetXmlFullPathFromXmlLoad<T>(T t)
        {
            try
            {
                return ((XDocument)Convert.ChangeType(t, typeof(T))).BaseUri.Replace("file:///", "").Replace("/", "\\");
            }
            catch (Exception ex)
            {
                Logger.LogThrowException("Failed to Get BaseUri.", new StackFrame(0).GetMethod().Name, ex.Message);
            }
            return "";
        }
        public T CreateAndGetXmlDoc<T>(string rootNode)
        {
            try
            {
                var xDocument = new XDocument(
                    new XElement(rootNode)
                    );
                //return _XDocument;
                return (T)Convert.ChangeType(xDocument, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
                //Logger.LogThrowException(String.Format("Failed to create Xml doc. RootNode is [{0}].", rootNode), new StackFrame(0).GetMethod().Name, ex.Message);
            }
            
        }
        public void SetClassFromXml<T>(T t)
        {
            IEnumerable<PropertyInfo> propertyInfos = t.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                try
                {
                    propertyInfo.SetValue(t, GetValue(propertyInfo.GetValue((t)).ToString()));
                }
                catch (Exception)
                {
                    //Logger.LogThrowMessage(String.Format("Failed to find xml node {0} in [{1}].", _PropertyInfo.GetValue(_T).ToString(), xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
                }
            }
        }
        public void SaveXmlDocToXml<T>(T t, string xmlFullPath)
        {
            try
            {
                var xDocument = ((XDocument)Convert.ChangeType(t, typeof(T)));
                xDocument.Save(xmlFullPath);
            }
            catch (Exception)
            {
                //Logger.LogThrowMessage(String.Format("Failed to save xml [{0}].", xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public void SetClassFromXmlDoc<T, C>(T t, C c, string node = "")
        {
            var xDocument = ((XDocument)Convert.ChangeType(t, typeof(T)));
            var xElement = node.Equals("") ? xDocument.Root : xDocument.Element(node);
            IEnumerable<PropertyInfo> propertyInfos = c.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                try
                {    
                    propertyInfo.SetValue(c, xElement.Element(propertyInfo.GetValue((c)).ToString()).Value);
                }
                catch (Exception)
                {
                    //Logger.LogThrowMessage(String.Format("Failed to find xml node {0} in [{1}].", _PropertyInfo.GetValue(_C).ToString(), xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
                }
            }
        }
        public void SetXmlFromClass<T, C>(T t, C c, string node = "")
        {
            var xDocument = ((XDocument)Convert.ChangeType(t, typeof(T)));
            var xElement = node.Equals("") ? xDocument.Root : xDocument.Element(node);
            IEnumerable<FieldInfo> fieldInfos = c.GetType().GetFields();
            foreach (var fieldInfo in fieldInfos)
            {
                try
                {
                    xElement.Add(new XElement(fieldInfo.Name, fieldInfo.GetValue(c)));
                }
                catch (Exception)
                {
                    //Logger.LogThrowMessage(String.Format("Failed to Set Xml From Class."), new StackFrame(0).GetMethod().Name, ex.Message);
                }
            }
        }
        public void SetAttributeToXmlDoc<T>(T t, string attributeName, string attributeValue, string node = "")
        {
            var xDocument = ((XDocument)Convert.ChangeType(t, typeof(T)));
            var xElement = node.Equals("") ? xDocument.Root : xDocument.Element(node);
            xElement.SetAttributeValue(attributeName, attributeValue);
        }
        public string [] GetValuesArray(string nodeName = null)
        {
            try
            {
                return GetElementsIEnumerable(nodeName).ToArray();
            }
            catch (Exception)
            {
                //Logger.LogThrowException(String.Format("Failed to get values array."), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
        public List<string> GetValuesList(string nodeName = null)
        {
            try
            {
                return GetElementsIEnumerable(nodeName).ToList();
            }
            catch (Exception)
            {
                //Logger.LogThrowException(String.Format("Failed to get values List."), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
        public IEnumerable<string> GetElementsIEnumerable(string nodeName = null)
        {
            //IEnumerable<XElement> elements = _XElement.Elements();
            //return elements.Select(x => x.Value).ToList<string>();
            try
            {
                if (string.IsNullOrEmpty(nodeName))
                {
                    return from x in _xElement.Elements() select x.Value;
                }
                return from x in _xElement.Elements(nodeName) select x.Value;
            }
            catch (Exception)
            {
                //Logger.LogThrowException(String.Format("Failed to get elements IEnumerable."), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
        public void SetValue<T>(T t, string nodeName = null, string nodeValue = "")
        {
            try
            {
                var xDocument = ((XDocument)Convert.ChangeType(t, typeof(T)));
                //bookVar = xml.Descendants("book").Where(a => a.Element("title").Value == param.Title);
                var xElement = nodeName.Equals("") ? xDocument.Root : xDocument.Descendants(nodeName).First(a => a.Name.LocalName.Equals(nodeName));
                xElement.Value = nodeValue;
            }
            catch (Exception)
            {
               //Logger.LogThrowException(String.Format("Failed to set node value."), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public string GetValue(string nodeName = null)
        {
            try
            {
                return _xElement.Element(nodeName)?.Value ;
            }
            catch (Exception)
            {
                //Logger.LogThrowException(String.Format("Failed to get node value."), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
        //public void test(string nodeName = null)
        //{
        //    var xDoc = new XDocument(new XElement("root",
        //   new XElement("dog",
        //       new XText("dog said black is a beautify color"),
        //       new XAttribute("color", "black")),
        //   new XElement("cat"),
        //   new XElement("pig", "pig is great")));

        //    //xDoc输出xml的encoding是系统默认编码，对于简体中文操作系统是gb2312
        //    //默认是缩进格式化的xml，而无须格式化设置
        //    xDoc.Save(Console.Out);

        //    Console.WriteLine();

        //    var query = from item in xDoc.Element("root").Elements()
        //                select new
        //                {
        //                    TypeName = item.Name,
        //                    Saying = item.Value,
        //                    Color = item.Attribute("color") == null ? (string)null : item.Attribute("color").Value
        //                };


        //    foreach (var item in query)
        //    {
        //        Console.WriteLine("{0} 's color is {1},{0} said {2}", item.TypeName, item.Color ?? "Unknown", item.Saying ?? "nothing");
        //    }

        //    Console.Read();
        //}
        //    var query = new XElement("root",
        //from p in xml.Elements("node")
        //from a in p.Attributes()
        //select new XElement(a.Name,
        //    new XAttribute("content", a.Value)
        //    )
        //);
        //    var query = new XElement("root",
        //    xml.Elements("node")
        //       .SelectMany(n => n.Attributes())
        //       .Select(a => new XElement(a.Name,
        //            new XAttribute("content", a.Value))));
    }    
}
