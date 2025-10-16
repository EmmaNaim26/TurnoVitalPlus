using System.Data;
using Microsoft.Data.SqlClient;   // <-- paquete NuGet Microsoft.Data.SqlClient

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
            // Se regresa cerrada; los repos abrirán/cerarrán
            return new SqlConnection(_connectionString);
        }
    }
}

