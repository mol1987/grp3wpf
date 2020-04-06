using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Library.TypeLib;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using alpha.Model;
using System.Diagnostics;
using System.Windows.Media;

namespace alpha
{
    public class AdminViewModel : BaseViewModel
    {
        #region Collections

        // Private collections for data comparing,
        // I do these as a fix, since I don't properly understand
        // how to trigger notifications inside an observable collection
        // when elements are altered from a datagrid
        private List<Article> _articles { get; set; } = new List<Article>();
        private List<Employee> _employees { get; set; } = new List<Employee>();
        private List<Ingredient> _ingredients { get; set; } = new List<Ingredient>();

        private ObservableCollection<ArticleModel> _pArticles = new ObservableCollection<ArticleModel>() { };
        public ObservableCollection<ArticleModel> Articles { get { return _pArticles; } set { _pArticles = value; } }
        public ObservableCollection<EmployeeModel> Employees { get; set; } = new ObservableCollection<EmployeeModel>();
        public ObservableCollection<IngredientModel> Ingredients { get; set; } = new ObservableCollection<IngredientModel>();
        public Ingredient SelectedRemoveIngredient { get; set; }
        public Ingredient SelectedAddIngredient { get; set; }

        /// <summary>
        /// Dictionary to keep track of changes in what collection
        /// key = collectionname
        /// val = element-number
        /// </summary>
        private Dictionary<string, int> ToBeUpdated = new Dictionary<string, int>();

        #endregion

        #region Item Bindings

        /// <summary>
        /// Header display
        /// </summary>
        public string Title { get; set; } = "AdminTerminal";
        //private Brush titleColor = (SolidColorBrush)new BrushConverter().ConvertFromString("#F9F9F9");
        //public System.Windows.Media.SolidColorBrush TitleColor { get; set; } = (SolidColorBrush)new BrushConverter().ConvertFromString("#F9F9F9");

        /// <summary>
        /// Quick fix for making vertical scrolling work
        /// </summary>
        public string ScrollViewerHeight { get; set; } = "800";

