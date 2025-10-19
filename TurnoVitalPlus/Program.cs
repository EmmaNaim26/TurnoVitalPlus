using System;
using System.Windows.Forms;

namespace TurnoVitalPlus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); // .NET 6/8 WinForms
            Application.Run(new MainForm());
        }
    }
}
