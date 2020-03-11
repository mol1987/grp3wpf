using MsSqlRepo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TypeLib;
using System.Linq;

namespace PizzaPalatsetWpf.Model
{
    public class Common : INotifyPropertyChanged
    {
        private static Common _instance = null;

        protected Common()
        {
        }

        public static Common GetInstance()
        {
            if (_instance == null)
                _instance = new Common();

            return _instance;
        }

        public NotifyTaskCompletion<IEnumerable<Articles>> Articles { get; set; }

        public void LoadArticles(Globals.ArticleTypes articleType)
        {
            OnPropertyChanged("Articles");
            switch (articleType)
            {
                case Globals.ArticleTypes.Pizza:
                    Articles = new NotifyTaskCompletion<IEnumerable<Articles>>(General.articlesRepo.GetAllAsync("Pizza"));
                    break;
                case Globals.ArticleTypes.Pasta:
                    break;
                case Globals.ArticleTypes.Sallad:
                    Articles = new NotifyTaskCompletion<IEnumerable<Articles>>(General.articlesRepo.GetAllAsync("Sallad"));
                    break;
                case Globals.ArticleTypes.Dricka:
                    Articles = new NotifyTaskCompletion<IEnumerable<Articles>>(General.articlesRepo.GetAllAsync("Drynk"));
                    break;
                case Globals.ArticleTypes.Tillbehor:
                    break;
                case Globals.ArticleTypes.Rekommenderat:
                    break;
                case Globals.ArticleTypes.Alla:
                    break;
                default:
                    break;
            }
        }

        public void Load()
        {
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventArgs ea = new PropertyChangedEventArgs(propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, ea);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
