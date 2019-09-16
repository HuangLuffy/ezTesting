using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CommonLib.Util.Log;

namespace CommonLib.Util.Xml
{
    public class XmlSerialization
    {
        public static string XmlSerializationWithEncoding(object obj)
        {
            string result;
            var xmlWriterSettings = new XmlWriterSettings {OmitXmlDeclaration = false, Encoding = Encoding.Default};
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
                {
                    var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add("", "");
                    var xmlSerializer = new XmlSerializer(obj.GetType());
                    xmlSerializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
                    result = xmlWriterSettings.Encoding.GetString(memoryStream.ToArray());
                }
            }
            return result;
        }

        public static string XmlSerializationFromObj(object obj, Encoding encoding = null)
        {
            string result = string.Empty;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            try
            {
                var xmlSerializer = new XmlSerializer(obj.GetType());
                using (var memoryStream = new MemoryStream())
                {
                    using (var xmlTextWriter = new XmlTextWriter(memoryStream, encoding))
                    {
                        var xmlSerializerNamespaces = new XmlSerializerNamespaces();
                        xmlSerializerNamespaces.Add("", "");
                        xmlTextWriter.Formatting = Formatting.Indented;
                        xmlSerializer.Serialize(xmlTextWriter, obj, xmlSerializerNamespaces);
                        xmlTextWriter.Flush();
                        xmlTextWriter.Close();
                    }
                    //UTF8Encoding _UTF8Encoding = new UTF8Encoding(false, true);
                    result = encoding.GetString(memoryStream.ToArray());
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowException("Failed to serialize xml.", new StackFrame(0).GetMethod().Name, ex.Message);
            }
            return result;
        }

        public static T XmlDeserialization<T>(string xml)
        {
            var result = default(T);
            var xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                using (var stringReader = new StringReader(xml))
                {
                    result = (T)xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowException("Failed to deserialize xml.", new StackFrame(0).GetMethod().Name, ex.Message);
            }
            return result;
        }
    }
}
