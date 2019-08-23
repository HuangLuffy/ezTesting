using System;
using CommonLib.Util.xml;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace CMTest.Xml
{
    public class XmlOps
    {
        private const string AttributeIndex = "index";
        private const string AttributeVmName = "vmName";
        private const string AttributeWaitTime = "waitTime";
        private const string NodeDevice = "device";
        private readonly XElement _vmPlugInOutDevicesRoot;
        private readonly Dictionary<string, Dictionary<string, string>> _devicesDict = new Dictionary<string, Dictionary<string, string>>();
        public XmlOps()
        {
            var xmlLinq = new XmlLinq(Path.Combine(Directory.GetCurrentDirectory(), "Conf.xml"));
            _vmPlugInOutDevicesRoot = xmlLinq.GetXElement().Element("vmPlugInOutDevices");
            //vmPlugInOutDevicesRoot = _XmlLinq.GetXElement().Descendants("device").Select(p => p.Elements());
            LoadDevicesDictInfo();
        }
        public string GetVmPlugInOutDeviceNeedToRun()
        {
            try
            {
                return _vmPlugInOutDevicesRoot.Attribute(AttributeVmName)?.Value.Trim();
            }
            catch (Exception)
            {
                return "";
            }  
        }
        private void LoadDevicesDictInfo()
        {
            var devices = _vmPlugInOutDevicesRoot.Elements(NodeDevice);
            devices.ToList().ForEach(item => _devicesDict.Add(item.Attribute(AttributeVmName).Value, new Dictionary<string, string>() { { AttributeIndex, item.Attribute(AttributeIndex).Value }, { AttributeWaitTime, item.Attribute(AttributeWaitTime).Value } }));
        }
        public string GetWaitTime(string deviceName)
        {
            return _devicesDict[deviceName][AttributeWaitTime];
        }
        public string GetIndex(string deviceName)
        {
            return _devicesDict[deviceName][AttributeIndex];
        }
        public List<string> GetDeviceNameList()
        {
            return _devicesDict.Keys.ToList();
        }
    }
}
