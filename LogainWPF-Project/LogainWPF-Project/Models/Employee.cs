using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogainWPF_Project.Models
{
   public  class Employee:INotifyPropertyChanged
   {
        private string _Email;
        private string _Password;
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                Email = value;
                onPropertyChanged(Email);
            }
        }
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                Password = value;
                onPropertyChanged(Password);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void onPropertyChanged(string property)
        {
            PropertyChangedEventHandler ph = PropertyChanged;
            if(ph != null)
            {
                ph(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
