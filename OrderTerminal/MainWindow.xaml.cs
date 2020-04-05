using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using Library.WebApiFunctionality;

namespace OrderTerminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new OrderViewModel();
            DataContext = vm;
            Closing += DataWindow_Closing;
        }

        void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            Trace.WriteLine("hejdå");
            WebApiServer.StopServer();
        }
    }
}
