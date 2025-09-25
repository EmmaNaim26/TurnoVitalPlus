// Reproduce la Imagen 5 con lista de botones de turno con '+' y un panel rojo de "Día de descanso extraordinario no disponible".
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;
using TurnoVitalPlus.Models;

namespace TurnoVitalPlus.Views
{
    public class RequestView : ControlView
    {
        private readonly RootController _root;

        public RequestView(RootController root) => _root = root;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var sidebar = new Sidebar();
            sidebar.OnInicio += _root.GoHome;
            sidebar.OnHorario += _root.GoSchedule;
            sidebar.OnEspacios += _root.GoSpaces;
            sidebar.OnSolicitar += _root.GoRequests;
            Controls.Add(sidebar);

            var surface = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, Padding = new Padding(12) };
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            Controls.Add(surface);
            surface.BringToFront();

            var header = new Panel { Dock = DockStyle.Top, Height = 56, Padding = new Padding(16), BackColor = PanelGray };
            header.Controls.Add(new Label { Text = "Solicita un turno o dia de descanso extraordinario", AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold) });
            surface.Controls.Add(header, 0, 0); surface.SetColumnSpan(header, 2);

            // Columna izquierda: botones de solicitud
            var left = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true };
            surface.Controls.Add(left, 0, 1);

            void AddRequest(string code, string label)
            {
                var container = new Panel { Width = 420, Height = 44, BackColor = Color.Gainsboro, Margin = new Padding(10, 8, 10, 8) };
                var txt = new Label { Text = label, AutoSize = false, Width = 300, Height = 44, TextAlign = ContentAlignment.MiddleLeft, Font = new Font("Segoe UI", 12, FontStyle.Bold), Left = 16 };
                var plus = new Button { Text = "+", Width = 44, Height = 44, BackColor = Color.FromArgb(0, 170, 90), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Left = 360, Top = 0 };
                plus.FlatAppearance.BorderSize = 0;
                plus.Click += (_, __) => _root.ApplyShiftRequest(new ShiftRequest { Code = code });
                container.Controls.Add(txt); container.Controls.Add(plus);
                left.Controls.Add(container);
            }

            AddRequest("MAT_URG", "MATUTINO URG");
            AddRequest("MAT_CIR", "MATUTINO CIR");
            AddRequest("MAT_REPC", "MATUTINO REPC");
            AddRequest("VES_COM", "VESPERTINO COM");
            AddRequest("MAT_MED", "MATUTINO MED");
            AddRequest("MAT_ENF", "MATUTINO ENF");

            // Columna derecha: panel rojo de no disponible
            var right = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            surface.Controls.Add(right, 1, 1);

            var red = new Panel { BackColor = Color.FromArgb(235, 87, 87), Dock = DockStyle.Top, Height = 160, Padding = new Padding(16) };
            red.Controls.Add(new Label
            {
                Text = "Dia de descanso extraordinario\nno disponible",
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White
            });
            right.Controls.Add(red);
        }
    }
}
