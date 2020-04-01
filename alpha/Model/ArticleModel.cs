﻿using Library.TypeLib;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace alpha.Model
{
    [AddINotifyPropertyChangedInterface]
    public class ArticleModel : Article, INotifyPropertyChanged
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public float? BasePrice { get; set; }
        public string? Type { get; set; }
        public bool? IsActive { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RemoveIngredient { get { return new RelayCommand(param => this.RemoveIngredientAction(param), null); } }
        public ICommand AddIngredient { get { return new RelayCommand(param => this.AddIngredientAction(param), null); } }

        public Ingredient SelectedIngredient { get; set; }
        public Ingredient SelectedRemoveIngredient { get; set; }
        public Ingredient SelectedAddIngredient { get; set; }


        private void RemoveIngredientAction(object args)
        {
            if (SelectedRemoveIngredient == null) return;

            Ingredients.Remove(Ingredients.Find(X => X.Name == SelectedRemoveIngredient.Name));

            Trace.WriteLine(SelectedRemoveIngredient.Name);
        }
        private void AddIngredientAction(object args)
        {
            if (SelectedAddIngredient == null) return;
            if (Ingredients.Any(x => x.Name == SelectedAddIngredient.Name)) return;
            Ingredients.Add(SelectedAddIngredient);
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
