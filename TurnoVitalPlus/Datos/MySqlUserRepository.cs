using System;
using MySql.Data.MySqlClient;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Datos
{
    public class MySqlUserRepository : IUserRepository
    {
        private readonly IConnectionFactory _factory;
        public MySqlUserRepository(IConnectionFactory factory) => _factory = factory;

        public User? Authenticate(string curp, string password)
        {
            using var cn = (MySqlConnection)_factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT id, nombre, curp, hospital, puesto FROM personal WHERE curp=@c AND contraseña=@p LIMIT 1", cn);
            cmd.Parameters.AddWithValue("@c", curp);
            cmd.Parameters.AddWithValue("@p", password);
            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;
            return new User
            {
                Id = rd.GetInt32("id"),
                FullName = rd.GetString("nombre"),
                Curp = rd.GetString("curp"),
                Hospital = rd.IsDBNull(rd.GetOrdinal("hospital")) ? "" : rd.GetString("hospital"),
                Position = rd.IsDBNull(rd.GetOrdinal("puesto")) ? "" : rd.GetString("puesto")
            };
        }

        public int CreateUser(User user, string password)
        {
            using var cn = (MySqlConnection)_factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("INSERT INTO personal (nombre, curp, hospital, contraseña, puesto) VALUES (@n,@c,@h,@p,@pos); SELECT LAST_INSERT_ID();", cn);
            cmd.Parameters.AddWithValue("@n", user.FullName);
            cmd.Parameters.AddWithValue("@c", user.Curp);
            cmd.Parameters.AddWithValue("@h", user.Hospital ?? "");
            cmd.Parameters.AddWithValue("@p", password);
            cmd.Parameters.AddWithValue("@pos", user.Position ?? "pendiente");
            var idObj = cmd.ExecuteScalar();
            return Convert.ToInt32(idObj);
        }

        public User? GetById(int id)
        {
            using var cn = (MySqlConnection)_factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT id, nombre, curp, hospital, puesto FROM personal WHERE id=@id LIMIT 1", cn);
            cmd.Parameters.AddWithValue("@id", id);
            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;
            return new User
            {
                Id = rd.GetInt32("id"),
                FullName = rd.GetString("nombre"),
                Curp = rd.GetString("curp"),
                Hospital = rd.GetString("hospital"),
                Position = rd.GetString("puesto")
            };
        }

        public User? GetByCurp(string curp)
        {
            using var cn = (MySqlConnection)_factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT id, nombre, curp, hospital, puesto FROM personal WHERE curp=@c LIMIT 1", cn);
            cmd.Parameters.AddWithValue("@c", curp);
            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;
            return new User
            {
                Id = rd.GetInt32("id"),
                FullName = rd.GetString("nombre"),
                Curp = rd.GetString("curp"),
                Hospital = rd.GetString("hospital"),
                Position = rd.GetString("puesto")
            };
        }
    }
}
