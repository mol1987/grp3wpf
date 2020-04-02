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
    public class IngredientModel : Ingredient, INotifyPropertyChanged
    {
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
    }
}
