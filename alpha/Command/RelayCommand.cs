using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The event that's fired when the <see cref="CanExecute(object)"/>  value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //private Action action;
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        #region Construtor
        //public RelayCommand(Action _action)
        //{
        //    action = _action;
        //}
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
    }
}
