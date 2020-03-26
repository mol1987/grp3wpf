using System.Windows;

namespace alpha
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Index : Window
    {
        public Index()
        {
            // Set up the actual XAML
            InitializeComponent();

            // Store window in global to be accesible, todo; this is probably bad practice but meh
            Global.ActualWindow = this;

            // Load up MVVM
            this.DataContext = new IndexViewModel(this);
        }
    }
}
