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

namespace alpha
{
    public class AdminViewModel : BaseViewModel
    {
        // Private collections for data comparing,
        // I do these as a fix, since I don't properly understand
        // how to trigger notifications inside an observable collection
        // when elements are altered from a datagrid
        private List<Article> _articles = new List<Article>();
        private List<Employee> _employees = new List<Employee>();
        private List<Ingredient> _ingredients = new List<Ingredient>();

        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>() {};
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();

        /// <summary>
        /// Header display
        /// </summary>
        public string Title { get; set; } = "AdminTerminal";

        /// <summary>
        /// Quick fix for making vertical scrolling work
        /// </summary>
        public string ScrollViewerHeight { get; set; } = "800";

        /// <summary>
        /// 
        /// </summary>
        public dynamic SelectedItem { get; set; }

        /// <summary>
        /// Grays out update button
        /// </summary>
        public string UpdateButtonEnabled { get; set; } = "Enabled";

        // Private holder
        private ICommand _updateData { get; set; }
        /// <summary>
        /// On updates checks all the differencies
        /// </summary>
        public ICommand UpdateData { get { return new RelayCommand(param => this.UpdateDataAction(param), null); } }

        /// <summary>
        /// Dictionary to keep track of changes in what collection
        /// key = collectionname
        /// val = element-number
        /// </summary>
        private Dictionary<string, int> ToBeUpdated = new Dictionary<string, int>();

        /// <summary>
        /// 
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


        #region Constructor
        // Constructor is called each time currentpage is loaded
        public AdminViewModel()
        {
            if (IsInDesignMode) { return; }

            //  // Not being able to scroll is very annoying, todo; do something
            //  double height = Global.ActualWindow.Height;
            //  ScrollViewerHeight = (height - 20.0).ToString();

            // Load SQL data
            RunAsyncActions();

            // Listeners for changes in Collections
            Articles.CollectionChanged += ArticlesCollectionChanged;
            Employees.CollectionChanged += EmployeesCollectionChanged;
            Ingredients.CollectionChanged += IngredientsCollectionChanged;
        }
        #endregion


        private async void RunAsyncActions()
        {
            await LoadData();
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
                Articles.Add(item);
                _articles.Add(item);
            }

            foreach (var item in await employeeRepo.GetAllAsync())
            {
                Employees.Add(item);
                _employees.Add(item);
            }

            foreach (var item in await ingredientRepo.GetAllAsync())
            {
                Ingredients.Add(item);
                _ingredients.Add(item);
            }
        }

        /// <summary>
        /// On button click
        /// </summary>
        /// <param name="args"></param>
        private void UpdateDataAction(object args)
        {
            var articleDifferences = Articles.Except(_articles).ToList();
            var employeeDifferences = Employees.Except(_employees).ToList();
            var ingredientDifferences = Ingredients.Except(_ingredients).ToList();

            var x = 0;

            ////  Since we have three different types of data
            ////  This very rough logic to differentiate
            //Type type = SelectedItem.GetType();

            //if (SelectedItem == null)
            //{
            //    MessageBox.Show("Nothing was selected");
            //    return;
            //}

            //if (type.Equals(typeof(Article)))
            //{
            //    //todo; Add database logic
            //    ToBeUpdated.Add("Articles", SelectedItem.ID);
            //    MessageBox.Show("Articles thingy to be updated todo; Add database logic");
            //}
            //else if (type.Equals(typeof(Employee)))
            //{
            //    //todo; Add database logic
            //    ToBeUpdated.Add("Employees", SelectedItem.ID);
            //    MessageBox.Show("Employee thingy to be updated todo; Add database logic");
            //}
            //else if (type.Equals(typeof(Ingredient)))
            //{
            //    //todo; Add database logic
            //    ToBeUpdated.Add("Ingredients", SelectedItem.ID);
            //    MessageBox.Show("Ingredient thingy to be updated todo; Add database logic");
            //}
            //// Reset
            //SelectedItem = null;
        }
        private void ArticlesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var x = sender;
           //ToBeUpdated.Add()
        }
        private void EmployeesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }
        private void IngredientsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }
        public void SelectedIndexChangedEvent()
        {

        }
    }
}
