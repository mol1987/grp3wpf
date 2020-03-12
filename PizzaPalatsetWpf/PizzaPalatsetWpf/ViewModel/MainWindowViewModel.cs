using PizzaPalatsetWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PizzaPalatsetWpf
{
    class MainWindowViewModel : BindableBase
    {

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = articleListViewModel;
        }

        private IngredientsViewModel ingredientsListViewModel = new IngredientsViewModel();
        private ArticleViewModel articleListViewModel = new ArticleViewModel();

        private BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public MyICommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {

            switch (destination)
            {
     
                case "articles":
                default:
                    CurrentViewModel = articleListViewModel;
                    break;
            }
        }
    }
}