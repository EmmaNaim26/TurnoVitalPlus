using System.Linq;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Datos;

namespace TurnoVitalPlus.Vista
{
    public class SpacesView : ControlVista
    {
        public SpacesView(RootController root, ISpacesRepository repo, int userId)
        {
            Dock = DockStyle.Fill;
            var sidebar = new Sidebar();
            sidebar.OnInicio += () => root.Start();
            Controls.Add(sidebar);

            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 2 };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 220));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            Controls.Add(panel);

            var card1 = MakeCard();
            card1.Controls.Add(MakeH1("Espacios disponibles"));
            var list = repo.GetAvailableSpaces().ToList();
            card1.Controls.Add(new Label { Text = list.Any() ? string.Join(", ", list) : "Sin espacios disponibles", AutoSize = true, Top = 50 });
            panel.Controls.Add(card1);

            var card2 = MakeCard();
            card2.Controls.Add(MakeH1("Días de descanso disponibles"));
            var days = repo.GetAvailableRestDays(userId).ToList();
            card2.Controls.Add(new Label { Text = days.Any() ? string.Join(", ", days) : "Ya has asignado tus días de descanso", AutoSize = true, Top = 50 });
            panel.Controls.Add(card2);
        }
    }
}
