using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace alpha
{
    [AddINotifyPropertyChangedInterface]
    public class ArticleExample
    {
        public string Name { get; set; } = "...";
        public double Price { get; set; } = 0.0;
    }
}
