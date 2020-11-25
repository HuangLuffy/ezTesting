using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLib.Util.Xml
{
    public class XmlTs
    {
        private readonly XmlLinq xmlLinq;
        private readonly IEnumerable<XElement> _contexts;
        public XmlTs()
        {
            xmlLinq = new XmlLinq(@"D:\Dev\CM\Auto\ts\masterplus_zh_cn.ts");
            _contexts = xmlLinq.GetXRoot().Elements("context");
        }

        public string Trs(string english)
        {
            //var fv = _contexts.Where(x => x.Element("name") != null).Select(b => b.Element("name").Value); ;
            //var a = _contexts.Where(x => x.Element("message").Element("source").Value.Equals("APPLY")).Select(b => b.Element("message").Element("translation").Value);
            return _contexts.First(x => x.Element("message").Element("source").Value.Equals(english)).Element("message").Element("translation").Value;
        }
    }
}
