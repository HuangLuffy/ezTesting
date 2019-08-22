using XunitTest.Handler;

namespace XunitTest.Projects
{
    public abstract class StepsCommon
    {
        protected readonly TestStepHandler _TestStepHandler;

        protected StepsCommon(string pathReportXml = "")
        {
            _TestStepHandler = new TestStepHandler(pathReportXml);
        }
        public void  End()
        {

        }
    }
}
