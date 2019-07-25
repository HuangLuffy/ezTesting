using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace CommonLib.Util.xml
{
    public class XmlLinq : IXml
    {
        string xmlFullPath = string.Empty;
        XElement _XElement = null;
        public XmlLinq()
        {
           
        }
        public XmlLinq(string xmlFullPath)
        {
            this.xmlFullPath = xmlFullPath;
            _XElement = XElement.Load(xmlFullPath);
            //Logger.LogThrowException(String.Format("Failed to get xml from [{0}].", xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
        }
        public XElement GetXElement()
        {
            return _XElement;
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
            XElement _XElementRoot = new XElement(rootName);
            foreach (var item in nodeValueList)
            {
                _XElementRoot.Add(new XElement(nodeName, item));
            }
            _XElementRoot.Save(xmlFullPath);
        }
        public String GetXmlFullPathFromXmlLoad<T>(T _T)
        {
            try
            {
                return ((XDocument)Convert.ChangeType(_T, typeof(T))).BaseUri.Replace("file:///", "").Replace("/", "\\");
            }
            catch (Exception ex)
            {
                Logger.LogThrowException(String.Format("Failed to Get BaseUri."), new StackFrame(0).GetMethod().Name, ex.Message);
            }
            return "";
        }
        public T CreateAndGetXmlDoc<T>(string rootNode)
        {
            try
            {
                XDocument _XDocument = new XDocument(
                    new XElement(rootNode)
                    );
                //return _XDocument;
                return (T)Convert.ChangeType(_XDocument, typeof(T));
            }
            catch (Exception)
            {
                return default(T);
                //Logger.LogThrowException(String.Format("Failed to create Xml doc. RootNode is [{0}].", rootNode), new StackFrame(0).GetMethod().Name, ex.Message);
            }
            
        }
        public void SetClassFromXml<T>(T _T)
        {
            IEnumerable<PropertyInfo> _PropertyInfos = _T.GetType().GetProperties();
            foreach (PropertyInfo _PropertyInfo in _PropertyInfos)
            {
                try
                {
                    _PropertyInfo.SetValue(_T, this.GetValue(_PropertyInfo.GetValue((_T)).ToString()));
                }
                catch (Exception)
                {
                    //Logger.LogThrowMessage(String.Format("Failed to find xml node {0} in [{1}].", _PropertyInfo.GetValue(_T).ToString(), xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
                }
            }
        }
        public void SaveXmlDocToXml<T>(T _T, string xmlFullPath)
        {
            try
            {
                XDocument _XDocument = ((XDocument)Convert.ChangeType(_T, typeof(T)));
                _XDocument.Save(xmlFullPath);
            }
            catch (Exception)
            {
                //Logger.LogThrowMessage(String.Format("Failed to save xml [{0}].", xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
        public void SetClassFromXmlDoc<T, C>(T _T, C _C, string node = "")
        {
            XDocument _XDocument = ((XDocument)Convert.ChangeType(_T, typeof(T)));
            XElement _XElement = node.Equals("") ? _XDocument.Root : _XDocument.Element(node);
            IEnumerable<PropertyInfo> _PropertyInfos = _C.GetType().GetProperties();
            foreach (PropertyInfo _PropertyInfo in _PropertyInfos)
            {
                try
                {    
                    _PropertyInfo.SetValue(_C, _XElement.Element(_PropertyInfo.GetValue((_C)).ToString()).Value);
                }
                catch (Exception)
                {
                    //Logger.LogThrowMessage(String.Format("Failed to find xml node {0} in [{1}].", _PropertyInfo.GetValue(_C).ToString(), xmlFullPath), new StackFrame(0).GetMethod().Name, ex.Message);
                }
            }
        }
        public void SetXmlFromClass<T, C>(T _T, C _C, string node = "")
        {
            XDocument _XDocument = ((XDocument)Convert.ChangeType(_T, typeof(T)));
            XElement _XElement = node.Equals("") ? _XDocument.Root : _XDocument.Element(node);
            IEnumerable <FieldInfo> _FieldInfos = _C.GetType().GetFields();
            foreach (FieldInfo _FieldInfo in _FieldInfos)
            {
                try
                {
                    _XElement.Add(new XElement(_FieldInfo.Name, _FieldInfo.GetValue(_C)));
                }
                catch (Exception)
                {
                    //Logger.LogThrowMessage(String.Format("Failed to Set Xml From Class."), new StackFrame(0).GetMethod().Name, ex.Message);
                }
            }
        }
        public void SetAttributeToXmlDoc<T>(T _T, string attributeName, string attributeValue, string node = "")
        {
            XDocument _XDocument = ((XDocument)Convert.ChangeType(_T, typeof(T)));
            XElement _XElement = node.Equals("") ? _XDocument.Root : _XDocument.Element(node);
            _XElement.SetAttributeValue(attributeName, attributeValue);
        }
        public string [] GetValuesArray(string nodeName = null)
        {
            try
            {
                return this.GetElementsIEnumerable(nodeName).ToArray<string>();
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
                return this.GetElementsIEnumerable(nodeName).ToList<string>();
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
                    return from x in _XElement.Elements() select x.Value;
                }
                else
                {
                    return from x in _XElement.Elements(nodeName) select x.Value;
                }
            }
            catch (Exception)
            {
                //Logger.LogThrowException(String.Format("Failed to get elements IEnumerable."), new StackFrame(0).GetMethod().Name, ex.Message);
                return null;
            }
        }
        public void SetValue<T>(T _T, string nodeName = null, string nodeValue = "")
        {
            try
            {
                XDocument _XDocument = ((XDocument)Convert.ChangeType(_T, typeof(T)));
                //bookVar = xml.Descendants("book").Where(a => a.Element("title").Value == param.Title);
                XElement _XElement = nodeName.Equals("") ? _XDocument.Root : _XDocument.Descendants(nodeName).Where(a => a.Name.LocalName.Equals(nodeName)).First();
                _XElement.Value = nodeValue;
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
                return _XElement.Element(nodeName).Value ;
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
