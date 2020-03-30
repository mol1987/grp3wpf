
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
        public float? BasePrice { get; set; }
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
        /// <summary>
        /// URL to image location .
        /// </summary>
        public string? ImageSource { get; set; }
        public ArticleOrder? ArticleOrder { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
