
using Library.TypeLib;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    [AddINotifyPropertyChangedInterface]
    public class OrderModel : INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public DateTime? TimeCreated { get; set; }
        public int? Orderstatus { get; set; }
        public float? Price { get; set; }
        public int? CustomerID { get; set; }
        public List<ArticleModel>? Articles { get; set; }
        public List<Ingredient> SelectedIngredients { get; set; }

        public ICommand SelectIngredients { get { return new RelayCommand(param => this.SelectIngredientsAction(param), null); } }
        private void SelectIngredientsAction(object param)
        {
            SelectedIngredients = ((ArticleModel)param).Ingredients;
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
