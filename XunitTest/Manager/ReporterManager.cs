using ReportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib
{
    public class ReporterManager
    {
        private static IReporter _IReporter;
        public static IReporter GeReporter(string pathReportXml, IReporter iReporter = null)
        {
            return _IReporter = iReporter == null ? new ReporterXsl(pathReportXml) : iReporter;
        }
    }
}
