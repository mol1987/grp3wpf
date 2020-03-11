using System;
using System.Collections.Generic;
using System.Text;


namespace jkb_rebase.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        public string TestString { get; set; } = "Admin hello";
        public AdminViewModel()
        {
            TestString = Library.LocalDataStorage.WorkingPath;
        }
    }
}