        // Private Holder
        private dynamic _selectedItem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public dynamic DynSelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; }
        }

        public int SelectedIndex { get; set; }

        /// <summary>
        /// Grays out update button
        /// </summary>
        public string UpdateButtonEnabled { get; set; } = "Enabled";

        #endregion

        #region Commands

        private ICommand _gridUpdate { get; set; }
        public ICommand GridUpdate { get { return new RelayCommand(param => this.TargetUpdated(param), null); } }

        // Private holder
        private ICommand _updateData { get; set; }
        /// <summary>
        /// On updates checks all the differencies
        /// </summary>
        public ICommand UpdateData { get { return new RelayCommand(param => this.UpdateDataAction(param), null); } }

        public ICommand RemoveIngredient { get { return new RelayCommand(param => this.RemoveIngredientAction(param), null); } }
        public ICommand AddIngredient { get { return new RelayCommand(param => this.AddIngredientAction(param), null); } }

        #endregion

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

        #region Constructor
        // Constructor is called each time currentpage is loaded
        public AdminViewModel()
        {
            if (IsInDesignMode) { return; }

            // Kick out if unauthorized
            if (!Global.HasAuthenticationRoles("admin"))
            {
                System.Windows.MessageBox.Show("-----Current role is unauthorized----- Not loading data");
                return;
            }

            Articles.CollectionChanged += items_CollectionChanged;
            Employees.CollectionChanged += items_CollectionChanged;
            Ingredients.CollectionChanged += items_CollectionChanged;


            // Load SQL data
            RunAsyncActions();

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private async void RunAsyncActions()
        {
            await LoadData();
        }

        // Left empty
        private async void UpdateDataAction(object args) { }

        /// <summary>
        /// On propchange INSIDE class-item in observablecollection
        /// This is a quick-fix since it doesn't update normally
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                var workingItem = e.OldItems[0];
                // Article
                if (workingItem is ArticleModel)
                {
                    var workingArticle = (ArticleModel)e.OldItems[0];
                    await Global.ArticleRepo.DeleteRowAsync((int)workingArticle.ID);
                }
                // Employee
                if (workingItem is EmployeeModel)
                {
                    var workingEmployee = (EmployeeModel)e.OldItems[0];
                    await Global.EmployeeRepo.DeleteRowAsync((int)workingEmployee.ID);
                }
                // Ingredient
                if (workingItem is IngredientModel)
                {
                    var workingIngredient = (IngredientModel)e.OldItems[0];
                    await Global.IngredientRepo.DeleteRowAsync((int)workingIngredient.ID);
                }
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= item_PropertyChanged;
            }
            if (e.NewItems != null)
            {

                // if new row, fill with garbage
                var workingItem = e.NewItems[0];
                // Articles
                if (workingItem is ArticleModel)
                {
                    var workingArticle = (ArticleModel)e.NewItems[0];
                    Article getArticle = new Article() { BasePrice = 80, Name = "blank", IsActive = true, Type = "blank" };

                    if (workingArticle.Name == null)
                    {
                        await Global.ArticleRepo.InsertAsync(getArticle);
                        Articles.Last().ID = getArticle.ID;
                        Articles.Last().Ingredients = new List<Ingredient>();
                    }
                }
                // Employees
                if (workingItem is EmployeeModel)
                {
                    var workingEmployee = (EmployeeModel)e.NewItems[0];
                    Employee getEmployee = new Employee() { Name = "blank", LastName = "blanksson", Email = "blank@blank.se", Password = "123" };

                    if (workingEmployee.Name == null)
                    {
                        await Global.EmployeeRepo.InsertAsync(getEmployee);
                        Employees.Last().ID = getEmployee.ID;
                    }
                }
                // Ingredients
                if (workingItem is IngredientModel)
                {
                    var workingEmployee = (IngredientModel)e.NewItems[0];
                    Ingredient getIngredient = new Ingredient() { Name = "blank", Price = 10 };

                    if (workingEmployee.Name == null)
                    {
                        await Global.IngredientRepo.InsertAsync(getIngredient);
                        Ingredients.Last().ID = getIngredient.ID;
                    }
                }
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += item_PropertyChanged;
            }
        }

        /// <summary>
        /// On propchange INSIDE class-item in observablecollection
        /// This is a quick-fix since it doesn't update normally
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Trace.WriteLine("prop changed");
            if (DynSelectedItem == null) return;

            // Article updated
            if (DynSelectedItem is ArticleModel)
            {
                if (e.PropertyName == "AddIngredient" ||
                    e.PropertyName == "SelectedAddIngredient" ||
                    e.PropertyName == "RemoveIngredient" ||
                    e.PropertyName == "SelectedRemoveIngredient")
                    return;
                var selectedArticle = (ArticleModel)DynSelectedItem;
                await Global.ArticleRepo.UpdateAsync(new Article() { ID = selectedArticle.ID, BasePrice = selectedArticle.BasePrice, Name = selectedArticle.Name, IsActive = selectedArticle.IsActive, Type = selectedArticle.Type, Ingredients = selectedArticle.Ingredients });
            }
            // Employee updated
            if (DynSelectedItem is EmployeeModel)
            {
                Trace.WriteLine("hello");
                var selectedArticle = (EmployeeModel)DynSelectedItem;
                await Global.EmployeeRepo.UpdateAsync(new Employee() { ID = selectedArticle.ID, Email = selectedArticle.Email, Name = selectedArticle.Name, LastName = selectedArticle.LastName, Password = selectedArticle.Password });
            }
            // Ingredient updated
            if (DynSelectedItem is IngredientModel)
            {
                var selectedArticle = (IngredientModel)DynSelectedItem;
                await Global.IngredientRepo.UpdateAsync(new Ingredient() { ID = selectedArticle.ID, Name = selectedArticle.Name, Price = selectedArticle.Price });
            }
            Trace.WriteLine("prop " + e.PropertyName);
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task LoadData()
        {
            // Repo inits
            var articleRepo = new Library.Repository.ArticlesRepository("Articles");
            var employeeRepo = new Library.Repository.EmployeesRepository("Employees");
            var ingredientRepo = new Library.Repository.IngredientsRepository("Ingredients");

            // The private lists are clones
            foreach (var item in await articleRepo.GetAllAsync())
            {
                var tempArticle = new ArticleModel { ID = item.ID, Name = item.Name, BasePrice = item.BasePrice, Type = item.Type, IsActive = item.IsActive };
                tempArticle.Ingredients = new List<Ingredient>();
                foreach (var itemIngredients in item.Ingredients.ToList())
                {
                    tempArticle.Ingredients.Add(new Ingredient { ID = itemIngredients.ID, Name = itemIngredients.Name, Price = itemIngredients.Price });
                }
                Articles.Add(tempArticle);
            }

            foreach (var item in await employeeRepo.GetAllAsync())
            {
                Employees.Add(new EmployeeModel { ID = item.ID, Name = item.Name, LastName = item.LastName, Email = item.Email, Password = item.Password });
            }

            foreach (var item in await ingredientRepo.GetAllAsync())
            {
                Ingredients.Add(new IngredientModel { ID = item.ID, Name = item.Name, Price = item.Price });
            }
        }

        private void RemoveIngredientAction(object args)
        {
            Trace.WriteLine(SelectedRemoveIngredient.Name);
        }
        private void AddIngredientAction(object args)
        {
            Trace.WriteLine(args.ToString());
        }

        #endregion

        #region Frustration
        public void SelectedIndexChangedEvent()
        {

        }
        public void TargetUpdated(object args)
        {
            Trace.WriteLine("hohoh");
        }
        #endregion
    }
}
