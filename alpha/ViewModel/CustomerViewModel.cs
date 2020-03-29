using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Library.TypeLib;
using Library.Repository;
using System.Windows;
using System.Threading.Tasks;

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

        // Private
        private float _checkoutSum = 0;
        private void AddToCheckoutSum(float n) => CheckOutSum += n;
        private void RemoveFromCheckoutSum(float n)
        {
            // Negative sum check
            if (CheckOutSum - n <= 0)
            {
                CheckOutSum = 0;
                return;
            }
            CheckOutSum -= n;
        }
        /// <summary>
        /// Total price of articles in checkout
        /// </summary>
        public float CheckOutSum
        {
            get { return _checkoutSum; }
            set { _checkoutSum = value; }
        }

        // Private
        private int _numberOfItemsInCheckout = 0;
        /// <summary>
        /// Current number of items added to checkout
        /// </summary>
        public int NumberOfItemsInCheckout
        {
            get { return _numberOfItemsInCheckout; }
            set { _numberOfItemsInCheckout = value; }
        }

        // Private
        private string _buyButtonEnabledMode = "False";
        /// <summary>
        /// Grays out the buy button in checkout depending on if there are any items in Checkout-list
        /// </summary>
        public string BuyButtonEnabledMode
        {
            get { return _buyButtonEnabledMode; }
            set { _buyButtonEnabledMode = value; }
        }

        /// <summary>
        /// Testing out, todo; remove this
        /// </summary>
        public ICommand ChangeArticle { get { return new RelayCommand(param => this.ChangeArticleAction(), null); } }

        /// <summary>
        /// When "<see cref="FilterButton"/>" is clicked
        /// </summary>
        public ICommand FilterArticles { get { return new RelayCommand(param => this.FilterArticleAction(param), null); } }

        /// <summary>
        /// Add ParentArticle to checkout
        /// When "<see cref="AddToCheckoutButton"/>" is clicked
        /// </summary>
        public ICommand AddToCheckout { get { return new RelayCommand(param => this.AddToCheckoutAction(param), null); } }

        /// <summary>
        /// Finalize purchase with articles currently in checkout
        /// </summary>
        public ICommand PurchaseCheckoutItems { get { return new RelayCommand(param => this.PurchaseCheckoutItemsAction(param), null); } }

        /// <summary>
        /// Remove item from checkout
        /// </summary>
        public ICommand RemoveFromCheckout { get { return new RelayCommand(param => this.RemoveFromCheckoutAction(param), null); } }

        /// <summary>
        /// Constructor, activates on page/frame load
        /// </summary>
        public CustomerViewModel()
        {
            // Loaded Data from WebApi
            var items = Global.Articles;
            foreach (var item in items)
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
            var item = Articles.FirstOrDefault(i => i.Article.Name == "Pizza_A");
            if (item != null)
            {
                item.ChangeName("Uppdaterad");
                item.ChangePrice(666.0f);
            }
            else
            {
                Articles.Add(new ArticleItemDataModel(new Article { Name = "Pizza_C", BasePrice = 129.0f }));
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
            if (selectedType == "All")
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

            foreach (var item in _)
            {
                Articles.Add(item);
            }
        }

        /// <summary>
        /// Add <see cref="ArticleItemDataModel"/> to checkout list
        /// </summary>
        /// <param name="arg"></param>
        public void AddToCheckoutAction(object arg)
        {
            var data = (ArticleItemDataModel)arg;
            //todo; make typecheck
            Checkout.Add(data);
            NumberOfItemsInCheckout = Checkout.Count();
            //TotalCheckoutPrice = (float)data.Article.BasePrice;
            AddToCheckoutSum((float)data.Article.BasePrice);
            BuyButtonEnabledMode = "True";
        }

        /// <summary>
        /// Remove <see cref="ArticleItemDataModel"/> from checkout
        /// </summary>
        /// <param name="arg"></param>
        public void RemoveFromCheckoutAction(object arg)
        {
            ArticleItemDataModel item = (ArticleItemDataModel)arg;

            var articleToRemove = Checkout.First(a => a.Article.ID == item.Article.ID);
            if (articleToRemove != null)
                Checkout.Remove(articleToRemove);

            //
            RemoveFromCheckoutSum((float)articleToRemove.Article.BasePrice);

            // Recount the checkout item
            NumberOfItemsInCheckout = Checkout.Count();

            //Safety check for negatives, maybe not needed
            if (NumberOfItemsInCheckout < 0)
                NumberOfItemsInCheckout = 0;

            // Disable the buy button
            if (NumberOfItemsInCheckout < 1)
                BuyButtonEnabledMode = "False";
        }

        /// <summary>
        /// Run all <see cref="ArticleItemDataModel"/> in <see cref="Checkout"/> as purchase and add to database
        /// </summary>
        /// <param name="args"></param>
        public void PurchaseCheckoutItemsAction(object args)
        {

            //todo;
            // Gör datauppkoppling och spara data i Checkout
            var dataToSave = Checkout;
            MessageBox.Show("Purchased");

            // Cleara ut
            Checkout.Clear();

            // Reset Counter
            NumberOfItemsInCheckout = Checkout.Count();

            // Reset price
            CheckOutSum = CheckOutSum * 0;
        }
    }
}
