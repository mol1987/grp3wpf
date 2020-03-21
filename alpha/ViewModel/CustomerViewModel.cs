using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;
using Library.TypeLib;

namespace alpha
{
    public class CustomerViewModel : BaseViewModel
    {
        public ObservableCollection<ArticleItemViewModel> articles { get; set; } = new ObservableCollection<ArticleItemViewModel>();

        public ArticleItemViewModel SelectedArticle { get; set; }

        public ICommand ChangeArticle { get { return new RelayCommand(param => this.ChangeArticleAction(), null); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerViewModel()
        {
            articles.Add(new ArticleItemViewModel(new Article { Name = "Pizza_A", Price = 99.0f }));
            articles.Add(new ArticleItemViewModel(new Article { Name = "Pizza_B", Price = 109.0f }));
        }

        /// <summary>
        /// Swapping View Action
        /// </summary>
        public void ChangeArticleAction()
        {
            Trace.WriteLine("HELLO");
            var item = articles.FirstOrDefault(i => i.Article.Name == "Pizza_A");
            if (item != null)
            {
                item.ChangeName("Uppdaterad");
                item.ChangePrice(666.0f);
            }
            else
            {
                articles.Add(new ArticleItemViewModel(new Article { Name = "Pizza_C", Price = 129.0f }));
            }
        }
    }
}
