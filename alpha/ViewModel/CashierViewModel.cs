using Library.TypeLib;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;
using Library.WebApiFunctionality;
using System.Threading.Tasks;

namespace alpha
{
    /// <summary>
    /// Public terminal that is used by the paying Customer
    /// </summary>
    public class CashierViewModel : BaseViewModel
    {
        public string Title { get; set; } = "Cashier View";

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();


        // Private holder
        private ICommand _foo;
        /// <summary>
        /// Command for swapping view
        /// </summary>
        public ICommand foo { get { return _foo ?? new RelayCommand(param => this.fooAction(param), null); } }

        #region Constructor
        public CashierViewModel()
        {
            LoadOrders();
        } 
        #endregion

        async void LoadOrders()
        {
            List<Order> tempOrders = (await General.ordersRepo.GetAllAsync()).ToList();
            Orders.Clear();
            foreach (var item in tempOrders.Where(x => x.Orderstatus == 2))
            {
                Orders.Add(item);
            }
        }

        public async void fooAction(object arg)
        {
            //todo; make typechec
            var workingOrder = Orders.Single(x => x.ID == (int)arg);
            workingOrder.Orderstatus = 3;
            await Global.OrderRepo.UpdateAsync(workingOrder);
            LoadOrders();

            // webAPI try sending update to the OrderTerminalen return after more than five
            int i = 0;
            while (await WebApiClient.DoneOrderAsync((int)arg, TypeOrder.removeorder) == false)
            {
                await Task.Delay(500);
                if (i++ > 5) return;
            }
            Trace.WriteLine("ok");
        }
    }
}
