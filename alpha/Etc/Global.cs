using Library.TypeLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace alpha
{
    /// <summary>
    /// Static & Global single instance class accessible from anywhere.
    /// Probable usage;
    ///     - Access point for data to different ViewModels
    ///     - Common variables
    /// </summary>
    public static class Global
    {
        public static List<Article> Articles { get; set; } = new List<Article>();
    }
}
