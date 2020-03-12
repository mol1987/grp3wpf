using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PizzaPalatsetWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Helper.Environment.LoadEnvFile();
            InitializeComponent();
        }

        private void TopViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            PizzaPalatsetWpf.ViewModel.TopViewModel topViewModelObject =
               new PizzaPalatsetWpf.ViewModel.TopViewModel();
            topViewModelObject.LoadStudents();

            TopViewControl.DataContext = topViewModelObject;
        }
        //private void ArticleViewControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    PizzaPalatsetWpf.ViewModel.ArticleViewModel articleViewModelObject =
        //       new PizzaPalatsetWpf.ViewModel.ArticleViewModel();
        //    articleViewModelObject.LoadPizzas();

        //    ArticleViewControl.DataContext = articleViewModelObject;
        //}
        private void SideViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            PizzaPalatsetWpf.ViewModel.SideViewModel sideViewModelObject =
               new PizzaPalatsetWpf.ViewModel.SideViewModel();
            //articleViewModelObject.LoadPizzas();

            SideViewControl.DataContext = sideViewModelObject;
        }
        //private void IngredientsViewControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    PizzaPalatsetWpf.ViewModel.IngredientsViewModel ingredientsViewModelObject =
        //       new PizzaPalatsetWpf.ViewModel.IngredientsViewModel();
        //    //articleViewModelObject.LoadPizzas();

        //    IngredientsViewControl.DataContext = ingredientsViewModelObject;
        //}
        //private void MainWindowViewControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    PizzaPalatsetWpf.ViewModel.MainWindowViewModel mainWindowViewModelObject =
        //       new PizzaPalatsetWpf.ViewModel.MainWindowViewModel();
        //    //articleViewModelObject.LoadPizzas();

        //    MainWindowViewControl.DataContext = mainWindowViewModelObject;
        //}
    }
}
