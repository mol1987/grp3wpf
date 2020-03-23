using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Library.TypeLib;

namespace alpha
{
    /// <summary>
    /// Terminal for the customer
    /// </summary>
    public class CustomerViewModel : BaseViewModel
    {
        /// <summary>
        /// Hidden articles, stored so we can use type filtering for display
        /// </summary>
        private List<ArticleItemDataModel> storedArticles = new List<ArticleItemDataModel>();

        /// <summary>
        /// Displayed articles
        /// </summary>
        public ObservableCollection<ArticleItemDataModel> Articles { get; set; } = new ObservableCollection<ArticleItemDataModel>();

        /// <summary>
        /// Customer added items
        /// </summary>
        public ObservableCollection<ArticleItemDataModel> Checkout { get; set; } = new ObservableCollection<ArticleItemDataModel>();

        /// <summary>
        /// Buttons in the top for filtering the view
        /// </summary>
        public ObservableCollection<string> Buttons { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// todo; probably going to remove this
        /// </summary>
        public ArticleItemDataModel SelectedArticle { get; set; }

        /// <summary>
        /// Articles are filtered by this
        /// </summary>
        public string CurrentlyDisplayedType { get; set; } = null;

        /// <summary>
        /// Testing out, todo; remove this
        /// </summary>
        public ICommand ChangeArticle { get { return new RelayCommand(param => this.ChangeArticleAction(), null); } }

        /// <summary>
        /// When "<see cref="FilterButton"/>" is clicked
        /// </summary>
        public ICommand FilterArticles { get { return new RelayCommand(param => this.FilterArticleAction(param), null); } }

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomerViewModel()
        {
            // Loaded Data from WebApi
            foreach (var item in Global.Articles)
            {
                // Storage
                storedArticles.Add(new ArticleItemDataModel(item));

                // Displayed Articles
                Articles.Add(new ArticleItemDataModel(item));

                // Create buttons for each type
                Buttons.Add(item.Type);
            }
        }

        /// <summary>
        /// Swapping View Action
        /// </summary>
        public void ChangeArticleAction()
        {
            Trace.WriteLine("HELLO");
            var item = Articles.FirstOrDefault(i => i.Article.Name == "Pizza_A");
            if (item != null)
            {
                item.ChangeName("Uppdaterad");
                item.ChangePrice(666.0f);
            }
            else
            {
                Articles.Add(new ArticleItemDataModel(new Article { Name = "Pizza_C", Price = 129.0f }));
            }
        }

        public void FilterArticleAction()
        {
            Trace.WriteLine("Error[], not supplying argument");
        }

        /// <summary>
        /// Loop and replace currently displayed articles
        /// </summary>
        /// <param name="arg">Name of article type</param>
        public void FilterArticleAction(object arg)
        {
            // From the buttons' commandArgument
            string selectedType = (string)arg;

            // When 'home' is hit. Reset displayed articles to show all
            if(selectedType == "All")
            {
                Articles.Clear();

                foreach (var item in storedArticles)
                {
                    Articles.Add(item);
                }

                // Return point here
                return;
            }

            // If not, filter by selected type
            var _ = storedArticles.Where(a => a.Article.Type == selectedType);

            Articles.Clear();

            foreach(var item in _)
            {
                Articles.Add(item);
            }
        }
    }
}
