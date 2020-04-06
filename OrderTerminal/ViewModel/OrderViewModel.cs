using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Library.TypeLib;
using Library.WebApiFunctionality;
using System.Linq;
using Library.Extensions.ObservableCollection;
using System.ComponentModel;
using System.Windows;

namespace OrderTerminal
{
    /// <summary>
    /// Public terminal for Customers over the availability status of their respective paid orders
    /// </summary>
    public class OrderViewModel : BaseViewModel
    {
        public string Title { get; set; } = "OrderView";
        public string LeftTitle { get; set; } = "Pågående";
        public string RightTitle { get; set; } = "Färdiga";

        /// <summary>
        /// Active order to be watched
        /// </summary>
        public ObservableCollection<Order> Orders { get; set; } = new AsyncObservableCollection<Order>();
        public ObservableCollection<Order> OrdersDone { get; set; } = new AsyncObservableCollection<Order>();

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

        public OrderViewModel()
        {
            if (IsInDesignMode) { return; }
            //
            SeverInit();
        }


        /// <summary>
        /// 
        /// </summary>
        private void SeverInit()
        {
            bool res = Library.Helper.Environment.LoadEnvFile() ? true : false;
            LoadOrders(); // cant load sql repo


            // start up the webAPI server
            // and adding the method to the event
            WebApiServer.returnOrderEvent += ManageOrders;
            WebApiServer.StartServer();

        }

        /// <summary>
        /// Initial Load Orders from SQL
        /// </summary>
        async void LoadOrders()
        {
            // Boot up sql
            var repo = new Library.Repository.OrdersRepository("orders");
            var res = await repo.GetAllAsync();

            // using the Enum.Foreach extension
            res.ForEach(item => { if (item.Orderstatus == 1) Orders.Add(item); 
                                  if (item.Orderstatus == 2) OrdersDone.Add(item); });

        }

        /// <summary>
        /// Method for managing orders coming in from the other terminals
        /// over web API throws an exception if the ordernumber is incorrect for the call
        /// that the client can use to know that it was an incorrect call
        /// </summary>
        /// <param name="orderNo">The order's Number</param>
        /// <param name="typeOrder">Type of order command: PlaceOrder, DoneOrder, RemoveOrder</param>
        private void ManageOrders(int orderNo, TypeOrder typeOrder)
        {
            Trace.WriteLine(typeOrder.ToString() + " ");
            switch (typeOrder)
            {
                // handle a new order. throws an exception if there is already an order with same number
                case TypeOrder.placeorder:
                    if (Orders.Any(x => x.ID == orderNo) != true)
                    {
                        Orders.Add(new Order { ID = orderNo, CustomerID = 99, Orderstatus = 0, Price = 99, TimeCreated = GetRandomTime() });
                    }
                    else throw new Exception();
                    break;
                // handle an order that is ready to get picked up. throws an exception if there isnt any order with that ordernumber
                case TypeOrder.doneorder:
                    var tempOrder = Orders.Single(x => x.ID == orderNo);
                    if (Orders.Contains(tempOrder) == true)
                    {
                        Orders.Remove(tempOrder);
                        tempOrder.Orderstatus = 1;
                        OrdersDone.Add(tempOrder);
                    }
                    break;
                // handle removing an order. throws an exception if there isnt any order with that ordernumber
                case TypeOrder.removeorder:
                    tempOrder = OrdersDone.Single(x => x.ID == orderNo);
                    if (OrdersDone.Contains(tempOrder) == true)
                    {                       
                        OrdersDone.Remove(tempOrder);
                    }
                    break;
                default:
                    throw new Exception();
                    break;
            }
        }

        private DateTime GetRandomTime()
        {
            DateTime a = DateTime.Now;
            var rand = new Random();

            // Add a random time between 15 and 45 secs ago
            a.AddSeconds(rand.Next(-45, -15));
            return a;
        }
    }
}
