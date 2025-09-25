// Fábrica de conexiones ADO.NET lista para futuras operaciones contra BD.
using System.Data;
using System.Data.SqlClient;

namespace TurnoVitalPlus
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }

    public class SqlConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;
        public SqlConnectionFactory(string connectionString) => _connectionString = connectionString;

        public IDbConnection Create()
        {
            // Regresa la conexión sin abrir; los repos la abrirán al usarla.
            return new SqlConnection(_connectionString);
        }
    }
}
