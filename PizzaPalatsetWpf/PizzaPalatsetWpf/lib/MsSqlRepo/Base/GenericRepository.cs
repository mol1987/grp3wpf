using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


/// <summary>
/// A generic repository. Basic CRUD operations.
/// </summary>

namespace MsSqlRepo
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly string _tableName;

        protected GenericRepository(string tableName)
        {
            _tableName = tableName;
        }
        /// <summary>
        /// Generate new connection based on connection string
        /// </summary>
        /// <returns></returns>
        private SqlConnection SqlConnection()
        {
            // Fetching from locally stored secrets
            string host = Helper.Globals.Get("host");
            string database = Helper.Globals.Get("db");
            string username = Helper.Globals.Get("usr");
            string password = Helper.Globals.Get("pwd");
            string port = Helper.Globals.Get("port");

            string connectionString = $"Data Source={host};Initial Catalog={database};User Id={username};Password={password};";
            return new SqlConnection(connectionString);
        }
        private NpgsqlConnection SqlConnectionPost()
        {
            // Fetching from locally stored secrets
            string host = Helper.Globals.Get("host");
            string database = Helper.Globals.Get("db");
            string username = Helper.Globals.Get("usr");
            string password = Helper.Globals.Get("pwd");
            string port = Helper.Globals.Get("port");

            string connectionString = $"Host={host};Username={username};Password={password};Database={database};Pooling=true; Port={port}";

            //Console.WriteLine(connectionString);
            return new NpgsqlConnection(connectionString);
        }
        /// <summary>
        /// Open new connection and return it for use
        /// </summary>
        /// <returns></returns>
        protected IDbConnection CreateConnection()
        {
            string backend = Helper.Globals.Get("backend");
            IDbConnection conn = null;

            if (backend == null)
                throw new Exception("Backend-type is not assigned");

            if (new string[] { "postgresql", "postgres", "postgre", "psql" }.Contains(backend.ToLower()))
                conn = SqlConnectionPost();
            else if (new string[] { "sql-server", "mssql", "sqlserver" }.Contains(backend.ToLower()))
                conn = SqlConnection();
            conn.Open();
            return conn;
        }
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        /// <summary>
        /// Gets all data from table
        /// </summary>
        /// <returns>Container of tabledata</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>($"SELECT * FROM {_tableName}");
            }
        }
        /// <summary>
        /// Delete a specific row
        /// </summary>
        /// <param name="id">Identification no.</param>
        /// <returns></returns>
        public async Task DeleteRowAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync($"DELETE FROM {_tableName} WHERE Id=@Id", new { Id = id });
            }
        }
        /// <summary>
        /// Get specific row
        /// </summary>
        /// <param name="id">Identification no.</param>
        /// <returns>An object with table data</returns>
        public async Task<T> GetAsync(int id)
        {
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE Id=@Id", new { Id = id });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");

                return result;
            }
        }
        /// <summary>
        /// Inserts a range of tabledata
        /// </summary>
        /// <param name="list">A list of tabledata</param>
        /// <returns>No. inserted rows</returns>
        public async Task<int> SaveRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            using (var connection = CreateConnection())
            {
                inserted += await connection.ExecuteAsync(query, list);
            }

            return inserted;
        }
        /// <summary>
        /// Gets properties from class
        /// </summary>
        /// <param name="listOfProperties"></param>
        /// <returns></returns>
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
        /// <summary>
        /// Inserts object to table
        /// </summary>
        /// <param name="t">Object</param>
        /// <returns></returns>
        public async Task InsertAsync(T t)
        {
            var insertQuery = GenerateInsertQuery(t);

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(insertQuery, t);
            }
        }
        /// <summary>
        /// Generate an sql string with insert info
        /// </summary>
        /// <param name="t">Object to generate insert query</param>
        /// <returns>Sql string</returns>
        public string GenerateInsertQuery(T t)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

            insertQuery.Append("(");

            //var properties = GenerateListOfProperties(GetProperties)
            List<string> properties = new List<string>();
            foreach (var prop in t.GetType().GetProperties())
            {
                if (prop.GetValue(t, null) != null)
                    properties.Add(prop.Name);

            }
            properties.ForEach(prop =>
            {
                insertQuery.Append($"[{prop}],");
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");
            return insertQuery.ToString();
        }
        /// <summary>
        /// Generate an sql string with insert info
        /// </summary>
        /// <returns>Sql query</returns>
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");

            insertQuery.Append("(");

            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(prop =>
            {
                insertQuery.Append($"[{prop}],");
            });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");

            return insertQuery.ToString();
        }

        /// <summary>
        /// Updates a row of table data
        /// </summary>
        /// <param name="t">Object to update</param>
        /// <returns></returns>
        public async Task UpdateAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery(t);

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(updateQuery, t);
            }
        }

        /// <summary>
        /// generates an update sql query string from object
        /// </summary>
        /// <param name="t">Object to generate from</param>
        /// <returns></returns>
        private string GenerateUpdateQuery(T t)
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            List<string> properties = new List<string>();
            foreach (var prop in t.GetType().GetProperties())
            {
                if (prop.GetValue(t, null) != null)
                    properties.Add(prop.Name);

            }

            properties.ForEach(property =>
            {
                if (!property.Equals("ID"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });


            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
        /// <summary>
        /// generates an update sql query string
        /// </summary>
        /// <returns></returns>
        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);

            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });

            updateQuery.Remove(updateQuery.Length - 1, 1); //remove last comma
            updateQuery.Append(" WHERE Id=@Id");

            return updateQuery.ToString();
        }
        /// <summary>
        /// Like InsertAsync() but with integer-id return
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public async Task<int> InsertWithReturnAsync(T t)
        {
            var insertQuery = GenerateInsertQuery(t);

            using (var connection = CreateConnection())
            {
                var res = await connection.ExecuteAsync(insertQuery, t);
                return res;
            }
        }
        /// <summary>
        /// Dry run insert with rollback. Test Insert functionaility without adding data
        /// </summary>
        /// <returns></returns>
        public async Task DryInsert(T t)
        {
            var insertQuery = GenerateInsertQuery(t);
            StringBuilder sb = new StringBuilder(insertQuery);
            sb.Insert(0, "BEGIN TRAN Tr1");
            sb.Append("ROLLBACK TRAN Tr1;");
            using (var connection = CreateConnection())
            {
                var res = await connection.ExecuteAsync(sb.ToString(), t);
                var x = res;
            }
        }

    }
}
