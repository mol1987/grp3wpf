using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Library.TypeLib;

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
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();

        public OrderViewModel()
        {
            Orders.Add(new Order { ID = 41, CustomerID = 99, Orderstatus = 1, Price = 99, TimeCreated = GetRandomTime() });
            Orders.Add(new Order { ID = 40, CustomerID = 99, Orderstatus = 1, Price = 199, TimeCreated = GetRandomTime() });
            Orders.Add(new Order { ID = 39, CustomerID = 100, Orderstatus = 1, Price = 299, TimeCreated = GetRandomTime() });
            Orders.Add(new Order { ID = 49, CustomerID = 101, Orderstatus = 1, Price = 399, TimeCreated = GetRandomTime() });
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
