using System;
using CommonLib.Util.Xml;
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
        private const string AttributeRunTimes = "runTimes";
        private const string NodeDevice = "device";
        private const string NodeRemoteOS = "remoteOS";
        private const string NodeLocalOS = "localOS";
        private const string NodeMasterPlusPerBuildPath = "masterPlusPerBuildPath";
        private const string NodeMasterPlusBuildPath = "masterPlusBuildPath";
        private const string NodeIP = "ip";
        private const string NodeRestartTimes = "restartTimes";
        private readonly XElement _vmPlugInOutDevicesRoot;
        private readonly Dictionary<string, Dictionary<string, string>> _devicesDict = new Dictionary<string, Dictionary<string, string>>();
        private readonly string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "Conf.xml");
        private readonly XmlLinq xmlLinq;
        public XmlOps()
        {
            xmlLinq = new XmlLinq(xmlPath);
            _vmPlugInOutDevicesRoot = xmlLinq.GetXRoot().Element("vmPlugInOutDevices");
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
            devices.ToList().ForEach(item => _devicesDict.Add(item.Attribute(AttributeVmName).Value, new Dictionary<string, string>() { { AttributeIndex, item.Attribute(AttributeIndex).Value }, { AttributeWaitTime, item.Attribute(AttributeWaitTime).Value }, { AttributeRunTimes, item.Attribute(AttributeRunTimes).Value } }));
        }
        public string GetWaitTime(string deviceName)
        {
            return _devicesDict[deviceName][AttributeWaitTime];
        }
        public string GetIndex(string deviceName)
        {
            return _devicesDict[deviceName][AttributeIndex];
        }
        public string GetRunTimes(string deviceName)
        {
            return _devicesDict[deviceName][AttributeRunTimes];
        }
        public IEnumerable<string> GetDeviceNameList()
        {
            return _devicesDict.Keys.ToList();
        }
        public string GetRemoteOsIp()
        {
            return xmlLinq.GetXRoot().Element(NodeRemoteOS).Element(NodeIP).Value;
        }
        public void SetRemoteOsIp(string newIp)
        {
            xmlLinq.GetXRoot().Element(NodeRemoteOS).Element(NodeIP).Value = newIp;
            xmlLinq.Save();
        }
        public string GetMasterPlusPerBuildPath()
        {
            return xmlLinq.GetXRoot().Element(NodeLocalOS).Element(NodeMasterPlusPerBuildPath).Value.Trim();
        }
        public string GetMasterPlusBuildPath()
        {
            return xmlLinq.GetXRoot().Element(NodeLocalOS).Element(NodeMasterPlusPerBuildPath).Value.Trim();
        }
        public string GetRestartTimes()
        {
            return xmlLinq.GetXRoot().Element(NodeRestartTimes).Value.Trim();
        }
        public void SetRestartTimes(int times)
        {
            xmlLinq.GetXRoot().Element(NodeRestartTimes).Value = times.ToString();
            xmlLinq.Save();
        }
    }
}
