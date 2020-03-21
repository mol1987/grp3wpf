using System;
using System.Collections.Generic;
using System.Text;
using Library.TypeLib;

namespace alpha
{
    public class ArticleItemViewModel : BaseViewModel
    {
        public Article Article { get; set; }

        public ArticleItemViewModel(Article article)
        {
            this.Article = article;
        }

        public void ChangeName(string n) => Article.Name = n;
        public void ChangePrice(double n) => Article.Price = (float)n;
        public void ChangePrice(float f) => Article.Price = f;

    }
}
