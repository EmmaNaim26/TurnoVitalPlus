using System.Data;

namespace TurnoVitalPlus.Datos
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}
