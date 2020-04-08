using Library.TypeLib;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace alpha
{
    [AddINotifyPropertyChangedInterface]
    public class ArticleModel : INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? BasePrice { get; set; }
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Ingredient> SelectedIngredients { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RemoveIngredient { get { return new RelayCommand(param => this.RemoveIngredientAction(param), null); } }
        public ICommand AddIngredient { get { return new RelayCommand(param => this.AddIngredientAction(param), null); } }
        public ICommand SelectIngredients { get { return new RelayCommand(param => this.SelectIngredientsAction(param), null); } }

       

        public Ingredient SelectedIngredient { get; set; }
        public Ingredient SelectedRemoveIngredient { get; set; }
        public IngredientModel SelectedAddIngredient { get; set; }

        private void SelectIngredientsAction(object param)
        {
            SelectedIngredients = ((ArticleModel)param).Ingredients;
        }
        private async void RemoveIngredientAction(object args)
        {
            if (SelectedRemoveIngredient == null) return;

            Ingredients.Remove(Ingredients.Find(X => X.Name == SelectedRemoveIngredient.Name));

            await Global.ArticleRepo.UpdateAsync(new Article() { ID = this.ID, BasePrice = this.BasePrice, Name = this.Name, IsActive = this.IsActive, Type = this.Type, Ingredients = this.Ingredients });

            Trace.WriteLine(SelectedRemoveIngredient.Name);

        }
        private async void AddIngredientAction(object args)
        {
            if (SelectedAddIngredient == null) return;
            if (Ingredients.Any(x => x.Name == SelectedAddIngredient.Name)) return;
            Ingredient ingredientToAdd = new Ingredient { ID = SelectedAddIngredient.ID, Name = SelectedAddIngredient.Name, Price = SelectedAddIngredient.Price };
            SelectedRemoveIngredient = ingredientToAdd;
            Ingredients.Add(ingredientToAdd);
            await Global.ArticleRepo.UpdateAsync(new Article() { ID = this.ID, BasePrice = this.BasePrice, Name = this.Name, IsActive = this.IsActive, Type = this.Type, Ingredients = this.Ingredients });
            Trace.WriteLine(SelectedAddIngredient.Name);
        }

    public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }
        }
    }
}
