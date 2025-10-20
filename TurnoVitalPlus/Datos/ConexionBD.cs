using MySql.Data.MySqlClient;

namespace TurnoVitalPlus.Datos
{
    public static class ConexionBD
    {
        private static string connectionString = "Server=127.0.0.1;Database=TurniVitalDB;Uid=root;Pwd=AdminDB9026_;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
