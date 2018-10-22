using ATLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project.Portal
{
    public class VM
    {
        public ATElementStruct Tab_TestVM = new ATElementStruct() {
            ClassName = "CNoFlickerButton"
        };
        public ATElementStruct Window_VM = new ATElementStruct()
        {
            ClassName = "VMUIFrame"
        };
        public ATElementStruct Item_RemovableDevices = new ATElementStruct()
        {
            Name = "Removable Devices"
        };
        public ATElementStruct H500M = new ATElementStruct()
        {
            //Name = "Logitech USB Optical Mouse"
            Name = "H500M"
        };
        public ATElementStruct Item_MM830 = new ATElementStruct()
        {
            Name = "MM830"
        };
        public ATElementStruct Item_MP860 = new ATElementStruct()
        {
            Name = "RGB Mousepad"
        };
        public ATElementStruct Item_Connect = new ATElementStruct()
        {
            Name = "Connect (Disconnect from Host)"
        };
        public ATElementStruct Item_Disconnect = new ATElementStruct()
        {
            Name = "Disconnect (Connect to host)"
        };
        public ATElementStruct Menu_Context = new ATElementStruct()
        {
            Name = "Context"
            //Name = "上下文"
        };
    }
}
