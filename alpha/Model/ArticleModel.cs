using Library.TypeLib;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace alpha.Model
{
    [AddINotifyPropertyChangedInterface]
    public class ArticleModel : Article, INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? BasePrice { get; set; }
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }
    }
}
