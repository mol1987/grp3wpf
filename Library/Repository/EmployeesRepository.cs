using System;
using System.Collections.Generic;
using System.Text;
using Library.TypeLib;

namespace Library.Repository
{
    public class EmployeesRepository : GenericRepository<Employee>
    {
        public EmployeesRepository(string tableName) : base(tableName)
        {
           
        }

    }
}
