using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace jkb_rebase.Model
{
    public class Article : INotifyPropertyChanged
    {
        public string Name { get; set;}
        public decimal Price { get; set; }
        #region Common

        public event PropertyChangedEventHandler PropertyChanged; 
        protected void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
