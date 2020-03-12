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

        public ICommand PizzaButton { get; set; }
        public ICommand PastaButton { get; set; }
        public ICommand SalladButton { get; set; }
        public ICommand DrickaButton { get; set; }
        public Common CommonData { get; private set; }

        public TopViewModel()
        {
            CommonData = Common.GetInstance();
            PizzaButton = new RelayCommand(o => MainButtonClick("Pizza"));
            PastaButton = new RelayCommand(o => MainButtonClick("Pasta"));
            SalladButton = new RelayCommand(o => MainButtonClick("Sallad"));
            DrickaButton = new RelayCommand(o => MainButtonClick("Drynk"));
            LoadStudents();
        }

        private void MainButtonClick(object sender)
        {
            CommonData.LoadArticles(sender.ToString());
            Trace.WriteLine(sender.ToString());
        }

        public void LoadStudents()
        {
           
        }
    }
}
