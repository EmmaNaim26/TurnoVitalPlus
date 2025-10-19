using System;
using MySql.Data.MySqlClient;

namespace TurnoVitalPlus.Controlador
{
    public class ControlRegister
    {
        private readonly string connectionString;

        public ControlRegister(string server, string database, string user, string password)
        {
            connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public bool RegistrarUsuario(string nombre, string correo, string contraseña)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO usuarios (nombre, correo, contraseña) VALUES (@nombre, @correo, @contraseña)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contraseña", contraseña);

                        int filas = cmd.ExecuteNonQuery();
                        return filas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar: " + ex.Message);
                return false;
            }
        }
    }
}
