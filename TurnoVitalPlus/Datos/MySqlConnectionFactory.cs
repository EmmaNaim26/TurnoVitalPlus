using MySql.Data.MySqlClient;

namespace TurnoVitalPlus.Datos
{
    public class MySqlConnectionFactory : IConnectionFactory
    {
        private readonly string _conn;
        public MySqlConnectionFactory(string connectionString) => _conn = connectionString;
        public MySqlConnection Create() => new MySqlConnection(_conn);
    }
}
