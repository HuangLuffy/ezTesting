using System;

namespace CommonLib.Util
{
    public static class UtilWait
    {
        private const int MaxWaitTimeInSec = 60;
        private const int IntervalInSec = 1;
        private static readonly int CompMaxWaitTimeInSec = -1;
        private static readonly int CompIntervalInSec = -1;
        private struct ResultType
        {
            public const string NonNullResult = "NON_NULL_RESULT";
            public const string NonEmptyResult = "NON_EMPTY";
            public const string AnyResult = "ANY_RESULT"; // suits for either getting result or getting exception
            public const string ForTrue = "FOR_TRUE";
        }
        public static bool ForTrueCatch<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1)
        {
            try
            {
                ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.ForTrue);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static T ForTrue<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.ForTrue);
        }
        public static T ForResult<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.AnyResult);
        }
        private static T ForWhat<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1, dynamic expectedResult = null)
        {
            if (maxWaitTimeInSec == -1)
            {
                maxWaitTimeInSec = CompMaxWaitTimeInSec < 0 ? MaxWaitTimeInSec : CompMaxWaitTimeInSec;
            }
            if (intervalInSec == -1)
            {
                intervalInSec = CompIntervalInSec < 0 ? IntervalInSec : CompIntervalInSec;
            }
            var dt = DateTime.Now;
            do {
                if (UtilTime.DateDiff(dt, DateTime.Now, UtilTime.DateInterval.Second) > Convert.ToDouble(maxWaitTimeInSec))
                {
                    throw new Exception($"Timeout! The max timeout is {maxWaitTimeInSec}s.");
                }
                try
                {
                    var actualResult = action.Invoke();
                    switch (expectedResult)
                    {
                        case ResultType.ForTrue when actualResult.ToString() == "True":
                            return actualResult;
                        case ResultType.AnyResult:
                            return actualResult;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                UtilTime.WaitTime(intervalInSec);
            } while(true);
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
