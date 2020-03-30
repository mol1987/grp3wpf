using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    /// <summary>
    /// Display terminal for Chiefs and Employees
    /// </summary>
    public class ChiefViewModel : BaseViewModel
    {
        public string Title { get; set; } = "Chief View";

        public ICommand SetOrderDone { get; set; }

        #region Constructor
        public ChiefViewModel()
        {

        } 
        #endregion
    }
}
