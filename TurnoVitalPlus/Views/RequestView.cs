using System;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public class RequestView : ControlVista
    {
        private readonly RootController _root;
        public RequestView(RootController root, TurnoVitalPlus.Datos.IScheduleRepository scheduleRepo)
        {
            _root = root;
            Dock = DockStyle.Fill;
            var sidebar = new Sidebar();
            sidebar.OnInicio += () => root.Start();
            Controls.Add(sidebar);

            var panel = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            Controls.Add(panel);

            panel.Controls.Add(MakeH1("Solicita un turno o día de descanso extraordinario"));

            var btn = new Button { Text = "Solicitar MATUTINO URG" };
            btn.Click += (_, __) =>
            {
                var req = new ShiftRequest { Code = "MAT_URG_" + DateTime.Now.Ticks };
                _root.ApplyRequest(req);
            };
            panel.Controls.Add(btn);
        }
    }
}
