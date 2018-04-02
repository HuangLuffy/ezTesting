using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonLib.Util
{
    public class UtilCloner
    {
        public static T Clone<T>(T RealObject)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, RealObject);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
                //XmlSerializer serializer = new XmlSerializer(typeof(T));
                //serializer.Serialize(objectStream, RealObject);
                //objectStream.Seek(0, SeekOrigin.Begin);
                //return (T)serializer.Deserialize(objectStream);
            }
        }
        public static List<T> CloneList<T>(List<T> _List)
        {
            List<T> _ListT = new List<T>();
            foreach (T item in _List)
            {
                _ListT.Add(item);
            }
            return _ListT;
        }
    }
}
