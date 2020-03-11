using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class Orders
    {
        public int? ID { get; set; }
        public DateTime? TimeCreated { get; set; }
        public int? Orderstatus { get; set; }
        public float? Price { get; set; }
        public int? CustomerID { get; set; }
        public List<Articles>? Articles { get; set; }
        /// <summary>
        /// Print out keys with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void PrintKeys(int n = 16)
        {
            Console.Write("{0}{1}{2}{3}\n", "ID".PadRight(4), "TimeCreated".PadRight(n), "Orderstatus".PadRight(n), "Price".PadRight(n), "CustomerID".PadRight(n), "Articles.Count()".PadRight(n));
        }
        /// <summary>
        /// Print out values with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void Print(int n = 16)
        {
            // Special conversing because of nullable => '?'
            //DateTime d = new DateTime();
            //if (TimeCreated != null)
            //{
            //    d = TimeCreated.Value;
            //}
            //string timestamp = d.ToString("yyyy-MM-dd HH:mm:ss");
            //Console.Write("{0}{1}{2}{3}\n", ID.ToString().PadRight(4), timestamp.ToString().PadRight(n), Orderstatus.ToString().PadRight(n), Price.ToString().PadRight(n), CustomerID.ToString().PadRight(n),Articles.Count.ToString().PadRight(n));
        }
    }
}
