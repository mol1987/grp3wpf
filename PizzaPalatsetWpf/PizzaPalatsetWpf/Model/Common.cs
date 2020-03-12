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

        public void LoadArticles(String articleType)
        {
            OnPropertyChanged("Articles");
            Articles = new NotifyTaskCompletion<IEnumerable<Articles>>(General.articlesRepo.GetAllAsync(articleType));
  
        }

        private Articles p_SelectedItem;
        public Articles SelectedItem
        {
            get { return p_SelectedItem; }

            set
            {
                p_SelectedItem = value;
                OnPropertyChanged("SelectedItem");
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
