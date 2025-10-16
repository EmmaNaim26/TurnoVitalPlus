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
            ApplicationConfiguration.Initialize();

            var connectionFactory = new SqlConnectionFactory(AppSettings.ConnectionString);
            var repositories = RepositoryRegistry.Build(connectionFactory);
            var rootController = new RootController(repositories);
            var mainForm = new MainForm(rootController);
            rootController.Bind(mainForm);

            Application.Run(mainForm);
        }
    }
}
