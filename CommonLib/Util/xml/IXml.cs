using System.Collections.Generic;

namespace CommonLib.Util.xml
{
    public interface IXml
    {
        IEnumerable<string> GetElementsIEnumerable(string nodeName = null);
        string[] GetValuesArray(string nodeName = null);
        List<string> GetValuesList(string nodeName = null);
        void SetValue<T>(T t, string nodeName = null, string nodeValue = "");
        string GetValue(string nodeName = null);
        void SetClassFromXml<T>(T t);
        T GetXmlLoad<T>(string xmlFullPath);
        string GetXmlFullPathFromXmlLoad<T>(T t);
        T CreateAndGetXmlDoc<T>(string rootNode);
        void SetXmlFromClass<T, C>(T t, C c, string node = "");
        void SetClassFromXmlDoc<T, C>(T t, C c, string node = "");
        void SaveXmlDocToXml<T>(T t, string xmlFullPath);
        void SetAttributeToXmlDoc<T>(T t, string attributeName, string attributeValue, string node = "");
        void CreateXmlWithSameNodeNameList(string rootName, string nodeName, IEnumerable<string> nodeValueList);
    }
}
