using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogainWPF_Project.Models;

namespace LogainWPF_Project.ViewModels
{
    public class ArticleViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<Article> article = new ObservableCollection<Article>
        {
            new Article{Name="Pizza"},
            new Article{Name= "Pasta"},
            new Article{Name= "Drink"},
            new Article {Name="Sallad"}
        };
        public ObservableCollection<Article> Article
        {
            get
            {
                return article;

            }
            set
            {
                article = value;
            }
        }

       

        private Article selectedArticle;

        public Article SelectedArticle
        {
            get
            {
                return selectedArticle;
            }
            set
            {
                selectedArticle = value;
                OnPropertyChanged("selectedArticle");
            }
        }

       
    }
}
