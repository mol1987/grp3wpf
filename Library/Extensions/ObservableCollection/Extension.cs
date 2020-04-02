using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Extensions.ObservableCollection
{
    public static class Extension
    {
        /// <summary>
        /// Foreach extension for <see cref="ObservableCollection"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var cur in enumerable)
            {
                action(cur);
            }
        }
    }
}
