using ATLib;
using System.Collections.Generic;
using System.Linq;

namespace CMTest.Vm
{
    public static class VmObj
    {
        public static List<string> GetDevicesItemList()
        {
            var tmpList = new List<string>();
            var f = typeof(DeviceItem).GetFields();
            f.ToList().ForEach(a => tmpList.Add(((ATElementStruct)a.GetValue("Name")).Name));
            return tmpList;
        }
        public struct DeviceItem
        {
            public static ATElementStruct Item_H500M = new ATElementStruct()
            {
                Name = "H500M"
            };
            public static ATElementStruct Item_MP750 = new ATElementStruct()
            {
                Name = "RPBU"
            };
            public static ATElementStruct Item_MM830 = new ATElementStruct()
            {
                Name = "MM830"
            };
            public static ATElementStruct Item_MH752 = new ATElementStruct()
            {
                Name = "MH752"
            };
            public static ATElementStruct Item_MK850 = new ATElementStruct()
            {
                Name = "Gaming Keyboard MK850"
            };
            public static ATElementStruct Item_MP860 = new ATElementStruct()
            {
                Name = "RGB Mousepad"
            };
            public static ATElementStruct Item_MH650 = new ATElementStruct()
            {
                Name = "MH650"
            };
        }
        public static ATElementStruct Tab_TestVM = new ATElementStruct() {
            ClassName = "CNoFlickerButton"
        };
        public static ATElementStruct Window_VM = new ATElementStruct()
        {
            ClassName = "VMUIFrame"
        };
        public static ATElementStruct Item_RemovableDevices = new ATElementStruct()
        {
            Name = "Removable Devices"
        };
        
        public static ATElementStruct Item_LogitechUSBOpticalMouse = new ATElementStruct()
        {
            Name = "Logitech USB Optical Mouse"
        };
       
        public static ATElementStruct Item_Connect = new ATElementStruct()
        {
            Name = "Connect (Disconnect from Host)"
        };
        public static ATElementStruct Item_Disconnect = new ATElementStruct()
        {
            Name = "Disconnect (Connect to host)"
        };
        public static ATElementStruct Menu_Context = new ATElementStruct()
        {
            Name = "Context"
            //Name = "上下文"
        };
        //also need to add related device to xml.
    }
}
