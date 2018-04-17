using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace ezTesting
{
    public class TestStep
    {
        public T Run<T>(Func<T> action, dynamic expectedResult = null)
        {
            try
            {
                T actualResult = action.Invoke();
                return actualResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Run(Action action, dynamic expectedResult = null)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [StepProperty("ExpectedResult", "ok")]
        public void firstStep()
        {

        }
    }
}
