using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Datos;

namespace TurnoVitalPlus.Vista
{
    public class SpacesView : UserControl
    {
        private readonly RootController _root;
        private readonly ISpacesRepository _repo;
        private readonly int _userId;

        public SpacesView(RootController root, ISpacesRepository repo, int userId)
        {
            _root = root;
            _repo = repo;
            _userId = userId;
            InitializeComponent();
            BuildUi();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1000, 700);
        }

        private void BuildUi()
        {
            var sidebar = new Panel { Width = 220, Dock = DockStyle.Left, BackColor = Color.FromArgb(245, 245, 245) };
            Controls.Add(sidebar);

            var content = new Panel { Dock = DockStyle.Fill, Padding = new Padding(16) };
            Controls.Add(content);

            var lblTitle = new Label { Text = "Espacios Disponibles", Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true };
            content.Controls.Add(lblTitle);

            var spaces = _repo.GetAvailableSpaces().ToList();
            var lst = new ListBox { Top = 50, Left = 16, Width = 500, Height = 200 };
            lst.Items.AddRange(spaces.ToArray());
            content.Controls.Add(lst);

            var restDays = _repo.GetAvailableRestDays(_userId).ToList();
            var lblDays = new Label { Text = restDays.Any() ? string.Join(", ", restDays) : "No hay días disponibles", Top = 270, Left = 16, AutoSize = true };
            content.Controls.Add(lblDays);
        }
    }
}
