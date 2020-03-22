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
using System.Data.SqlClient;
using System.Data;
using LogainWPF_Project.ViewModels;
using LogainWPF_Project.Models;

namespace LogainWPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>jjnjkjkkklklkllkklkl
    public partial class MainWindow : Window
    {
        ArticleViewModel articles;
        public MainWindow()
        {
            InitializeComponent();
            articles = new ArticleViewModel();
            this.DataContext = articles;
        }

        //private void btnSubmit_Click(object sender, RoutedEventArgs e)
        //{
        //    SqlConnection conn = new SqlConnection("Data Source=SQL6009.site4now.net;Initial Catalog=DB_A53DDD_JAKOB;User Id=DB_A53DDD_JAKOB_admin;Password=hunter12;");
        //}

        private void btnArtiklar_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=SQL6009.site4now.net;Initial Catalog=DB_A53DDD_JAKOB;User Id=DB_A53DDD_JAKOB_admin;Password=hunter12;");
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from Articles", conn);
            DataTable ArtiklarList = new DataTable();
            adapter.Fill(ArtiklarList);


            //try
            //{
            //    conn.Open();
            //    string query = "Select* from Articles";
            //    SqlCommand comma = new SqlCommand(query, conn);
            //    SqlDataReader dr = comma.ExecuteReader();
            //    while (dr.Read())
            //    {
            //        string name = dr.GetString(1);
            //        ComboBox1.Items.Add(name);

            //    }
            //    conn.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);

            //}
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "SELECT  COUNT(*) FROM Articles where ID=@ID and Name=@Name and BasePrice=@BasePrice and Type=@Type";
                    SqlCommand sqlcmd = new SqlCommand(query, conn);
                    sqlcmd.CommandType = CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@ID", ArtiklarList.TableName);

                    sqlcmd.Parameters.AddWithValue("@BasePrice", ArtiklarList.TableName);
                    sqlcmd.Parameters.AddWithValue("@Type", ArtiklarList.TableName);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MainWindow dashbord = new MainWindow();
                        dashbord.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Items are not found");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
          
            var combo = (ComboBox)(sender);
            var value = (ComboBoxItem)combo.SelectedValue;
            this.Nametxt.Text = (string)value.Content;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Article article = articles.SelectedArticle;
            MessageBox.Show(article.Name);
        }

        //private void LoginScreen_Loaded(object sender, RoutedEventArgs e)
        //{
        //    ViewModels.EmployeeViewModel employeeViewModelObject = new ViewModels.EmployeeViewModel();
        //    employeeViewModelObject.LoadEmployeesLoginUppgifter();
        //    LoginScreen.DataContext = employeeViewModelObject;





        //}
    }
}
