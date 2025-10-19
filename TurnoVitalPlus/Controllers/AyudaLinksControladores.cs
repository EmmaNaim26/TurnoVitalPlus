using System;
using System.Diagnostics;
using TurnoVitalPlus.Infrastructure;

namespace TurnoVitalPlus.Controlador
{
    public static class AyudaLinksControladores
    {
        public static void OpenGuia() => OpenUrl(AppSettings.UrlGuia);
        public static void OpenSoporte() => OpenUrl(AppSettings.UrlSoporte);
        public static void OpenAyuda() => OpenUrl(AppSettings.UrlAyuda);

        private static void OpenUrl(string url)
        {
            try
            {
                var psi = new ProcessStartInfo { FileName = url, UseShellExecute = true };
                Process.Start(psi);
            }
            catch { }
        }
    }
}
