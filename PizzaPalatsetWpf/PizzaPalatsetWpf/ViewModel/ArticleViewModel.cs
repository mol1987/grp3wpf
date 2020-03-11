using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaPalatsetWpf.Model;
using TypeLib;
using MsSqlRepo;
using System.Diagnostics;


namespace PizzaPalatsetWpf.ViewModel
{
    class ArticleViewModel
    {
        private Pizza _pizzas = new Pizza();
        public Common CommonData { get; private set; }
        public ArticleViewModel()
        {
            CommonData = Common.GetInstance();
            LoadPizzas();
        }
       
        public async void LoadPizzas()
        {
            CommonData.Articles = new NotifyTaskCompletion<IEnumerable<Articles>>(General.articlesRepo.GetAllAsync());
            //CommonData.LoadArticles(Globals.ArticleTypes.Pizza);

        }
    }
}
