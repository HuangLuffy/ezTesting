using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CommonLib.Util.xml
{
    public class XmlSerialization
    {
        public static string XMLSerializationWithEncoding(Object Obj)
        {
            string result = string.Empty;
            XmlWriterSettings _XmlWriterSettings = new XmlWriterSettings();
            _XmlWriterSettings.OmitXmlDeclaration = false;
            _XmlWriterSettings.Encoding = Encoding.Default;
            using (MemoryStream _MemoryStream = new MemoryStream())
            {
                using (XmlWriter _XmlWriter = XmlWriter.Create(_MemoryStream, _XmlWriterSettings))
                {
                    XmlSerializerNamespaces _XmlSerializerNamespaces = new XmlSerializerNamespaces();
                    _XmlSerializerNamespaces.Add("", "");
                    XmlSerializer _XmlSerializer = new XmlSerializer(Obj.GetType());
                    _XmlSerializer.Serialize(_XmlWriter, Obj, _XmlSerializerNamespaces);
                    result = _XmlWriterSettings.Encoding.GetString(_MemoryStream.ToArray());
                }
            }
            return result;
        }

        public static string XmlSerializationFromObj(object obj, Encoding _Encoding = null)
        {
            string result = string.Empty;
            if (_Encoding == null)
            {
                _Encoding = Encoding.UTF8;
            }
            try
            {
                XmlSerializer _XmlSerializer = new XmlSerializer(obj.GetType());
                using (MemoryStream _MemoryStream = new MemoryStream())
                {
                    using (XmlTextWriter _XmlTextWriter = new XmlTextWriter(_MemoryStream, _Encoding))
                    {
                        XmlSerializerNamespaces _XmlSerializerNamespaces = new XmlSerializerNamespaces();
                        _XmlSerializerNamespaces.Add("", "");
                        _XmlTextWriter.Formatting = Formatting.Indented;
                        _XmlSerializer.Serialize(_XmlTextWriter, obj, _XmlSerializerNamespaces);
                        _XmlTextWriter.Flush();
                        _XmlTextWriter.Close();
                    }
                    //UTF8Encoding _UTF8Encoding = new UTF8Encoding(false, true);
                    result = _Encoding.GetString(_MemoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowException(String.Format("Failed to serialize xml."), new StackFrame(0).GetMethod().Name, ex.Message);
            }
            return result;
        }

        public static T XmlDeserialization<T>(string xml)
        {
            T result = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                using (StringReader stringReader = new StringReader(xml))
                {
                    result = (T)xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowException(String.Format("Failed to deserialize xml."), new StackFrame(0).GetMethod().Name, ex.Message);
            }
            return result;
        }
    }
}
