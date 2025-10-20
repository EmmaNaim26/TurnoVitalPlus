using System.Configuration;

namespace TurnoVitalPlus.Infraestructura
{
    public static class AppSettings
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["TurniVitalDB"]?.ConnectionString ?? "";
    }
}
