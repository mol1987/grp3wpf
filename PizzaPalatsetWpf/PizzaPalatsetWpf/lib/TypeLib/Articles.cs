
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TypeLib
{
    public class Articles : ICloneable
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? BasePrice { get; set; }
        public string? Type { get; set; }
        public ArticleOrders? ArticleOrder { get; set; }
        public List<Ingredients> Ingredients { get; set; }
        /// <summary>
        /// Print out keys with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void PrintKeys(int n = 16)
        {
            Console.Write("{0}{1}{2}{3}{4}\n", "ID".PadRight(n), "Name".PadRight(n), "BasePrice".PadRight(n), "Type".ToString().PadRight(n), "Ingredients");
        }
        /// <summary>
        /// Print out values with $n for padding distance
        /// </summary>
        /// <param name="n"></param>
        public void Print(int n = 16)
        {
            string ingredients = "";
            Ingredients.ForEach(a => ingredients += a.Name + ", ");
            ingredients = new Regex(", $").Replace(ingredients, "");
            Console.Write("{0}{1}{2}{3}{4}\n", ID.ToString().PadRight(n), Name.PadRight(n), BasePrice.ToString().PadRight(n), Type.PadRight(n), ingredients.PadLeft(n));
        }

        public override string ToString()
        {
            return Name;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
