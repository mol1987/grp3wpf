using System;
using System.Collections.Generic;
using System.Text;
using Library.TypeLib;

namespace alpha
{
    /// <summary>
    /// Container for article object inside XAML
    ///     todo; add
    ///     - Include Image
    ///     - Add button
    ///     - See details button
    /// </summary>
    public class ArticleItemDataModel
    {
        /// <summary>
        /// <see cref="TypeLib.Article"/>
        /// </summary>
        public Article Article { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="article"></param>
        public ArticleItemDataModel(Article article)
        {
            this.Article = article;
        }

        public void ChangeName(string n) => Article.Name = n;
        public void ChangePrice(double n) => Article.BasePrice = (float)n;
        public void ChangePrice(float f) => Article.BasePrice = f;
    }
}
