using System;
using System.Collections.Generic;
using System.Text;

namespace alpha
{
    public class WindowSettings
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        /// <summary>
        /// Autoconstruct into Maximized Window
        /// </summary>
        public string WindowState { get; set; }
    }
}
