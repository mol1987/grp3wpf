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

        public static double WindowWidth { get; set; } = 800;
        public static double WindowHeight { get; set; } = 600;

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
        public string SwapViewButton { get; set; } = "Change View(F1)";

        /// <summary>
        /// Text for button
        /// </summary>
        public string QuitProgramButtonText { get; set; } = "Quit program(Esc)";

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

            // Set window to fit users screen
            WindowWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            WindowHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            // Loads remote data
            LoadArticles();
        }
        #endregion

        // Private holder
        public ICommand _quitApplication;
        /// <summary>
        /// Quit the application, button command
        /// </summary>
        public ICommand QuitApplication { get { return _quitApplication ?? new RelayCommand(param => this.QuitApplicationAction(param), null); } }

        // Private holder
        private ICommand _swapView;
        /// <summary>
        /// Command for swapping view
        /// </summary>
        public ICommand SwapView { get { return _swapView ?? new RelayCommand(param => this.ChangeViewAction(), null); } }

        // Private Holder
        private ICommand _onKeyDown;
        /// <summary>
        /// Command for catching onkey action
        /// <see cref="Button"/> key={keyname} modifier={control/shift etc}
        /// </summary>
        public ICommand OnKeyDown { get { return _onKeyDown ?? new RelayCommand(param => this.OnKeyDownAction(param), null); } }

        // Private Holder
        private ICommand _toggleVisibility;
        /// <summary>
        /// Command for element "Visiblity" toggling between "Hidden" and "Visible"
        /// </summary>
        public ICommand ToggleVisibility { get { return _toggleVisibility ?? new RelayCommand(param => this.ToggleItemVisibility(), null); } }

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
        private void OnKeyDownAction(object args)
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

        /// <summary>
        /// Quits the application
        /// </summary>
        private void QuitApplicationAction(object args)
        {
            App.Current.MainWindow.Close();
        }
    }
}
