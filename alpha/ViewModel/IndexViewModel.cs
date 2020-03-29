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
<<<<<<< HEAD
=======
        public static double WindowWidth { get; set; } = 800;
        public static double WindowHeight { get; set; } = 600;

>>>>>>> 73240c4b5a535d9a7723f86e2b8bc2f6eff81b4d
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
        /// Testning, todo; remove
        /// </summary>
        public string TestItemVisibility { get; set; } = "Visible";
         
        /// <summary>
        /// The actual xaml-window
        /// Probably use for messageBoxes etc
        /// </summary>
        private Window _window;

        #region Constructor
        /// <summary>
        /// Main Constructor for the Indexer
        /// </summary>
        /// <param name="window"><see cref="Window"/> The actual window itself, passed as an argument</param>
        public IndexViewModel(Window window)
        {
            _window = window;
<<<<<<< HEAD
=======

            // Set window to fit users screen
            WindowWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            WindowHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            
>>>>>>> 73240c4b5a535d9a7723f86e2b8bc2f6eff81b4d
            // Loads remote data
            LoadArticles();
        }
        #endregion

        // Private holder
<<<<<<< HEAD
=======
        public ICommand _quitApplication;

        public ICommand QuitApplication { get { return _quitApplication  ?? new RelayCommand(param => this.QuitApplicationAction(), null); } }

        // Private holder
>>>>>>> 73240c4b5a535d9a7723f86e2b8bc2f6eff81b4d
        private ICommand _swapView;
        /// <summary>
        /// Command for swapping view
        /// </summary>
        public ICommand SwapView
        {
            get
            {
                if (_swapView == null)
                    _swapView = new RelayCommand(param => this.ChangeViewAction(), null);
                return _swapView;
            }
        }

        // Private Holder
        private ICommand _onKeyDown;
        /// <summary>
        /// (Currently not working)
        /// Command for catching onkey onkey action
        /// </summary>
        public ICommand OnKeyDown
        {
            get
            {
                if (_onKeyDown == null)
                    _onKeyDown = new RelayCommand(param => this.OnKeyDownAction(), null);
                return _onKeyDown;
            }
        }

        // Private Holder
        private ICommand _toggleVisibility;
        /// <summary>
        /// Command for element "Visiblity" toggling between "Hidden" and "Visible"
        /// </summary>
        public ICommand ToggleVisibility
        {
            get
            {
                if (_toggleVisibility == null)
                    _toggleVisibility = new RelayCommand(param => this.ToggleItemVisibility(), null);
                return _toggleVisibility;
            }
        }

        /// <summary>
        /// Swaps the current view/page in main_frame 
        /// </summary>
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

        /// <summary>
        /// Just testing for now, todo; add onkey triggersr
        /// </summary>
        private void OnKeyDownAction()
        {
            var x = 0;
        }

        /// <summary>
        /// Just testing, todo; remove
        /// </summary>
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
            //WebApi
            //Global.Articles = await (new Library.ArticleProcessor().LoadArticle());

            //Sql Connection
            Global.Articles = (await Global.ArticleRepo.GetAllAsync()).ToList();
        }
<<<<<<< HEAD
=======

        /// <summary>
        /// Quits the application
        /// </summary>
        private void QuitApplicationAction()
        {
            App.Current.MainWindow.Close();
        }
>>>>>>> 73240c4b5a535d9a7723f86e2b8bc2f6eff81b4d
    }
}
