using ReportLib;

namespace XUnitTest.Manager
{
    public class ReporterManager
    {
        private static IReporter _iReporter;
        public static IReporter GeReporter(string pathReportXml, IReporter iReporter = null)
        {
            return _iReporter = iReporter ?? new ReporterXsl(pathReportXml);
        }
    }
}
