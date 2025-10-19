using System.Configuration;
using MySql.Data.MySqlClient;

namespace TurnoVital
{
    public class Database
    {
        private readonly string connectionString;

        public Database()
        {
            // Lee la cadena de conexión desde App.config
            connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
