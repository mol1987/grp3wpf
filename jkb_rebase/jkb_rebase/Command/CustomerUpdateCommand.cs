using jkb_rebase.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace jkb_rebase.Command
{
    public class CustomerUpdateCommand : ICommand
    {
        private CustomerViewModel _ViewModel;
        public event EventHandler CanExecuteChanged;
        public CustomerUpdateCommand(CustomerViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SaveChanges();
        }
    }
}
