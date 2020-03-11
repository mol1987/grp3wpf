using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace MsSqlRepo
{
    public static class General
    {
        public static ArticlesRepository articlesRepo = new ArticlesRepository("Articles");
        public static OrdersRepository ordersRepo = new OrdersRepository("Orders");
        public static IngredientsRepository ingredientsRepo = new IngredientsRepository("Ingredients");
        public static EmployeesRepository employeesRepo = new EmployeesRepository("Employees");
        public static async Task<List<Articles>> getArticles()
        {
            return (await articlesRepo.GetAllAsync()).ToList();
        }
        public static async Task<Articles> getArticle(this Articles a, int id)
        {
            return (await articlesRepo.GetAsync(id));
        }
        public static async Task insertOrder(this Orders o)
        {
            await ordersRepo.InsertAsync(o);
        }
        public static async Task<List<Ingredients>> getIngredients()
        {
            return (await ingredientsRepo.GetAllAsync()).ToList();
        }

    }
}
