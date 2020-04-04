using System;
using System.Diagnostics;

namespace alpha
{
    public static class Utilities
    {
        /// <summary>
        /// For measuring performance
        /// Usage ;
        /// 
        /// TimeSpan time = StopwatchUtil.Time(() =>
        /// {
        ///     // code here
        /// });
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static TimeSpan Time(Action action)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
    }
}
