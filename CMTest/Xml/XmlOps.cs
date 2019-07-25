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
        public const string ATTRIBUTE_WAITTIME = "waitTime";
        public const string NODE_DEVICE = "device";
        private XElement vmPlugInOutDevicesRoot;
        private Dictionary<string, Dictionary<string, string>> Devices_Dict = new Dictionary<string, Dictionary<string, string>>();
        public XmlOps()
        {
            XmlLinq _XmlLinq = new XmlLinq(Path.Combine(Directory.GetCurrentDirectory(), "Conf.xml"));
            vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Element("vmPlugInOutDevices");
            //vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Descendants("device").Select(p => p.Elements());
            GetDevicesInfo();
        }
        public string GetVmPlugInOutDeviceNeedToRun()
        {
            try
            {
                return vmPlugInOutDevicesRoot.Attribute(ATTRIBUTE_VMNAME).Value.Trim();
            }
            catch (System.Exception)
            {
                return "";
            }  
        }
        private void GetDevicesInfo()
        {
            var devices = vmPlugInOutDevicesRoot.Elements(NODE_DEVICE);
            devices.ToList().ForEach(item => Devices_Dict.Add(item.Attribute(ATTRIBUTE_VMNAME).Value, new Dictionary<string, string>() { { ATTRIBUTE_INDEX, item.Attribute(ATTRIBUTE_INDEX).Value }, { ATTRIBUTE_WAITTIME, item.Attribute(ATTRIBUTE_WAITTIME).Value } }));
        }
        public string GetWaitTime(string deviceName)
        {
            return Devices_Dict[deviceName][ATTRIBUTE_WAITTIME];
        }
        public string GetIndex(string deviceName)
        {
            return Devices_Dict[deviceName][ATTRIBUTE_INDEX];
        }
        public List<string> GetDeviceNameList()
        {
            return Devices_Dict.Keys.ToList();
        }
    }
}
