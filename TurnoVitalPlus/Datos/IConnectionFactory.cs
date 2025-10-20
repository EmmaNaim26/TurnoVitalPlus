using MySql.Data.MySqlClient;

namespace TurnoVitalPlus.Datos
{
    public interface IConnectionFactory
    {
        MySqlConnection Create();
    }
}
