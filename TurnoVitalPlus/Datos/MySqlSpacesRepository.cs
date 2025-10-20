using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace TurnoVitalPlus.Datos
{
    public class MySqlSpacesRepository : ISpacesRepository
    {
        private readonly IConnectionFactory _factory;
        public MySqlSpacesRepository(IConnectionFactory factory) => _factory = factory;

        public IEnumerable<string> GetAvailableSpaces()
        {
            var list = new List<string>();
            using var cn = _factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT name FROM spaces WHERE available=1", cn);
            using var rd = cmd.ExecuteReader();
            while (rd.Read()) list.Add(rd.GetString(0));
            return list;
        }

        public IEnumerable<string> GetAvailableRestDays(int userId)
        {
            var list = new List<string>();
            using var cn = _factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT day_label FROM rest_days WHERE user_id=@u", cn);
            cmd.Parameters.AddWithValue("@u", userId);
            using var rd = cmd.ExecuteReader();
            while (rd.Read()) list.Add(rd.GetString(0));
            return list;
        }
    }
}
