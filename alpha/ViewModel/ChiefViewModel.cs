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

        public ObservableCollection<DisplayObject> Orders { get; set; } = new ObservableCollection<DisplayObject>();

        public ObservableCollection<DisplayObject> FinishedOrders { get; set; } = new ObservableCollection<DisplayObject>();


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
            //Sql connection
            List<DisplayObject> filteredList = new List<DisplayObject>();
            List<DisplayObject> res = (await new Library.Repository.OrdersRepository("Orders").GetAllAsync(1)).ToList();

            // Filter and smash together if the same OrderID
            // This is done because of how each ingredient is an
            // unique SQL row and has to be stuck into the same string
            res.ForEach(a =>
            {
                bool isMatch = false;
                // ...
                filteredList
                    .Where(b => b.ArticleOrderID == a.ArticleOrderID)
                    .ToList()
                    .ForEach(c =>
                    {
                        c.Ingredients.Add(new Ingredient { Name = a.IngredientsName });
                        c.IngredientsName += String.Format(", {0}", a.IngredientsName);
                        isMatch = true;
                    });

                // Return to avoid duplicate
                if (isMatch)
                    return;

                // New object
                filteredList.Add(a);
            });

            filteredList.ForEach(d => Orders.Add(d));


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
            var dobj = (DisplayObject)args;

            // Remove from left side
            // Removes all with the same OrderID at the moment
            foreach(var item in Orders.ToList())
            {
                if(item.OrderID == dobj.OrderID)
                {
                    //
                    Orders.Remove(item);
                    // Done after
                    item.OrderStatus = 2;
                    // Add to right
                    FinishedOrders.Add(item);
                }
            }
            
            //todo; sql
            Order updOrder = new Order() { ID = dobj.OrderID , TimeCreated = dobj.TimeStamp,  Orderstatus = dobj.OrderStatus};
            await General.ordersRepo.UpdateAsync(updOrder);

            // webAPI try sending update to the OrderTerminalen return after more than five
            int i = 0;
            while (await WebApiClient.DoneOrderAsync(dobj.OrderID, TypeOrder.doneorder) == false)
            {
                await Task.Delay(500);
                if (i++ > 5) return;
            }
            Trace.WriteLine("ok");
        }
    }
}
