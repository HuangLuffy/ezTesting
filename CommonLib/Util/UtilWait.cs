using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util
{
    public class UtilWait
    {
        private const int maxWaitTimeInSec = 60;
        private const int intervalInSec = 1;
        private struct ResulType
        {
            public const string NON_NULL_RESULT = "NON_NULL_RESULT";
            public const string NON_EMPTY_RESULT = "NON_EMPTY";
            public const string ANY_RESULT = "ANY_RESULT";
            public const string FOR_TRUE = "FOR_TRUE";
        }
        public static bool ForTrueCatch<T>(Func<T> action, int maxWaitTimeInSec = maxWaitTimeInSec, int intervalInSec = intervalInSec)
        {
            try
            {
                ForWhat(action, maxWaitTimeInSec, intervalInSec, ResulType.FOR_TRUE);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static T ForTrue<T>(Func<T> action, int maxWaitTimeInSec = maxWaitTimeInSec, int intervalInSec = intervalInSec)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResulType.FOR_TRUE);
        }
        public static T ForResult<T>(Func<T> action, int maxWaitTimeInSec = maxWaitTimeInSec, int intervalInSec = intervalInSec)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResulType.ANY_RESULT);
        }
        public static T ForWhat<T>(Func<T> action, int maxWaitTimeInSec = maxWaitTimeInSec, int intervalInSec = intervalInSec, dynamic expectedResult = null)
        {
            DateTime dt = DateTime.Now;
            do {
                try
                {
                    T actualResult = action.Invoke();
                    if (expectedResult == ResulType.FOR_TRUE)
                    {
                        if (actualResult.ToString() == "True")
                        {
                            return actualResult;
                        }
                    }
                    else if (expectedResult == ResulType.ANY_RESULT)
                    {
                        return actualResult;
                    }
                    if (UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second) > Convert.ToDouble(maxWaitTimeInSec))
                    {
                        throw new Exception($"Timeout! The max timeout is {maxWaitTimeInSec}s.");
                    }

                }
                catch (Exception e)
                {
                    throw e;
                    //Console.WriteLine(e);
                }
                UtilTime.WaitTime(intervalInSec);
            } while(true);
        }
        public static bool ForPass(Action action, int maxWaitTimeInSec = maxWaitTimeInSec, int intervalInSec = intervalInSec)
        {
            bool passed = false;
            int retries = 0;
            while (!passed && (retries * intervalInSec < maxWaitTimeInSec))
                try
                {
                    action.Invoke();
                    passed = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    retries++;
                    UtilTime.WaitTime(intervalInSec);
                }
            return passed;
        }
        
        //public void test()
        //{
        //    ForTrue(() =>
        //    {
        //        if (true)
        //        {
        //            return true;
        //        }
        //    }, 600, 10);
        //}
    }
}
