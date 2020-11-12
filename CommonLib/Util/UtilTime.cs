using System;
using System.Threading;
using System.Timers;

namespace CommonLib.Util
{
    public class UtilTime
    {
        private static Thread _counterThread = null;
        public static void WaitTime(double timeout)
        {
            Thread.Sleep((int)(timeout * 1000));
        }
        #region datediff
        public enum DateInterval
        {
            /// <summary>
            /// 
            /// </summary>
            Day,
            /// <summary>
            /// 
            /// </summary>
            DayOfYear,
            /// <summary>
            /// 
            /// </summary>
            Hour,
            /// <summary>
            /// 
            /// </summary>
            Minute,
            /// <summary>
            /// 
            /// </summary>
            Month,
            /// <summary>
            /// 
            /// </summary>
            Quarter,
            /// <summary>
            /// 
            /// </summary>
            Second,
            /// <summary>
            /// 
            /// </summary>
            Weekday,
            /// <summary>
            /// 
            /// </summary>
            WeekOfYear,
            /// <summary>
            /// 
            /// </summary>
            Year
        }
        public static long DateDiff(DateTime oldTime)
        {
            return DateDiff(oldTime, DateTime.Now, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek, DateInterval.Second);
        }
        public static long DateDiff(DateTime oldTime, DateTime newTime, DateInterval interval = DateInterval.Second)
        {
            return DateDiff(oldTime, newTime, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek, interval);
        }
        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            if (nMonth <= 9)
                return 3;
            return 4;
        }
        public static long DateDiff(DateTime oldTime, DateTime newTime, DayOfWeek eFirstDayOfWeek, DateInterval interval = DateInterval.Second)
        {
            if (interval == DateInterval.Year)
                return newTime.Year - oldTime.Year;

            if (interval == DateInterval.Month)
                return (newTime.Month - oldTime.Month) + (12 * (newTime.Year - oldTime.Year));

            var ts = newTime - oldTime;

            switch (interval)
            {
                case DateInterval.Day:
                case DateInterval.DayOfYear:
                    return Round(ts.TotalDays);
                case DateInterval.Hour:
                    return Round(ts.TotalHours);
                case DateInterval.Minute:
                    return Round(ts.TotalMinutes);
                case DateInterval.Second:
                    return Round(ts.TotalSeconds);
                case DateInterval.Weekday:
                    return Round(ts.TotalDays / 7.0);
                case DateInterval.WeekOfYear:
                {
                    while (newTime.DayOfWeek != eFirstDayOfWeek)
                        newTime = newTime.AddDays(-1);
                    while (oldTime.DayOfWeek != eFirstDayOfWeek)
                        oldTime = oldTime.AddDays(-1);
                    ts = newTime - oldTime;
                    return Round(ts.TotalDays / 7.0);
                }
            }

            if (interval != DateInterval.Quarter) return 0;
            double d1Quarter = GetQuarter(oldTime.Month);
            double d2Quarter = GetQuarter(newTime.Month);
            var d1 = d2Quarter - d1Quarter;
            double d2 = (4 * (newTime.Year - oldTime.Year));
            return Round(d1 + d2);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dVal"></param>
        /// <returns></returns>
        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }
        #endregion

        #region elapse
        public static void CountDown(int maxNum, Action<int> action, bool stopLastThread = true)
        {
            try
            {
                if (_counterThread != null && _counterThread.IsAlive is true)
                {
                    _counterThread.Abort();
                }
            }
            catch (Exception)
            {
                // ignored
            }

            _counterThread = new Thread(() => {
                for (var i = maxNum; i > 0; i--)
                {
                    action.DynamicInvoke(i);
                    Thread.Sleep(1000);
                    if (i == 1)
                    {
                        _counterThread.Abort();
                    }
                    //Thread currthread = Thread.CurrentThread;
                }
            });
            _counterThread.Start();
        }
        //(DateTime.Now - dateTime).Seconds;
        public static TimeSpan TimeElapsed(DateTime dateTime)
        {
            return DateTime.Now - dateTime;
        }
        public static string GetTimeString()
        {
            return $"{DateTime.Now:yyyyMMdd_hhmmss}";
        }
        private static void TimeElapsedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"The Elapsed event was raised at {e.SignalTime}");
        }
        public void Bbb(Action action)
        {

            var aTimer = new System.Timers.Timer(10000);              
            aTimer.Elapsed += new ElapsedEventHandler(TimeElapsedEvent);    //添加一个钩子：注册一个函数，在倒计时完成后调用           
            aTimer.Interval = 2000;               //设置 倒计时 的时间为 2s     
            aTimer.AutoReset = true;   //设置 是否 循环执行：默认会循环执行             
            aTimer.Enabled = true;//开启 倒计时  
            //Console.WriteLine("按下回车结束程序");
            //Console.ReadLine();
        }
        public void Aaa()
        {

            System.Timers.Timer aTimer = new System.Timers.Timer(10000);         //添加一个钩子：注册一个函数，在倒计时完成后调用        
            aTimer.Elapsed += new ElapsedEventHandler(TimeElapsedEvent);         //设置 倒计时 的时间为 2s        
            aTimer.Interval = 2000;         //设置 是否 循环执行：默认会循环执行        
            aTimer.AutoReset = true;         //开启 倒计时       
            aTimer.Enabled = true;
            //Console.WriteLine("按下回车结束程序");
            //Console.ReadLine();
        }
        #endregion
    }
}
