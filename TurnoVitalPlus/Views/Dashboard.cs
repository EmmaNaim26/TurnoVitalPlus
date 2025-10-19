using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public class DashboardView : ControlVista
    {
        private readonly RootController _root;
        private readonly Schedule _schedule;

        public DashboardView(RootController root, Schedule schedule)
        {
            _root = root;
            _schedule = schedule;
            Dock = DockStyle.Fill;
            BuildUi();
        }

        private void BuildUi()
        {
            var sidebar = new Sidebar();
            sidebar.OnInicio += () => _root.Start();
            sidebar.OnHorario += () => _root.GoToSchedule();
            sidebar.OnEspacios += () => _root.GoToSpaces();
            sidebar.OnSolicitar += () => _root.GoToRequest();
            sidebar.OnGuia += () => AyudaLinksControladores.OpenGuia();
            sidebar.OnSoporte += () => AyudaLinksControladores.OpenSoporte();
            sidebar.OnAyuda += () => AyudaLinksControladores.OpenAyuda();
            Controls.Add(sidebar);

            var surface = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(12) };
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            Controls.Add(surface);

            var left = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            var right = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            surface.Controls.Add(left, 0, 0);
            surface.Controls.Add(right, 1, 0);

            var cardUser = MakeCard();
            cardUser.Controls.Add(MakeH1("Datos del Usuario"));
            var u = _root.CurrentUser;
            var lbl = new Label { Text = u != null ? $"{u.FullName}\n{u.Curp}\n{u.Hospital}" : "Sin usuario", AutoSize = true, Top = 50 };
            cardUser.Controls.Add(lbl);
            left.Controls.Add(cardUser);

            var cardHospital = MakeCard();
            cardHospital.Controls.Add(MakeH1("Datos del Hospital"));
            cardHospital.Controls.Add(new Label { Text = "Hospital General \"Juan Blanco Alarcon\"\nAv. Universidad 6142, Acapulco", AutoSize = true, Top = 50 });
            left.Controls.Add(cardHospital);

            var cardCal = MakeCard();
            cardCal.Controls.Add(MakeH1(DateTime.Now.ToString("MMMM yyyy")));
            var cal = new MonthCalendar { Dock = DockStyle.Top, MaxSelectionCount = 1 };
            cardCal.Controls.Add(cal);
            right.Controls.Add(cardCal);

            var cardTurno = MakeCard();
            cardTurno.Controls.Add(MakeH1("Tu turno:"));
            cardTurno.Controls.Add(new Label { Text = _schedule.CurrentTurn, AutoSize = true, Top = 50 });
            right.Controls.Add(cardTurno);
        }
    }
}
