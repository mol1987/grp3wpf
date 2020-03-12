using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace PizzaPalatsetWpf.Model
{
    public class ArticleModel { }
    public class Article : INotifyPropertyChanged
    {

        private string _name;
        private List<Articles> _articles = new List<Articles>();
        public List<Articles> Articles { 
            get 
            {
                RaisePropertyChanged("Articles");
                return _articles;
            } 
            set 
            {
                RaisePropertyChanged("Articles");
                _articles = value; 
            } 
        }

        public string Name { 
            get 
            {
                RaisePropertyChanged("Name");
                return _name;
            }
            set 
            {
                RaisePropertyChanged("Name");
                _name = value;
            }
        }

       
        public override string ToString()
        {
            return Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            
            if (PropertyChanged != null)
            {
                
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
