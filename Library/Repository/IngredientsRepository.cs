using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.TypeLib;

namespace Library.Repository
{
    public class IngredientsRepository : GenericRepository<Ingredient>
    {
        string tableName;
        public IngredientsRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }
    }
}
