using ATLib;
using CommonLib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTest.Tool
{
    public class KeysTest
    {
        readonly AT app;
        public KeysTest(string appPath)
        {
            var p = UtilProcess.StartProcessReturn(appPath);
            AT f = new AT();
            app = f.GetElementFromHwnd(p.Handle);
        }
        public void ClickClear()
        {
            app.GetElementFromChild(new ATElementStruct() { Name = "Clear"}).DoClick();
        }
        public void ClickClose()
        {
            app.GetElementFromChild(new ATElementStruct() { Name = "Close" }).DoClick();
        }
        public void GetContent()
        {
            app.GetElementFromChild(new ATElementStruct() { Name = "Close" }).DoClick();
        }
    }
}
