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

            // Load environment with error check
            if (!Library.Helper.Environment.LoadEnvFile())
            {
                MessageBox.Show("Missing environment file, shutting down", "Error");
                this.Close();
            }

            // Auth check, if nothing is set, default to poweruser
            if (!Library.Helper.Globals.Exists("role"))
                Global.Roles.AddRange(new string[] { "admin", "customer" });
            else
                Global.Roles.Add(Library.Helper.Globals.Get("role"));

            // Load up MVVM
            this.DataContext = new IndexViewModel(this);
        }
    }
}
