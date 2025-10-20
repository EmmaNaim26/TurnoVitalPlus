using System.Diagnostics;

namespace TurnoVitalPlus.Controlador
{
    public static class AyudaLinksControladores
    {
        public static void AbrirLink(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch
            {
                // Se podría agregar un log o mensaje si falla
            }
        }
    }
}
