using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;

namespace alpha
{
    public class CustomerViewModel : BaseViewModel
    {
        public ObservableCollection<Article> articles { get; set; } = new ObservableCollection<Article>();
        public ICommand ChangeArticle
        {
            get
            {
                return new RelayCommand(param => this.ChangeArticleAction(), null);
            }
        }

        public CustomerViewModel()
        {
            articles.Add(new Article { Name="Pizza_A", Price=99.0});
            articles.Add(new Article { Name = "Pizza_B", Price = 109.0 });
        }
        public void ChangeArticleAction()
        {
            Trace.WriteLine("HELLO");
            var item = articles.FirstOrDefault(i => i.Name == "Pizza_A");
            if (item != null)
            {
                item.Name = "Uppdaterad";
                item.Price = 666.0;
            }
            else
            {
                articles.Add(new Article { Name = "Pizza_C", Price = 129.0 });
            }
        }
    }
}
