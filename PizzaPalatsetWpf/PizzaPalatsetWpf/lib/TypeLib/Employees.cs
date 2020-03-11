using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class Employees
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        /// <summary>
        /// Print out keys with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void PrintKeys(int n = 16)
        {
            Console.Write("{0}{1}{2}{3}{4}\n", "ID".PadRight(n), "Name".PadRight(n), "LastName".PadRight(n), "Email".ToString().PadRight(n), "Password");
        }
        /// <summary>
        /// Print out values with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void Print(int n = 16)
        {
            Console.Write("{0}{1}{2}{3}{4}\n", ID.ToString().PadRight(n), Name.PadRight(n), LastName.ToString().PadRight(n), Email.PadRight(n), Password.PadRight(n));
        }
    }
}
