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
using Project_WPF.Views;

namespace Project_WPF
{
    /// <summary>
    /// Interaction logic for ArticleTypes.xaml
    /// </summary>
    public partial class ArticleTypes : Page
    {
        public ArticleTypes()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mContentFrame.Navigate(new PizzaList());

        }
    }
}
