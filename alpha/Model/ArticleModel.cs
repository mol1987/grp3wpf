using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace alpha.Model
{
    public class ArticleModel :  Library.TypeLib.Article, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void ItemPropertyChanged()   
        {
        
        }
    }
}
