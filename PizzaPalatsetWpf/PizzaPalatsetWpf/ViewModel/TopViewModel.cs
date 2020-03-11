using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PizzaPalatsetWpf.Model;
using System.Windows.Input;
using System.Diagnostics;

namespace PizzaPalatsetWpf.ViewModel
{
    public class TopViewModel 
    {

        public ICommand ButtonCommand { get; set; }
        public Common CommonData { get; private set; }

        public TopViewModel()
        {
            CommonData = Common.GetInstance();
            ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));
            LoadStudents();
        }

        private void MainButtonClick(object sender)
        {
            CommonData.LoadArticles(Globals.ArticleTypes.Pizza);
            Trace.WriteLine(sender.ToString());
        }

        public void LoadStudents()
        {
           
        }
    }
}
