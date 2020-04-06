using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TypeLib
{
    public class DisplayObject
    {
        public int OrderID { get; set; }
        /// <summary>
        /// Specfic ID for each Article inside each order
        /// Useful for the chief, since he needs to see each individual item and it's ingredients
        /// </summary>
        public int? ArticleOrderID { get; set; }
        public int? ArticleType { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? ArticleName { get; set; }
        public string? IngredientsName { get; set; }
        public int? OrderStatus { get; set; }
        public Order? Order { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        /// <summary>
        /// Formated for display
        /// </summary>
        public string FormattedTimeStamp => TimeStamp?.ToString("MM-dd HH:mm:ss") ?? "Missing timestamp";

        /// <summary>
        /// Neatly displayed, example => "Ost, Skinka, Tomatsås"
        /// </summary>
        public string JoinedIngredients
        {
            get
            {
                string r = "";
                Ingredients.ForEach(a => r += string.Format("{0} ,", a.Name));
                return r;
            }
        }
    }
}