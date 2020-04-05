using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.TypeLib;
using System.Linq;

namespace Library.Repository
{
    public class IngredientsRepository : GenericRepository<Ingredient>
    {
        string tableName;
        public IngredientsRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }
        public async Task InsertAsync(Ingredient ingredient)
        {
            using (var connection = CreateConnection())
            {
                string sql = @"INSERT INTO Ingredients (Name, Price) VALUES (@Name, @Price) SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = (await connection.QueryAsync<int>(sql, new { Name = ingredient.Name, Price = ingredient.Price})).Single();
                ingredient.ID = id;
            }
        }
    }
}
