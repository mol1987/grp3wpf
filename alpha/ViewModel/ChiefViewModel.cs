using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library.Repository;
using Library.TypeLib;
using Library.WebApiFunctionality;

namespace alpha
{
    /// <summary>
    /// Display terminal for Chiefs and Employees
    /// </summary>
    public class ChiefViewModel : BaseViewModel
    {
        public string Title { get; set; } = "ChiefTerminal";


        public ObservableCollection<OrderModel> Orders { get; set; } = new ObservableCollection<OrderModel>();

        public ObservableCollection<OrderModel> FinishedOrders { get; set; } = new ObservableCollection<OrderModel>();


        private ICommand _setOrderDone;
        public ICommand SetOrderDone { get { return new RelayCommand(param => this.SetOrderDoneAction(param), null); } }

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
        /// <summary>
        /// Initieated on pageswap
        /// </summary>
        public ChiefViewModel()
        {
            if (IsInDesignMode) { return; }

            LoadOrders();
        }
        #endregion

        /// <summary>
        /// ...
        /// </summary>
        private async void AsyncActions()
        {

        }

        /// <summary>
        /// ...
        /// </summary>
        async void LoadOrders()
        {

            // To avoid having the slide-in aniamtion freezing
            // takes 450ms for animation to complete
            await Task.Delay(450);

            ////Sql connection
            //List<DisplayObject> filteredList = new List<DisplayObject>();
            //List<DisplayObject> res = (await new Library.Repository.OrdersRepository("Orders").GetAllAsync(1)).ToList();

            //// Filter and smash together if the same OrderID
            //// This is done because of how each ingredient is an
            //// unique SQL row and has to be stuck into the same string
            //res.ForEach(a =>
            //{
            //    bool isMatch = false;
            //    // ...
            //    filteredList
            //        .Where(b => b.ArticleOrderID == a.ArticleOrderID)
            //        .ToList()
            //        .ForEach(c =>
            //        {
            //            c.Ingredients.Add(new Ingredient { Name = a.IngredientsName });
            //            c.IngredientsName += String.Format(", {0}", a.IngredientsName);
            //            isMatch = true;
            //        });

            //    // Return to avoid duplicate
            //    if (isMatch)
            //        return;

            //    // New object
            //    filteredList.Add(a);
            //});

            //filteredList.ForEach(d => { Orders.Add(d);  });

            var tempOrders = (await Global.OrderRepo.GetAllAsync()).ToList();
            tempOrders.ForEach(async x => {
                if (x.Orderstatus == 1)
                {
                    await Global.OrderRepo.GetAllAsync(x);
                    List<ArticleModel> tempArticles = new List<ArticleModel>();
                    x.Articles.ForEach(y => tempArticles.Add(new ArticleModel { ID = y.ID, BasePrice = y.BasePrice, Name = y.Name, IsActive = y.IsActive, Ingredients = y.Ingredients }));
                    Orders.Add(new OrderModel { ID = x.ID, CustomerID = x.CustomerID, Orderstatus = x.Orderstatus, Price = x.Price, TimeCreated = x.TimeCreated, Articles = tempArticles });
                }
            });

            


            //Orders.Add(a)

            //List<Order> tempOrders = (await General.ordersRepo.GetAllAsync()).ToList();
            //foreach (var item in tempOrders)
            //{
            //    Orders.Add(item);
            //}
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <param name="args"></param>
        private async void SetOrderDoneAction(object args)
        {
            var dobj = (OrderModel)args;

            // Remove from left side
            // Removes all with the same OrderID at the moment
            
            foreach(var item in Orders)
            {
                if(item.ID == dobj.ID)
                {
                    //
                    Orders.Remove(item);
                    // Done after
                    item.Orderstatus = 2;
                    // Add to right
                    FinishedOrders.Add(item);
                    break;
                }
            }
            
            //todo; sql
            Order updOrder = new Order() { ID = dobj.ID , TimeCreated = dobj.TimeCreated,  Orderstatus = dobj.Orderstatus};
            await General.ordersRepo.UpdateAsync(updOrder);

            // webAPI try sending update to the OrderTerminalen return after more than five
            await WebApiClient.DoneOrderAsync((int)dobj.ID, TypeOrder.doneorder);

            Trace.WriteLine("ok");
        }
    }
}
