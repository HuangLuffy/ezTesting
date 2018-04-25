using CommonLib.Util;
using ReportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace XunitTest.Wrapper
{
    public class DetailedTestStep : TestStep
    {
        IReporter _IReporter;
        public string pathReportXml = "";
        public DetailedTestStep(string pathReportXml)
        {
            _IReporter = ReporterManager.GeReporter(pathReportXml);
        }
        public T Rec<T>(Func<T> action)
        {
            try
            {
                if (this.needToBlockTest)
                    return default(T);
                DateTime dt = DateTime.Now;
                T result = Exec<T>(action);
                
               // _IReporter.AddTestStep(classname, UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second), );
                return Exec<T>(action);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Rec(Action action)
        {
            if (this.needToBlockTest)
                return;
            Exec(action);
        }
    }
}
