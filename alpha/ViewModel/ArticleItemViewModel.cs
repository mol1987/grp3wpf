using System;
using System.Collections.Generic;
using System.Text;

namespace alpha
{
    public class ArticleItemViewModel
    {
        public Article Article { get; set; }

        public ArticleItemViewModel(Article article)
        {
            this.Article = article;
        }

        public void ChangeName(string n) => Article.Name = n;
        public void ChangePrice(double n) => Article.Price = n;
        public void ChangePrice(float f) => Article.Price = (double)f;

    }
}
