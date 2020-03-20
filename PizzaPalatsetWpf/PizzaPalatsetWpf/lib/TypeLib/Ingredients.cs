using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class Ingredients
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        /// <summary>
        /// Print out keys with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void PrintKeys(int n = 16)
        {
            Console.Write("{0}{1}{2}\n", "ID".PadRight(n), "Name".PadRight(n), "Price".ToString().PadRight(n));
        }
        /// <summary>
        /// Print out values with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void Print(int n = 16)
        {
            Console.Write("{0}{1}{2}\n", ID.ToString().PadRight(n), Name.PadRight(n), Price.ToString().PadRight(n));
        }
        override public string ToString()
        {
            return Name;
        }
    }
}
