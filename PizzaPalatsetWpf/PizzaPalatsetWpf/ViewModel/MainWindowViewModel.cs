using PizzaPalatsetWpf.Model;
using PizzaPalatsetWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PizzaPalatsetWpf.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            CommonData = Common.GetInstance();
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = articleListViewModel;
            CommonData.PropertyChanged += OnSessionContextPropertyChanged;
        }

        public Common CommonData { get; private set; }

        private IngredientsViewModel ingredientsListViewModel = new IngredientsViewModel();
        private ArticleViewModel articleListViewModel = new ArticleViewModel();

        private BindableBase _CurrentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        private BindableBase _PreviousViewModel;

        public MyICommand<string> NavCommand { get; private set; }

        private void OnNav(string destination)
        {    
            switch (destination)
            {
                case "articles":
                    _PreviousViewModel = CurrentViewModel;
                    CurrentViewModel = articleListViewModel;
                    break;
                case "ingredients":
                    _PreviousViewModel = CurrentViewModel;
                    CurrentViewModel = ingredientsListViewModel;
                    break;
                case "back":
                    CurrentViewModel = _PreviousViewModel;
                    break;
                default:
                    _PreviousViewModel = CurrentViewModel;
                    CurrentViewModel = articleListViewModel;
                    break;
            }
            
        }
        private void OnSessionContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                OnNav("ingredients");
            }
            if (e.PropertyName == "BackButton")
            {
                OnNav("back");
            }
        }
    }
}