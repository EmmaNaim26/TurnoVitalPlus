using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public class ScheduleView : ControlVista
    {
        public ScheduleView(RootController root, Schedule schedule)
        {
            Dock = DockStyle.Fill;
            var sidebar = new Sidebar();
            sidebar.OnInicio += () => root.Start();
            Controls.Add(sidebar);

            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            Controls.Add(panel);

            var card = MakeCard();
            card.Controls.Add(MakeH1("Horario Laboral"));
            panel.Controls.Add(card);

            var grid = new DataGridView { Dock = DockStyle.Fill, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            // Fill grid using schedule.Grid if needed
            panel.Controls.Add(grid);
        }
    }
}
