// Reproduce la Imagen 4 con dos tarjetas: "Espacios disponibles" y "Dias de descanso disponibles".
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TurnoVitalPlus.Views
{
    using TurnoVitalPlus.Controllers;

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
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            var sidebar = new Sidebar();
            sidebar.OnInicio += _root.GoHome;
            sidebar.OnHorario += _root.GoSchedule;
            sidebar.OnEspacios += _root.GoSpaces;
            sidebar.OnSolicitar += _root.GoRequests;
            Controls.Add(sidebar);

            var surface = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, Padding = new Padding(12) };
            Controls.Add(surface);
            surface.BringToFront();

            var card1 = MakeCard();
            card1.Controls.Add(MakeH1("Espacios disponibles"));
            var spaces = _spacesRepo.GetAvailableSpaces().ToList();
            var msg1 = spaces.Any() ? string.Join(", ", spaces) : "Sin espacios disponibles";
            var txt1 = new Label { Text = msg1, AutoSize = true, Font = new Font("Segoe UI", 12) };
            txt1.Top = 60; card1.Controls.Add(txt1);

            var card2 = MakeCard();
            card2.Controls.Add(MakeH1("Dias de descanso disponibles"));
            var rests = _spacesRepo.GetAvailableRestDays(_userId).ToList();
            var msg2 = rests.Any() ? string.Join(", ", rests) : "Ya has asignado tus dias de descanso";
            var txt2 = new Label { Text = msg2, AutoSize = true, Font = new Font("Segoe UI", 12) };
            txt2.Top = 60; card2.Controls.Add(txt2);

            surface.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            surface.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            surface.Controls.Add(card1, 0, 0);
            surface.Controls.Add(card2, 0, 1);
        }
    }
}
