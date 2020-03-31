using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Library.TypeLib;
using Library.WebApiFunctionality;
using System.Linq;

namespace alpha
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

        public OrderViewModel()
        {
            //
            SeverInit();

            Orders.Add(new Order { ID = 41, CustomerID = 99, Orderstatus = 0, Price = 99, TimeCreated = GetRandomTime() });
            Orders.Add(new Order { ID = 40, CustomerID = 99, Orderstatus = 1, Price = 199, TimeCreated = GetRandomTime() });
            Orders.Add(new Order { ID = 39, CustomerID = 100, Orderstatus = 1, Price = 299, TimeCreated = GetRandomTime() });
            Orders.Add(new Order { ID = 49, CustomerID = 101, Orderstatus = 1, Price = 399, TimeCreated = GetRandomTime() });
        }


        /// <summary>
        /// 
        /// </summary>
        private void SeverInit()
        {
            if (Global.IsServerStarted)
                return;

            // start up the webAPI server
            // and adding the method to the event
            WebApiServer.returnOrderEvent += ManageOrders;
            WebApiServer.StartServer();
            Global.IsServerStarted = true;

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
            Trace.WriteLine(typeOrder.ToString() + " " + orderNo + Orders.Last().ID);
            switch (typeOrder)
            {
                // handle a new order. throws an exception if there is already an order
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
