using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace alpha.Model
{
    [AddINotifyPropertyChangedInterface]
    public class ArticleModel :  Library.TypeLib.Article, INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
