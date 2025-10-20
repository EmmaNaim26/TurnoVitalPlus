using Proyecto.Controlador;
using System;
using System.Configuration;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Datos;
using TurnoVitalPlus.Vista;

namespace TurnoVitalPlus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string conn = ConfigurationManager.ConnectionStrings["TurnoVitalDb"].ConnectionString;
            var factory = new MySqlConnectionFactory(conn);
            var repos = RepositoryRegistry.Build(factory);

            var main = new MainForm();
            var root = new RootController(repos, main);

            main.SetRootController(root);
            root.Start();

            Application.Run(main);
        }
    }
}
