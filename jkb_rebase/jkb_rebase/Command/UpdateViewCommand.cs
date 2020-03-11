using jkb_rebase.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace jkb_rebase.Command
{
    public class UpdateViewCommand : ICommand
    {
        /// <summary>
        /// ...
        /// </summary>
        private IndexViewModel viewModel;
        public event EventHandler CanExecuteChanged;
        public UpdateViewCommand(IndexViewModel viewModel)
        {
            this.viewModel = viewModel;
        }
        /// <summary>
        /// todo; some kind of validation here
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "Splash":
                    viewModel.SelectedViewModel = new SplashViewModel();
                    break;
                case "Customer":
                    viewModel.SelectedViewModel = new CustomerViewModel();
                    break;
                case "Admin":
                    viewModel.SelectedViewModel = new AdminViewModel();
                    break;
                case "Chief":
                    viewModel.SelectedViewModel = new ChiefViewModel();
                    break;
                case "Cashier":
                    viewModel.SelectedViewModel = new CashierViewModel();
                    break;
                default:
                    break;
            }
        }
    }
}
