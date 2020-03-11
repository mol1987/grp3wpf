using System;
using System.Collections.Generic;
using System.Text;

namespace TypeLib
{
    public class DisplayObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int OrdersID { get; set; }
        public int Orderstatus { get; set; }
        public string Ingredient { get; set; }
        public void PrintKeys()
        {
            Console.WriteLine("{0}{1}{2}{3}{4}","ID".PadRight(8),"Name".PadRight(14),"OrdersID".PadRight(8),"Orderstatus".PadRight(8),"Ingredient".PadRight(14));
        }
        public void Print()
        {
            Console.Write("{0}{1}{2}{3}",
                ID.ToString().PadRight(8),
                Name.PadRight(14),
                OrdersID.ToString().PadRight(8),
                Ingredient.PadRight(14));
            Console.BackgroundColor = Orderstatus <= 1 ? ConsoleColor.DarkBlue : ConsoleColor.DarkGreen;
            Console.Write(Orderstatus.ToString().PadRight(8));
            Console.ResetColor();
            Console.Write("\n");
        }
    }
}