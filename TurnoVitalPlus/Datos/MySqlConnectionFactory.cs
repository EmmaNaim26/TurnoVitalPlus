using System.Data;
using MySql.Data.MySqlClient;

namespace TurnoVitalPlus.Datos
{
    public class MySqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public MySqlConnectionFactory(string connectionString) => _connectionString = connectionString;
        public IDbConnection Create() => new MySqlConnection(_connectionString);
    }
}
