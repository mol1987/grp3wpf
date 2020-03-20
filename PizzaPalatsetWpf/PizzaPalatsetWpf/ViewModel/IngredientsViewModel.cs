using PizzaPalatsetWpf.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace PizzaPalatsetWpf.ViewModel
{
    class IngredientsViewModel : BindableBase
    {
        public Common CommonData { get; private set; }
        public ICommand BackButton { get; set; }
        public IngredientsViewModel()
        {
            CommonData = Common.GetInstance();
            BackButton = new RelayCommand(o => IngrButtonClick("BackButton"));
        }
        private void IngrButtonClick(object sender)
        {
            switch (sender.ToString())
            {
                case "BackButton":
                    CommonData.GoBack();

                    break;
                default:
                    break;
            }
        }
    }
}
