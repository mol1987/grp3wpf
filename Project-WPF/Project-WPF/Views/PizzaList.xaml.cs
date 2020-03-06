using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_WPF.Views
{
    /// <summary>
    /// Interaction logic for PizzaList.xaml
    /// </summary>
    public partial class PizzaList : Page
    {
        public PizzaList()
        {
            InitializeComponent();
        }

        private void Kyckling_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mContentFrame.Navigate(new ArticleTypes());
            // Här måste finnas ett fucktion som kan lägga alla din välj  under your orderList jkkkkkjj
            
            //this.ListView.Name +=(Button)sender.Content;
        }
    }
}
