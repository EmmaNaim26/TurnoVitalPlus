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
            using var cn = _factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT id, nombre AS fullname, curp, hospital, puesto FROM personal WHERE curp=@curp AND contraseña=@pass LIMIT 1", cn);
            cmd.Parameters.AddWithValue("@curp", curp);
            cmd.Parameters.AddWithValue("@pass", password);
            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;
            return new User
            {
                Id = rd.GetInt32("id"),
                FullName = rd.IsDBNull(rd.GetOrdinal("fullname")) ? "" : rd.GetString("fullname"),
                Curp = rd.IsDBNull(rd.GetOrdinal("curp")) ? "" : rd.GetString("curp"),
                Hospital = rd.IsDBNull(rd.GetOrdinal("hospital")) ? "" : rd.GetString("hospital"),
                Position = rd.IsDBNull(rd.GetOrdinal("puesto")) ? "" : rd.GetString("puesto")
            };
        }

        public User? GetById(int id)
        {
            using var cn = _factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT id, nombre AS fullname, curp, hospital, puesto FROM personal WHERE id=@id LIMIT 1", cn);
            cmd.Parameters.AddWithValue("@id", id);
            using var rd = cmd.ExecuteReader();
            if (!rd.Read()) return null;
            return new User
            {
                Id = rd.GetInt32("id"),
                FullName = rd.IsDBNull(rd.GetOrdinal("fullname")) ? "" : rd.GetString("fullname"),
                Curp = rd.IsDBNull(rd.GetOrdinal("curp")) ? "" : rd.GetString("curp"),
                Hospital = rd.IsDBNull(rd.GetOrdinal("hospital")) ? "" : rd.GetString("hospital"),
                Position = rd.IsDBNull(rd.GetOrdinal("puesto")) ? "" : rd.GetString("puesto")
            };
        }
    }
}
