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
    class ArticleViewModel : BindableBase
    {
        Article article = new Article();
 
        public Common CommonData { get; private set; }
        public ArticleViewModel()
        {
            CommonData = Common.GetInstance();
            Load();
        }

        public Articles SelectedItem {
            get { return CommonData.SelectedItem; }
            set
            {
                
                CommonData.SelectedItem = value; 
            }
        }

        public async void Load()
        {
            CommonData.Articles = new NotifyTaskCompletion<IEnumerable<Articles>>(General.articlesRepo.GetAllAsync());
            //CommonData.LoadArticles(Globals.ArticleTypes.Pizza);
 
        }
    }
}
