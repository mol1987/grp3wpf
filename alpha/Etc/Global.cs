using Library.TypeLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Library.Repository;
using System.Threading.Tasks;
using System.Linq;

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
        /// <summary>
        /// List of <see cref="Article"/>
        /// Global usage as a quick fix for access in terminals
        /// </summary>
        public static List<Article> Articles { get; set; } = new List<Article>();

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

        #region Repositories & Connections
        public static ArticlesRepository ArticleRepo = new ArticlesRepository("Articles");
        public static OrdersRepository OrderRepo = new OrdersRepository("Orders");
        public static IngredientsRepository IngredientRepo = new IngredientsRepository("Ingredients");
        public static EmployeesRepository EmployeeRepo = new EmployeesRepository("Employees"); 
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Article>> getArticles()
        {
            return (await ArticleRepo.GetAllAsync()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Article> getArticle(this Article a, int id)
        {
            return (await ArticleRepo.GetAsync(id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static async Task insertOrder(this Order o)
        {
            await OrderRepo.InsertAsync(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static async Task<List<Ingredient>> getIngredients()
        {
            return (await IngredientRepo.GetAllAsync()).ToList();
        }
    }
}
