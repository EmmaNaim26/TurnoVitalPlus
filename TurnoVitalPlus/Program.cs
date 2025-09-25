// Punto de entrada del programa: inicializa estilos y abre la ventana principal.
// Mantiene una sola ventana; las secciones se cargan como vistas (UserControls) dentro del MainForm.
using System;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;
using TurnoVitalPlus.Infrastructure;

namespace TurnoVitalPlus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Habilita estilos visuales de WinForms
            ApplicationConfiguration.Initialize();

            // Inyección mínima: fabrica de conexiones y repositorios (preparado para BD futura)
            var connectionFactory = new SqlConnectionFactory(AppSettings.ConnectionString);
            var repositories = RepositoryRegistry.Build(connectionFactory);

            // Controlador raíz: orquesta navegación y flujo de la app
            var rootController = new RootController(repositories);

            // Ventana única: aloja todas las vistas
            var mainForm = new MainForm(rootController);

            // El controlador raíz necesita referenciar el contenedor visual para inyectar vistas
            rootController.Bind(mainForm);

            Application.Run(mainForm);
        }
    }
}
