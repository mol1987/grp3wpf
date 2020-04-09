using Library.TypeLib;
using System.Collections.Generic;
using System.Windows;
using Library.Repository;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using System.Diagnostics;

namespace alpha
{
    /// <summary>
    /// Static & Global single instance class accessible from anywhere.
    /// Probable usage;
    ///     - Access point for data to different ViewModels
    ///     - Common variables
    /// </summary>
    public static class Global
    {
        private static bool _stopThread = false;
        private static int _updateDataTime = 1000 * 30;
        static readonly object _object = new object();
        /// <summary>
        /// List of <see cref="Article"/>
        /// Global usage as a quick fix for access in terminals
        /// </summary>
        public static List<Article> Articles { get; set; } = new List<Article>();
        public static ObservableCollection<Article> ObsArticles { get; set; } = new ObservableCollection<Article>();

        public static List<OrderModel> ObsOrdersPlaced { get; set; } = new List<OrderModel>();
        public static ObservableCollection<OrderModel> ObsOrdersDone { get; set; } = new ObservableCollection<OrderModel>();
        
        public delegate void DataUpdateDelegate(object model, string typeOfUpdate);
        static public event DataUpdateDelegate dataUpdateEvent;

        /// <summary>
        /// Refresh articles
        /// </summary>
        public static async System.Threading.Tasks.Task ReloadArticlesAsync()
        {
            Global.Articles = (await Global.ArticleRepo.GetAllAsync()).ToList();
        }

        static Thread Thread = new Thread(new ThreadStart(UpdateDataThread));

        static async void UpdateDataThread()
        {
            while (_stopThread == false)
            {
                // update articles
                var tempArticle = (await Global.ArticleRepo.GetAllAsync()).ToList();
                tempArticle.ForEach(x => {
                    if (ObsArticles.Any(y => y.ID == x.ID) == false)
                        ObsArticles.Add(x);
                    });
                dataUpdateEvent?.Invoke(ObsArticles, "ObsArticles");

                lock (_object)
                {
                    // update orders
                    List<Order> placedOrders = new List<Order>();
                    List<Order> doneOrders = new List<Order>();
                    var tempOrder = Global.OrderRepo.GetAllAsync().Result.ToList();
                    tempOrder.ForEach(x =>
                    {
                        if (x.Orderstatus == 1)
                        {
                            Trace.WriteLine("i");
                            Global.OrderRepo.GetAllAsync(x).Wait();

                            //List<ArticleModel> tempArticles = new List<ArticleModel>();
                            //x.Articles.ForEach(y => tempArticles.Add(new ArticleModel { ID = y.ID, BasePrice = y.BasePrice, Name = y.Name, IsActive = y.IsActive, Ingredients = y.Ingredients }));
                            placedOrders.Add(x);
                            Trace.WriteLine("add");
                            //if (ObsOrdersPlaced.Any(y => y.ID == x.ID) == false)
                            //    ObsOrdersPlaced.Add(new OrderModel { ID = x.ID, CustomerID = x.CustomerID, Orderstatus = x.Orderstatus, Price = x.Price, TimeCreated = x.TimeCreated, Articles = tempArticles });
                        } else if (x.Orderstatus == 2)
                        {
                            doneOrders.Add(x);
                        }
                    });
                    Trace.WriteLine("invoke: " + tempOrder.Count);
                    Trace.WriteLine("done: " + doneOrders.Count);

                    dataUpdateEvent?.Invoke(new List<Order>(placedOrders), "ObsOrdersPlaced");
                    dataUpdateEvent?.Invoke(new List<Order>(doneOrders), "ObsOrdersDone");
                    Thread.Sleep(_updateDataTime);
                }
            }
        }
        public static void StartUpdateDataThread()
        {
            Thread.Start();
        }
        public static void StopUpdateDataThread()
        {
            _stopThread = true;
        }


        /// <summary>
        /// wpf-window. Probably not needed like this at the moment
        /// </summary>
        public static Window ActualWindow;

        /// <summary>
        /// Check to avoid having multiple servers for WebApi
        /// Apparently enum is always static(?) as itäs a type and not a variable
        /// nevermind, just using bool instead
        /// </summary>
        public static bool IsServerStarted = false;

        /// <summary>
        /// Roles for authentication
        /// </summary>
        public static List<string> Roles = new List<string>() { "All" };

        /// <summary>
        /// ? check role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool HasAuthenticationRoles (string role) => Roles.Contains(role);

        /// <summary>
        /// Disable abilty to switch between pages and
        /// pretend like its a release build
        /// </summary>
        public static bool IsTerminalLocked { get; set; } = false; 

        #region Repositories & Connections
        public static ArticlesRepository ArticleRepo = new ArticlesRepository("Articles");
        public static OrdersRepository OrderRepo = new OrdersRepository("Orders");
        public static IngredientsRepository IngredientRepo = new IngredientsRepository("Ingredients");
        public static EmployeesRepository EmployeeRepo = new EmployeesRepository("Employees"); 
        #endregion
    }
}
