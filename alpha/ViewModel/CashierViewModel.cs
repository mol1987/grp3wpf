using Library.TypeLib;
using Library.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;

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
            foreach (var item in tempOrders)
            {
                Orders.Add(item);
            }
        }

        public void fooAction(object arg)
        {
            var data = (ArticleItemDataModel)arg;
            //todo; make typechec
            Trace.WriteLine("pressed");
        }
    }
}
