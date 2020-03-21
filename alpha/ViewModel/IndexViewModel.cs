using System;
using System.Windows;
using System.Windows.Input;

namespace alpha
{
    public class IndexViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The current view displayed
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Splash;

        public string SwapViewButton { get; set; } = "Change View";

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
        /// 
        /// </summary>
        /// <param name="window"></param>
        public IndexViewModel(Window window)
        {
            _window = window;
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
        private void ChangeViewAction()
        {
            //
            //
            foreach (ApplicationPage page in Enum.GetValues(typeof(ApplicationPage)))
            {
                if (!page.Equals(CurrentPage))
                {
                    CurrentPage = page;
                    break;
                }
            }
        }
        private void OnKeyDownAction()
        {
            var x = 0;
        }
    }
}
