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
        public static List<Article> Articles { get; set; } = new List<Article>();
        public static Window ActualWindow;

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
