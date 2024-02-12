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
        private string _connectionString { get; }
        public  NpgsqlConnection connection { get; }
        public DapperContext()
        {
            _connectionString = "Host=localhost;Database=anyexcel;Username=postgres;Password=PostgreSQL";
            connection = new NpgsqlConnection(_connectionString);
            connection.Open();
        }
    }

}
