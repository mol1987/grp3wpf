using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.TypeLib;

namespace Library.Repository
{
    public static class General
    {
        public static ArticlesRepository articlesRepo = new ArticlesRepository("Articles");
        public static OrdersRepository ordersRepo = new OrdersRepository("Orders");
        public static IngredientsRepository ingredientsRepo = new IngredientsRepository("Ingredients");
        public static EmployeesRepository employeesRepo = new EmployeesRepository("Employees");
        public static async Task<List<Article>> getArticles()
        {
            return (await articlesRepo.GetAllAsync()).ToList();
        }
        public static async Task<Article> getArticle(this Article a, int id)
        {
            return (await articlesRepo.GetAsync(id));
        }
        public static async Task insertOrder(this Order o)
        {
            await ordersRepo.InsertAsync(o);
        }
        public static async Task<List<Ingredient>> getIngredients()
        {
            return (await ingredientsRepo.GetAllAsync()).ToList();
        }

    }
}
