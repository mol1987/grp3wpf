using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace alpha
{
    /// <summary>
    /// View/ViewModel used as a global Indexer for the other Views and ViewModels
    /// </summary>
    public class IndexViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The current view displayed
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Splash;

        /// <summary>
        /// Helper for swapping index, probably överblivet
        /// </summary>
        public int CurrentPageIndex { get; set; } = 0;

        /// <summary>
        /// Text for button that swaps <see cref="CurrentPage"/>, hide this in final version
        /// </summary>
        public string SwapViewButton { get; set; } = "Change View";

        /// <summary>
        /// 
        /// </summary>
        public string TestItemVisibility { get; set; } = "Visible";

        #endregion
         
        #region Private Properties
        /// <summary>
        /// The actual xaml-window
        /// Probably use for messageBoxes etc
        /// </summary>
        private Window _window;
        #endregion

        #region Constructor
        /// <summary>
        /// Main Constructor for the Indexer
        /// </summary>
        /// <param name="window"><see cref="Window"/> The actual window itself, passed as an argument</param>
        public IndexViewModel(Window window)
        {
            _window = window;
            LoadArticles();
        }
        #endregion

        private ICommand _swapView;

        private ICommand _onKeyDown;

        public ICommand SwapView
        {
            get
            {
                if (_swapView == null)
                    _swapView = new RelayCommand(param => this.ChangeViewAction(), null);
                return _swapView;
            }
        }

        public ICommand OnKeyDown
        {
            get
            {
                if (_onKeyDown == null)
                    _onKeyDown = new RelayCommand(param => this.OnKeyDownAction(), null);
                return _onKeyDown;
            }
        }

        private ICommand _toggleVisibility;
        public ICommand ToggleVisibility
        {
            get
            {
                if (_toggleVisibility == null)
                    _toggleVisibility = new RelayCommand(param => this.ToggleItemVisibility(), null);
                return _toggleVisibility;
            }
        }

        private void ChangeViewAction()
        {
            int i = 0;
            // Loop to change view
            foreach (ApplicationPage page in Enum.GetValues(typeof(ApplicationPage)))
            {
                // If CurrentPage is the last Page, (currently set to Cashierview), reset to Index page and quit the loop
                if (CurrentPage.Equals(ApplicationPage.Cashier))
                {
                    CurrentPageIndex = 0;
                    CurrentPage = ApplicationPage.Splash;
                    break;
                }
                // If Page is itself, skip to next
                if (page.Equals(CurrentPage))
                {
                    i++;
                    continue;
                }
                // If page is less than current index, skip
                if (i < CurrentPageIndex)
                {
                    i++;
                    continue;
                }

                // Page switch and quit the loop, since we have what we came for
                CurrentPageIndex = i;
                CurrentPage = page;
                break;
            }
        }

        private void OnKeyDownAction()
        {
            var x = 0;
        }
        private void ToggleItemVisibility()
        {
            // ? if visible hide it, else if hidden; show it
            TestItemVisibility = TestItemVisibility == "Visible" ? "Hidden" : "Visible";
        }
        /// <summary>
        /// Loads from the set up WebApi
        /// </summary>
        private async void LoadArticles()
        {
            Global.Articles = await (new Library.ArticleProcessor().LoadArticle());
        }
    }
}
