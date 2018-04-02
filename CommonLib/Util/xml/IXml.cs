using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.Util.xml
{
    public interface IXml
    {
        IEnumerable<string> GetElementsIEnumerable(string nodeName = null);
        string[] GetValuesArray(string nodeName = null);
        List<string> GetValuesList(string nodeName = null);
        void SetValue<T>(T _T, string nodeName = null, string nodeValue = "");
        string GetValue(string nodeName = null);
        void SetClassFromXml<T>(T _T);
        T GetXmlLoad<T>(string xmlFullPath);
        string GetXmlFullPathFromXmlLoad<T>(T _T);
        T CreateAndGetXmlDoc<T>(string rootNode);
        void SetXmlFromClass<T, C>(T _T, C _C, string node = "");
        void SetClassFromXmlDoc<T, C>(T _T, C _C, string node = "");
        void SaveXmlDocToXml<T>(T _T, string xmlFullPath);
        void SetAttributeToXmlDoc<T>(T _T, string attributeName, string attributeValue, string node = "");
        void CreateXmlWithSameNodeNameList(string rootName, string nodeName, IEnumerable<string> nodeValueList);
    }
}
