using System;
using System.Collections.Generic;
using System.Text;

namespace alpha
{
    /// <summary>
    /// List of available Pages inside <see cref="Window.Frame"/>
    /// </summary>
    public enum ApplicationPage
    {
        /// <summary>
        /// Current View Displayed
        /// </summary>
        Splash = 0,
        Customer = 1,
        Chief = 2,
        Order = 3,
        Cashier = 4,
        Admin = 5
    }
}
