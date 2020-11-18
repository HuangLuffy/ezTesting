using System;
using System.Threading;

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
        public static T ForNonNull<T>(Func<T> action, int maxWaitTimeInSec = -1, int intervalInSec = -1)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.NonNullResult);
        }
        public static T ForTrue<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.ForTrue);
        }
        public static T ForAnyResult<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1)
        {
            return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.AnyResult);
        }
        public static T ForAnyResultCatch<T>(Func<T> action, int maxWaitTimeInSec = -1, int intervalInSec = -1)
        {
            try
            {
                return ForWhat(action, maxWaitTimeInSec, intervalInSec, ResultType.AnyResult);
            }
            catch (Exception)
            {
                return default(T);
            } 
        }
        private static T ForWhat<T>(Func<T> action, int maxWaitTimeInSec =-1, int intervalInSec = -1, dynamic expectedResult = null)
        {
            var exceptionMsg = "";
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
                    throw new Exception($"UtilWait > Timeout! The max timeout is {maxWaitTimeInSec}s. The last exception message is {exceptionMsg}.");
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
                        case ResultType.NonNullResult when actualResult != null:
                            return actualResult;
                    }
                }
                catch (Exception ex)
                {
                    exceptionMsg = ex.Message;
                }
                UtilTime.WaitTime(intervalInSec);
            } while(true);
        }
        public static Thread WaitTimeElapseThread(string comment, int times = -1)
        {
            return new Thread(() =>
            {
                var s = 0;
                while (true)
                {
                    UtilTime.WaitTime(1);
                    UtilCmd.WriteTitle($"{comment}  Please Wait...  Time elapsed {s++}s ");
                    if (s == times)
                    {
                        return;
                    }
                }
            });
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
