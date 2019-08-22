using ReportLib;

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
