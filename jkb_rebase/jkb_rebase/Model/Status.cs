﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace jkb_rebase.Model
{
    public class Status : INotifyPropertyChanged
    {
        public string StatusMessage { get; set; }
        #region Common

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}