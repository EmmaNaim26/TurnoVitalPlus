// Controlador auxiliar para abrir enlaces de Guía/Soporte/Ayuda desde la vista.
using System.Diagnostics;
using TurnoVitalPlus.Infrastructure;

namespace TurnoVitalPlus.Controllers
{
    public static class HelpLinksController
    {
        public static void OpenGuide() => Process.Start(new ProcessStartInfo { FileName = AppSettings.UrlGuia, UseShellExecute = true });
        public static void OpenSupport() => Process.Start(new ProcessStartInfo { FileName = AppSettings.UrlSoporte, UseShellExecute = true });
        public static void OpenHelp() => Process.Start(new ProcessStartInfo { FileName = AppSettings.UrlAyuda, UseShellExecute = true });
    }
}
