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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace LogainWPF_Project
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    { 
        public LoginScreen()
        {
            InitializeComponent();
        } 

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
           SqlConnection conn = new SqlConnection("Data Source=SQL6009.site4now.net;Initial Catalog=DB_A53DDD_JAKOB;User Id=DB_A53DDD_JAKOB_admin;Password=hunter12;");
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "select COUNT(1) FROM Employees where Email=@Email and Password=@Password";
                    SqlCommand sqlcmd = new SqlCommand(query, conn);
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    sqlcmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MainWindow dashbord = new MainWindow();
                        dashbord.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("username or password is incorrect");
                    }
                   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
