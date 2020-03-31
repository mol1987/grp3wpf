using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TypeLib
{
    public class DisplayObject
    {
        public int OrderID { get; set; }
        public int? ArticleType { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string? ArticleName { get; set; }
        public string? IngredientsName { get; set; }
        public int? OrderStatus { get; set; }
        public Order? Order { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}