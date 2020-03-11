using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TypeLib;

namespace MsSqlRepo
{
    public class IngredientsRepository : GenericRepository<Ingredients>
    {
        string tableName;
        public IngredientsRepository(string tableName) : base(tableName)
        {
            this.tableName = tableName;
        }
    }
}
