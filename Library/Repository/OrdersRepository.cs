using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.TypeLib;

namespace Library.Repository
{
    public class OrdersRepository : GenericRepository<Order>
    {
        string tableName;
        public OrdersRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }
        /// <summary>
        /// Inserts an order with all the articles and custom ingredients contained in order.
        /// gets the orderid and stores it in order
        /// </summary>
        /// <param name="order">Created order object</param>
        /// <returns></returns>
        public async Task InsertAsync(Order order)
        {
            var orderTemp = new Order { CustomerID = order.CustomerID, Price = order.Price };
            string insertQuery = base.GenerateInsertQuery(orderTemp);
            insertQuery += " SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = CreateConnection())
            {
                order.ID = (await connection.QueryAsync<int>(insertQuery, orderTemp)).Single();
                foreach (var article in order.Articles)
                {
                    using (var transaction = connection.BeginTransaction())
                    {
                        string sql = @"INSERT INTO ArticleOrders (ArticlesID, OrdersID) VALUES (@ArticlesID, @OrdersID) SELECT CAST(SCOPE_IDENTITY() as int)";
                        var id = (await connection.QueryAsync<int>(sql, new { OrdersID = (int)order.ID, ArticlesID = (int)article.ID }, transaction: transaction)).Single();
                        article.ArticleOrder = (await connection.QuerySingleOrDefaultAsync<ArticleOrder>("Select * from ArticleOrders where ID = @ArticleOrdersID", new { ArticleOrdersID = id }, transaction: transaction));

                        foreach (var ingredient in article.Ingredients)
                        {
                            var sqlQuery = $"INSERT INTO ArticleOrdersIngredients (IngredientsID, ArticleOrdersID) VALUES (@IngredientsID, @ArticleOrdersID)";
                            await connection.ExecuteAsync(sqlQuery, new ArticleOrdersIngredient { IngredientsID = (int)ingredient.ID, ArticleOrdersID = article.ArticleOrder.ID }, transaction: transaction);
                        }
                        transaction.Commit();
                    }
                }
            }
        }

        public async Task GetAllAsync(Order order)
        {
            using (var connection = base.CreateConnection())
            {
                IEnumerable<ArticleOrder> articleOrders = await connection.QueryAsync<ArticleOrder>($"SELECT * FROM ArticleOrders WHERE OrdersID=@Id", new { Id = order.ID });
                List<Article> articles = new List<Article>();

                string sql = $"select Ingredients.* FROM ARTICLES INNER JOIN ArticleOrders ON Articles.ID = ArticleOrders.ArticlesID inner join ArticleOrdersIngredients on ArticleOrdersIngredients.ArticleOrdersID = ArticleOrders.ID inner join Ingredients on Ingredients.ID = ArticleOrdersIngredients.IngredientsID WHERE ArticleOrders.OrdersID = @OrdersID and ArticleOrders.ID = @ArticleOrdersID";

                Article article;
                foreach (ArticleOrder articleOrdersItem in articleOrders)
                {
                    article = await connection.QuerySingleOrDefaultAsync<Article>($"SELECT * FROM {tableName} WHERE Id=@Id", new { Id = articleOrdersItem.ArticlesID });
                    article.Ingredients = new List<Ingredient>();
                    article.Ingredients = (await connection.QueryAsync<Ingredient>(sql, new { OrdersID = order.ID, ArticleOrdersID = articleOrdersItem.ID })).ToList();
                    articles.Add(article);
                }
                order.Articles = articles;
            }
        }
        public async Task<List<DisplayObject>> GetAllAsync(int n)
        {
            using (var connection = base.CreateConnection())
            {
                string testquery =
                    String.Join(
                    " ",
                    "(SELECT ArticleOrders.ID, Articles.Name, OrdersID, Orderstatus, Ingredients.Name as 'Ingredient' FROM ArticleOrders",
                    "INNER JOIN Orders ON ArticleOrders.OrdersID = Orders.ID",
                    "INNER JOIN Articles ON Articles.ID = ArticleOrders.ArticlesID",
                    "INNER JOIN ArticleOrdersIngredients ON ArticleOrders.ID = ArticleOrdersIngredients.ArticleOrdersID",
                    "INNER JOIN ArticleIngredients ON ArticleOrdersIngredients.IngredientsID = ArticleIngredients.IngredientID",
                    "JOIN Ingredients ON ArticleIngredients.IngredientID = Ingredients.ID",
                    "WHERE Orders.Orderstatus = '1') ORDER BY TimeCreated");
                //.Replace(System.Environment.NewLine, "").Trim(new char[] { });
                string longquery = @"
                (SELECT DISTINCT
                ArticleOrders.ID, Articles.Name, OrdersID, Orderstatus, Ingredients.Name as 'Ingredient'
                FROM ArticleOrders
                INNER JOIN Orders ON ArticleOrders.OrdersID = Orders.ID
                INNER JOIN Articles ON Articles.ID = ArticleOrders.ArticlesID
                INNER JOIN ArticleOrdersIngredients ON ArticleOrders.ID = ArticleOrdersIngredients.ArticleOrdersID
                INNER JOIN ArticleIngredients ON ArticleOrdersIngredients.IngredientsID = ArticleIngredients.IngredientID
                JOIN Ingredients ON ArticleIngredients.IngredientID = Ingredients.ID
                WHERE Orders.Orderstatus = '1'
                ) ORDER BY ArticleOrders.ID";
                var articleOrders = (await connection.QueryAsync<DisplayObject>(longquery, new { Orderstatus = n })).ToList();
                return articleOrders;

                //IEnumerable<ArticleOrders> articleOrders = await connection.QueryAsync<ArticleOrders>($"SELECT * FROM ArticleOrders WHERE OrdersID=@Id", new { Id = order.ID });
                //List<Articles> articles = new List<Articles>();

                //string sql = $"select Ingredients.* FROM ARTICLES INNER JOIN ArticleOrders ON Articles.ID = ArticleOrders.ArticlesID inner join ArticleOrdersIngredients on ArticleOrdersIngredients.ArticleOrdersID = ArticleOrders.ID inner join Ingredients on Ingredients.ID = ArticleOrdersIngredients.IngredientsID WHERE ArticleOrders.OrdersID = @OrdersID and ArticleOrders.ID = @ArticleOrdersID";

                //Articles article;
                //foreach (ArticleOrders articleOrdersItem in articleOrders)
                //{
                //    article = await connection.QuerySingleOrDefaultAsync<Articles>($"SELECT * FROM {tableName} WHERE Id=@Id LIMIT BY $limit", new { Id = articleOrdersItem.ArticlesID, limit = limit });
                //    article.Ingredients = new List<Ingredients>();
                //    article.Ingredients = (await connection.QueryAsync<Ingredients>(sql, new { OrdersID = order.ID, ArticleOrdersID = articleOrdersItem.ID })).ToList();
                //    articles.Add(article);
                //}
                //order.Articles = articles;
            }
        }
    }
}
