using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    /// <summary>
    /// Changing of the current page from <see cref="IndexViewModel"/>
    /// </summary>
    public class ChangeViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private IndexViewModel _viewModel;
        public ChangeViewCommand(IndexViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //_viewModel.ChangeView();
        }
    }
}
