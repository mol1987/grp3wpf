using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    /// <summary>
    /// CustomerView top panel.
    /// Filtering article types
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class FilterButton : INotifyPropertyChanged
    {
        public string Type { get; set; } = "____";
        public bool IsActive { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }
    }

}

