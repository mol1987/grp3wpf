using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Library.TypeLib;

namespace Library.Repository
{
    public class EmployeesRepository : GenericRepository<Employee>
    {
        public EmployeesRepository(string tableName) : base(tableName)
        {
           
        }
        public async Task InsertAsync(Employee employee)
        {
            using (var connection = CreateConnection())
            {
                string sql = @"INSERT INTO Employees (Name, LastName, Email, Password) VALUES (@Name, @LastName, @Email, @Password) SELECT CAST(SCOPE_IDENTITY() as int)";
                var id = (await connection.QueryAsync<int>(sql, new { Name = employee.Name, LastName = employee.LastName, Email = employee.Email, Password = employee.Password })).Single();
                employee.ID = id; 
            }
        }
    }
}
