using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Library.TypeLib;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;

namespace alpha
{
    public class AdminViewModel : BaseViewModel
    {
        public ObservableCollection<Article> Articles { get; set; } = new ObservableCollection<Article>() {};
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Ingredient> Ingredients { get; set; } = new ObservableCollection<Ingredient>();

        /// <summary>
        /// Quick fix for making vertical scrolling work
        /// </summary>
        public string ScrollViewerHeight { get; set; } = "800";

        public dynamic SelectedItem { get; set; }

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


        #region Constructor
        // Constructor is called each time currentpage is loaded
        public AdminViewModel()
        {
            //  // Not being able to scroll is very annoying, todo; do something
            //  double height = Global.ActualWindow.Height;
            //  ScrollViewerHeight = (height - 20.0).ToString();

            // Load SQL data
            LoadData();

            // Listeners for changes in Collections
            Articles.CollectionChanged += ArticlesCollectionChanged;
            Employees.CollectionChanged += EmployeesCollectionChanged;
            Ingredients.CollectionChanged += IngredientsCollectionChanged;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private async void LoadData()
        {
            // Repo inits
            var articleRepo = new Library.Repository.ArticlesRepository("Articles");
            var employeeRepo = new Library.Repository.EmployeesRepository("Employees");
            var ingredientRepo = new Library.Repository.IngredientsRepository("Ingredients");


            foreach (var item in await articleRepo.GetAllAsync())
            {
                Articles.Add(item);
            }

            foreach (var item in await employeeRepo.GetAllAsync())
            {
                Employees.Add(item);
            }

            foreach (var item in await ingredientRepo.GetAllAsync())
            {
                Ingredients.Add(item);
            }
        }

        /// <summary>
        /// On button click
        /// </summary>
        /// <param name="args"></param>
        private void UpdateDataAction(object args)
        {
            //  Since we have three different types of data
            //  This very rough logic to differentiate
            Type type = SelectedItem.GetType();

            if (SelectedItem == null)
            {
                MessageBox.Show("Nothing was selected");
                return;
            }

            if (type.Equals(typeof(Article)))
            {
                //todo; Add database logic
                ToBeUpdated.Add("Articles", SelectedItem.ID);
                MessageBox.Show("Articles thingy to be updated todo; Add database logic");
            }
            else if (type.Equals(typeof(Employee)))
            {
                //todo; Add database logic
                ToBeUpdated.Add("Employees", SelectedItem.ID);
                MessageBox.Show("Employee thingy to be updated todo; Add database logic");
            }
            else if (type.Equals(typeof(Ingredient)))
            {
                //todo; Add database logic
                ToBeUpdated.Add("Ingredients", SelectedItem.ID);
                MessageBox.Show("Ingredient thingy to be updated todo; Add database logic");
            }
            // Reset
            SelectedItem = null;
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
