﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XunitTest.Handler;

namespace XunitTest.Projects
{
    public abstract class Steps_Common
    {
        public TestStepHandler _TestStepHandler;
        public Steps_Common(string pathReportXml = "")
        {
            this._TestStepHandler = new TestStepHandler(pathReportXml);
        }
        public void  End()
        {

        }
    }
}
