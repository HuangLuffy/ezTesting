using CommonLib.Util.xml;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace CMTest.Xml
{
    public class XmlOps
    {
        private const string ATTRIBUTE_INDEX = "index";
        private const string ATTRIBUTE_VMNAME = "vmName";
        private const string ATTRIBUTE_WAITTIME = "waitTime";
        private const string NODE_DEVICE = "device";
        private readonly XElement vmPlugInOutDevicesRoot;
        private readonly Dictionary<string, Dictionary<string, string>> _devicesDict = new Dictionary<string, Dictionary<string, string>>();
        public XmlOps()
        {
            var _XmlLinq = new XmlLinq(Path.Combine(Directory.GetCurrentDirectory(), "Conf.xml"));
            vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Element("vmPlugInOutDevices");
            //vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Descendants("device").Select(p => p.Elements());
            LoadDevicesDictInfo();
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
        private void LoadDevicesDictInfo()
        {
            var devices = vmPlugInOutDevicesRoot.Elements(NODE_DEVICE);
            devices.ToList().ForEach(item => _devicesDict.Add(item.Attribute(ATTRIBUTE_VMNAME).Value, new Dictionary<string, string>() { { ATTRIBUTE_INDEX, item.Attribute(ATTRIBUTE_INDEX).Value }, { ATTRIBUTE_WAITTIME, item.Attribute(ATTRIBUTE_WAITTIME).Value } }));
        }
        public string GetWaitTime(string deviceName)
        {
            return _devicesDict[deviceName][ATTRIBUTE_WAITTIME];
        }
        public string GetIndex(string deviceName)
        {
            return _devicesDict[deviceName][ATTRIBUTE_INDEX];
        }
        public List<string> GetDeviceNameList()
        {
            return _devicesDict.Keys.ToList();
        }
    }
}
