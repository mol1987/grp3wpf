using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Helper
{
    /// <summary>
    /// Global scope, reachable from everywhere. Is limited to strings for now
    /// </summary>
    public static class Globals
    {
        private static Dictionary<string, string> items = new Dictionary<string, string> { };
        
        /// <summary>
        /// Check if global value is set
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns True or False</returns>
        public static bool Exists(string key)
        {
            return items.ContainsKey(key);
        }

        /// <summary>
        /// Get stored string value
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns string</returns>
        public static string Get(string key)
        {
            if (!Exists(key))
                return "";

            return items[key];
        }

        /// <summary>
        /// Get stored string value with assigned default value as backup
        /// </summary>
        /// <param name="key"></param>
        /// <param name="backupValue"></param>
        /// <returns></returns>
        public static string Get(string key, ref string backupValue)
        {
            if (!Exists(key))
                return backupValue;

            return items[key];
        }

        /// <summary>
        /// Set value to global
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void Set(string key, string val)
        {
            items.Add(key, val);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Animation
    {
        private static System.Threading.Timer Timer;
        public string Basename { get; set; }
        private string CurrentString = "";
        public Animation(string title = "")
        {
            Basename = title;
        }
        public void Startanimation()
        {
            Timer = new System.Threading.Timer(Callback, null, 0, 100);
        }
        public void Haltanimation()
        {
            Timer.Dispose();
        }
        public void Clearanimation()
        {
            Haltanimation();
            CurrentString = "";
            SetTitle();
        }
        public void SetTitle()
        {
            Console.Title = String.Format("{0}{1}", Basename, CurrentString);
        }
        public void SetTitle(string v)
        {
            Console.Title = v;
        }
        private void Callback(object state)
        {
            CurrentString += ".";
            if (CurrentString.Length > 6)
            {
                CurrentString = "";
            }
            SetTitle();
        }
    }
}
