using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogainWPF_Project.Commands;
using LogainWPF_Project.Models;

namespace LogainWPF_Project.ViewModels
{ 
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private Employee employee;

        public Employee Employee 
        {
            get
            {
                return employee;

            }
            set
            {
                employee = value;
                NotifyPropertyChanged("Employee");

            }

            
        }
        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee>Employees
        {
            get
            {
                return employees;
            }
            set
            {
                employees = value;
                NotifyPropertyChanged("employees");
            }
        }
        private ICommand loginCommand;
        //public ICommand LoginCommand
        //{
        //    get
        //    {
        //        if (loginCommand == null)
        //        {
        //            loginCommand = new RelayCommand(LogainExecute, CanLogainExecute, false);

        //        }
        //        return LoginCommand;
        //    }

        //}

        private bool CanLogainExecute(object parameter)
        {
            if(string.IsNullOrEmpty(Employee.Email)|| string.IsNullOrEmpty(Employee.Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void LogainExecute(object parameter)
        {
            employees.Add(Employee);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
