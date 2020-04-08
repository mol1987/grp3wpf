using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Library.TypeLib;
using PropertyChanged;

namespace alpha
{
    [AddINotifyPropertyChangedInterface]
    public class IngredientModel : INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }

        public bool IsChecked { get; set; } = false;
        public ICommand SetToChecked { get { return new RelayCommand(param => this.SetToCheckedAction(param), null); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void SetToCheckedAction(object args) {

            Trace.WriteLine("hello");
            this.IsChecked = this.IsChecked ? false : true; 
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
            
        override public string ToString()
        {
            return Name;
        }
    }
}
