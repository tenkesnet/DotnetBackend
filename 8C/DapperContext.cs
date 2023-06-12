using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace Tanulok
{
    public class DapperContext

    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public readonly SqliteConnection connection;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
            connection = new SqliteConnection(_connectionString);
            connection.Open();
        }
        public IDbConnection CreateConnection()
        {
            var connection =new SqliteConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
