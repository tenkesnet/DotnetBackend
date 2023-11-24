using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Npgsql;

namespace MyDataGrid
{
    public class DapperContext

    {
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public readonly NpgsqlConnection connection;
        public DapperContext()
        {
            //_configuration = configuration;
            _connectionString = "Host=localhost;Database=sporttabella;Username=postgres;Password=PostgreSQL";
            connection = new NpgsqlConnection(_connectionString);
            connection.Open();
        }


        /*public IDbConnection CreateConnection()
        {
            var connection =new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }*/
    }

}
