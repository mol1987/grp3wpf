using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    /// <summary>
    /// Placeholder display. Index defaults to this.
    /// todo; animation shift and some kind of loading trigger/progress bar
    /// </summary>
    public class SplashViewModel : BaseViewModel
    {
        public string Intro { get; set; } = "PizzeriaPalatset";

        public int LoadedArticles { get { return Global.Articles.Count; } }
    }
}
