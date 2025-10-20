using MySql.Data.MySqlClient;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Datos
{
    public class MySqlScheduleRepository : IScheduleRepository
    {
        private readonly IConnectionFactory _factory;
        public MySqlScheduleRepository(IConnectionFactory factory) => _factory = factory;

        public Schedule GetByUserId(int userId)
        {
            var s = new Schedule();
            using var cn = _factory.Create();
            cn.Open();
            using var cmd = new MySqlCommand("SELECT zone, shift_label, current_turn, rest_days_label, period_label, notes FROM schedules WHERE user_id=@id LIMIT 1", cn);
            cmd.Parameters.AddWithValue("@id", userId);
            using var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                s.Zone = rd.IsDBNull(0) ? s.Zone : rd.GetString(0);
                s.ShiftLabel = rd.IsDBNull(1) ? s.ShiftLabel : rd.GetString(1);
                s.CurrentTurn = rd.IsDBNull(2) ? s.CurrentTurn : rd.GetString(2);
                s.RestDaysLabel = rd.IsDBNull(3) ? s.RestDaysLabel : rd.GetString(3);
                s.PeriodLabel = rd.IsDBNull(4) ? s.PeriodLabel : rd.GetString(4);
                s.Notes = rd.IsDBNull(5) ? "" : rd.GetString(5);
            }

            // Fill simple grid if exists
            using (var cmd2 = new MySqlCommand("SELECT row_idx, col_idx, cell_text FROM schedule_grid WHERE user_id=@id", cn))
            {
                cmd2.Parameters.AddWithValue("@id", userId);
                using var rd2 = cmd2.ExecuteReader();
                while (rd2.Read())
                {
                    int r = rd2.GetInt32("row_idx");
                    int c = rd2.GetInt32("col_idx");
                    if (r >= 0 && r < s.Grid.GetLength(0) && c >= 0 && c < s.Grid.GetLength(1))
                        s.Grid[r, c] = rd2.IsDBNull(rd2.GetOrdinal("cell_text")) ? "" : rd2.GetString("cell_text");
                }
            }

            return s;
        }

        public void ApplyShiftRequest(int userId, ShiftRequest req)
        {
            using var cn = _factory.Create();
            cn.Open();
            using var tx = cn.BeginTransaction();
            using var cmd = new MySqlCommand("INSERT INTO shift_requests (user_id, code, requested_at, details) VALUES (@uid,@code,@dat,@det)", cn, tx);
            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@code", req.Code);
            cmd.Parameters.AddWithValue("@dat", req.RequestedAt);
            cmd.Parameters.AddWithValue("@det", req.Details);
            cmd.ExecuteNonQuery();

            using var cmd2 = new MySqlCommand("UPDATE schedules SET notes = CONCAT(IFNULL(notes,''), '\nSolicitud: ', @code) WHERE user_id=@uid", cn, tx);
            cmd2.Parameters.AddWithValue("@code", req.Code);
            cmd2.Parameters.AddWithValue("@uid", userId);
            cmd2.ExecuteNonQuery();

            tx.Commit();
        }
    }
}
