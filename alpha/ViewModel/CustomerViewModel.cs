using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Library.TypeLib;
using Library.Repository;
using System.Windows;
using System.Threading.Tasks;
using System.ComponentModel;
using Library.WebApiFunctionality;
//using Library.Extensions.ObservableCollection;

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
        /// 
        /// </summary>
        public ObservableCollection<IngredientModel> Ingredients { get; set; } = new ObservableCollection<IngredientModel>();

        /// <summary>
        /// Buttons in the top for filtering the view
        /// </summary>
        public ObservableCollection<FilterButton> FilterButtons { get; set; } = new ObservableCollection<FilterButton>();

        /// <summary>
        /// todo; probably going to remove this
        /// </summary>
        public ArticleItemDataModel SelectedArticle { get; set; }

        /// <summary>
        /// Articles are filtered by this
        /// </summary>
        public string CurrentlyDisplayedType { get; set; } = null;

        /// <summary>
        /// Toggles modal off and on
        /// </summary>
        public bool IsModal { get; set; } = false;

        //public string TerminalLockStatus { get{ return Global.Isterminallockedstatus ? "locked" : "unlocked"}}

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
        public ICommand SetToChecked { get { return new RelayCommand(param => this.EmptyTestAction(param), null); } }

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
        /// Close and Show Custom Article menu
        /// </summary>
        public ICommand ToggleModal { get { return new RelayCommand(param => this.ToggleModalAction(param), null); } }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ToggleModalAndResetIngredients
        {
            get
            {
                return new RelayCommand(param =>
                {
                    this.ToggleModalAction(param);
                    this.ResetIngredients();
                }, null);
            }
        }

        /// <summary>
        /// From Custom article modal menu
        /// Param = IngredientModel
        /// </summary>
        public ICommand CreateNewCustomArticle { get { return new RelayCommand(param => this.CreateNewCustomArticleAction(param), null); } }

        public ICommand LockTerminal { get { return new RelayCommand(param => {
            Global.IsTerminalLocked = Global.IsTerminalLocked ? false : true;
        }, null); } }

        #region ? in designmode

        /// <summary>
        /// Fixes the UI/Data-load bug
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor
                    .FromProperty(prop, typeof(FrameworkElement))
                    .Metadata.DefaultValue;
            }
        }

        #endregion

        /// <summary>
        /// Constructor, activates on page/frame load
        /// </summary>
        public CustomerViewModel()
        {
            if (IsInDesignMode) { return; }

            // Loaded Data from WebApi
            var items = Global.Articles;

            // One nav Button for The "Hem"-knapp
            FilterButtons.Add(new FilterButton { Type = "Hem" });

            foreach (var item in items)
            {
                // Storage, used for filtering later on
                storedArticles.Add(new ArticleItemDataModel(item));

                // Displayed Articles
                Articles.Add(new ArticleItemDataModel(item));

                // Create nav buttons for each type once
                if (FilterButtons.Where(fb => fb.Type == item.Type).Count() < 1)
                    FilterButtons.Add(new FilterButton { Type = item.Type });
            }

            // ...
            RunAsyncActions();

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
            if (selectedType == "All" || selectedType == "Hem")
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
        /// Boolean switch
        /// </summary>
        /// <returns></returns>
        public bool ToggleModalAction(object arg) => IsModal = IsModal ? false : true;

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
        private async void PurchaseCheckoutItemsAction(object args)
        {
            var repo = new Library.Repository.OrdersRepository("Orders");
            string result = "Something went wrong";

            // Datauppkoppling och spara data som finns i Checkout
            Order order = new Order {
                CustomerID = 1337,
                Orderstatus = 1,
                Price = CheckOutSum,
                Articles = new List<Article>(),
                //System.DateTime.Now.ToString("yy-MM-dd hh:mm:ss")
                TimeCreated = System.DateTime.Now,
            };

            foreach (var articleDataModel in Checkout)
            {
                // Deconstruct into Article
                Article newArticle = articleDataModel.Article;
                order.Articles.Add(newArticle);
            }


            await repo.InsertAsync(order);

            // webAPI client code
            int i = 0;
            await WebApiClient.DoneOrderAsync((int)order.ID, TypeOrder.placeorder);
            

            // Some info output
            result = string.Format("Purchased. Your order-ID is <{0}>. Total price is {1}.", order.ID, CheckOutSum);
            MessageBox.Show(result);

            // Cleara ut
            Checkout.Clear();

            // Reset Counter
            NumberOfItemsInCheckout = Checkout.Count();

            // Reset price
            CheckOutSum = CheckOutSum * 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void CreateNewCustomArticleAction(object args)
        {
            // Filter out unchecked ingredients
            var selectedIngredients = Ingredients.Where(a => a.IsChecked);
            // Initiate new Article model
            var newArticle = new Article() { Name = "Custom Pizza", ID = 9999, BasePrice = 40, IsActive = true, Type = "Pizza", Ingredients = new List<Ingredient>() };

            foreach (var item in Ingredients)
            {
                newArticle.Ingredients.Add(new Ingredient { ID = item.ID, Name = item.Name, Price = item.Price });
                newArticle.BasePrice += item.Price;
            }

            // Inside the wrapper thingy
            var articleItemDataModel = new ArticleItemDataModel(newArticle);

            // Add to checkout
            AddToCheckoutAction(articleItemDataModel);

            // Reset for next custom article
            ResetIngredients();
        }

        /// <summary>
        /// Set all ingredientModels to unchecked
        /// </summary>
        private void ResetIngredients()
        {
            foreach (var _ in Ingredients)
            {
                _.IsChecked = false;
            }
        }
        private void EmptyTestAction(object args)
        {
            var _ = args;
        }

        /// <summary>
        /// Async Actions
        /// </summary>
        private async void RunAsyncActions()
        {
            await LoadData();
        }

        /// <summary>
        /// Load ingredients for custom pizza
        /// </summary>
        /// <returns></returns>
        private async Task LoadData()
        {
            var ingredientRepo = new Library.Repository.IngredientsRepository("Ingredients");
            foreach (var item in await ingredientRepo.GetAllAsync())
            {
                Ingredients.Add(new IngredientModel { Name = item.Name, ID = item.ID, Price = item.Price });
            }
        }
    }
}
