using jkb_rebase.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace jkb_rebase.ViewModel
{
    public class IndexViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        public ICommand UpdateViewCommand { get; set; }

        public IndexViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
        //private BaseViewModel _selectedViewModel;
        ///// <summary>
        ///// Accessing the selectedViewModel
        ///// </summary>
        //public BaseViewModel SelectedViewModel
        //{
        //    get { return _selectedViewModel; }
        //    set
        //    {
        //        _selectedViewModel = value;
        //        OnPropertyChanged(nameof(SelectedViewModel));
        //    }
        //}
        ////private enum ViewModels
        ////{
        ////    SplashViewModel,
        ////    CustomerViewModel,
        ////    AdminViewModel
        ////}
        ////public ICommand SplashCommand { get; set; }
        ////public ICommand CustCommand { get; set; }
        ////public ICommand SwapPage { get; set; }
        //public ICommand UpdateViewCommand { get; set; }
        ////public event PropertyChangedEventHandler PropertyChanged;
        ////private void OnPropertyChanged(string propName)
        ////{
        ////    if (PropertyChanged != null)
        ////    {
        ////        PropertyChanged(this, new PropertyChangedEventArgs(propName));
        ////    }
        ////}
        ///// <summary>
        ///// Constructor
        ///// </summary>
        //public IndexViewModel()
        //{
        //    //SplashCommand = new UpdateViewCommand(OpenSplash);
        //    //CustCommand = new UpdateViewCommand(OpenCustomer);
        //    UpdateViewCommand = new UpdateViewCommand(this);
        //}
        ////private void OpenSplash(object obj)
        ////{
        ////    SelectedViewModel = new SplashViewModel();
        ////}

        ////private void OpenCustomer(object obj)
        ////{
        ////    SelectedViewModel = new CustomerViewModel();
        ////}
    }
}
