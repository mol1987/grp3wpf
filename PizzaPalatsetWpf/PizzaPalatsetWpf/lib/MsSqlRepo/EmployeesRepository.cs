using System;
using System.Collections.Generic;
using System.Text;
using TypeLib;

namespace MsSqlRepo
{
    public class EmployeesRepository : GenericRepository<Employees>
    {
        public EmployeesRepository(string tableName) : base(tableName)
        {
           
        }

    }
}
