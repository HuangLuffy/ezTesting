using CommonLib.Util.xml;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using CommonLib.Util;

namespace CMTest.Xml
{
    public class XmlOps
    {
        public const string ATTRIBUTE_INDEX = "index";
        public const string ATTRIBUTE_VMNAME = "vmName";
        public const string NODE_DEVICE = "vmName";
        private XElement vmPlugInOutDevicesRoot;
        private Dictionary<string, Dictionary<string, string>> Devices_Dict = new Dictionary<string, Dictionary<string, string>>();
        public XmlOps()
        {
            XmlLinq _XmlLinq = new XmlLinq(Directory.GetCurrentDirectory() + "/" + "Conf.xml");
            vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Element("vmPlugInOutDevices");
            //vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Descendants("device").Select(p => p.Elements());
        }
        public string GetVmPlugInOutDeviceNeedToRun()
        {
            return vmPlugInOutDevicesRoot.Attribute(ATTRIBUTE_VMNAME).Value;
        }
        public void GetVmPlugInOutRunDevice()
        {
            var devices = vmPlugInOutDevicesRoot.Elements(NODE_DEVICE);
            devices.ToList().ForEach(item => Devices_Dict.Add(item.Attribute(ATTRIBUTE_VMNAME).Value, new Dictionary<string, string>() { { ATTRIBUTE_INDEX, item.Attribute(ATTRIBUTE_INDEX).Value } }));
        }
    }
}
