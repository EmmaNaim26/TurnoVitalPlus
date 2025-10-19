using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace TurnoVital
{
    public class ConexionBD
    {
        private static string server = "127.0.0.1:3306"; // IP de la PC donde está la BD
        private static string database = "TurniVitalDB";   // Nombre de la base de datos
        private static string user = "root";             // Usuario MySQL
        private static string password = "AdminDB9026_"; // Contraseña MySQL
        private static string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";

        public static MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            return connection;
        }
    }
}

