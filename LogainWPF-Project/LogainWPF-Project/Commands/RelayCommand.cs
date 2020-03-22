using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Catel.MVVM;
using CommandManager = System.Windows.Input.CommandManager;

namespace LogainWPF_Project.Commands
{
    public class RelayCommand : ICommand
    {
        Action<Object> executeAction;
        Func<Object, bool> canExecute;
        bool canExcuteCache;
        public RelayCommand(Action<Object>executeAction,Func<Object,bool>canExecute,bool canExcuteCache)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
           this. canExcuteCache = canExcuteCache;
        }
        

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            else
            {
                return canExecute(parameter);
            }
            
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
