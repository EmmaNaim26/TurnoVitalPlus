// Views/SpacesView.cs
// Pestaña "Espacios" 100% responsive: dos tarjetas apiladas que se ajustan al tamaño de la ventana.

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;

namespace TurnoVitalPlus.Views
{
    public class SpacesView : ControlView
    {
        private readonly RootController _root;
        private readonly ISpacesRepository _spacesRepo;
        private readonly Guid _userId;

        public SpacesView(RootController root, ISpacesRepository repo, Guid userId)
        {
            _root = root;
            _spacesRepo = repo;
            _userId = userId;
            DoubleBuffered = true; // reduce flicker al redimensionar
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // ======= Sidebar fijo a la izquierda =======
            var sidebar = new Sidebar();
            sidebar.OnInicio += _root.GoHome;
            sidebar.OnHorario += _root.GoSchedule;
            sidebar.OnEspacios += _root.GoSpaces;
            sidebar.OnSolicitar += _root.GoRequests;
            Controls.Add(sidebar);

            // ======= Superficie derecha con layout vertical =======
            var surface = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(12),
                BackColor = Color.White
            };
            // Alturas: tarjeta superior con alto fijo cómodo; la inferior ocupa el resto
            surface.RowStyles.Add(new RowStyle(SizeType.Absolute, 220)); // "Espacios disponibles"
            surface.RowStyles.Add(new RowStyle(SizeType.Percent, 100));  // "Días de descanso disponibles"
            Controls.Add(surface);
            surface.BringToFront();

            // ======= Tarjeta 1: Espacios disponibles =======
            var cardEspacios = MakeCard();
            cardEspacios.Dock = DockStyle.Fill;
            cardEspacios.MinimumSize = new Size(0, 180);

            var header1 = MakeH1("Espacios disponibles");
            header1.Dock = DockStyle.Top;
            cardEspacios.Controls.Add(header1);

            var espacios = _spacesRepo.GetAvailableSpaces().ToList();
            var textoEspacios = new Label
            {
                Text = espacios.Any() ? string.Join(", ", espacios) : "Sin espacios disponibles",
                Dock = DockStyle.Fill,
                AutoSize = false,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Padding = new Padding(4),
            };
            // Un contenedor para dar margen superior al contenido
            var holder1 = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4, 10, 4, 4) };
            holder1.Controls.Add(textoEspacios);
            cardEspacios.Controls.Add(holder1);

            surface.Controls.Add(cardEspacios, 0, 0);

            // ======= Tarjeta 2: Días de descanso disponibles =======
            var cardDescanso = MakeCard();
            cardDescanso.Dock = DockStyle.Fill;
            cardDescanso.MinimumSize = new Size(0, 220);

            var header2 = MakeH1("Dias de descanso disponibles");
            header2.Dock = DockStyle.Top;
            cardDescanso.Controls.Add(header2);

            var descansos = _spacesRepo.GetAvailableRestDays(_userId).ToList();
            var textoDescansos = new Label
            {
                Text = descansos.Any() ? string.Join(", ", descansos) : "Ya has asignado tus dias de descanso",
                Dock = DockStyle.Fill,
                AutoSize = false,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                Padding = new Padding(4),
            };
            var holder2 = new Panel { Dock = DockStyle.Fill, Padding = new Padding(4, 10, 4, 4) };
            holder2.Controls.Add(textoDescansos);
            cardDescanso.Controls.Add(holder2);

            surface.Controls.Add(cardDescanso, 0, 1);
        }
    }
}
