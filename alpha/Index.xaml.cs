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
            // Load up MVVM
            this.DataContext = new IndexViewModel(this);
        }
    }
}
