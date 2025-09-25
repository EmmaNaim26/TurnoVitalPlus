us// Lee valores de App.config para no acoplar el código a cadenas literales.
using System.Configuration;

namespace TurnoVitalPlus.Infrastructure
{
    public static class AppSettings
    {
        // Cadena de conexión expuesta a la app
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["TurnoVitalPlusDb"]?.ConnectionString ?? "";

        // URLs configurables para los linklabels de Guía/Soporte/Ayuda
        public static string UrlGuia => ConfigurationManager.AppSettings["UrlGuia"] ?? "https://example.com/guia";
        public static string UrlSoporte => ConfigurationManager.AppSettings["UrlSoporte"] ?? "https://example.com/soporte";
        public static string UrlAyuda => ConfigurationManager.AppSettings["UrlAyuda"] ?? "https://example.com/ayuda";
    }
}
