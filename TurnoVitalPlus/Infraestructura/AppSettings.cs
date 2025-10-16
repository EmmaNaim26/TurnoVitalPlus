using System.Configuration;

namespace TurnoVitalPlus.Infrastructure
{
    public static class AppSettings
    {
        public static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["TurnoVitalPlusDb"]?.ConnectionString ?? "";

        public static string UrlGuia => ConfigurationManager.AppSettings["UrlGuia"] ?? "https://example.com/guia";
        public static string UrlSoporte => ConfigurationManager.AppSettings["UrlSoporte"] ?? "https://example.com/soporte";
        public static string UrlAyuda => ConfigurationManager.AppSettings["UrlAyuda"] ?? "https://example.com/ayuda";
    }
}
