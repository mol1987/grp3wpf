
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Library.TypeLib
{
    public class Article
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public string? Type { get; set; }
        public ArticleOrder? ArticleOrder { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
