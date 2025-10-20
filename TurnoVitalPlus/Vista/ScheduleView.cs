using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public class ScheduleView : UserControl
    {
        private readonly RootController _root;
        private readonly Schedule _schedule;

        public ScheduleView(RootController root, Schedule schedule)
        {
            _root = root;
            _schedule = schedule;
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

            var title = new Label { Text = "Horario Laboral", Font = new Font("Segoe UI", 16, FontStyle.Bold), AutoSize = true };
            content.Controls.Add(title);

            var grid = new DataGridView { Top = 60, Left = 16, Width = 720, Height = 320, ReadOnly = true, AllowUserToAddRows = false };
            grid.ColumnCount = 7;
            for (int i = 0; i < 7; i++) grid.Columns[i].Name = $"Día {i + 1}";
            for (int r = 0; r < _schedule.Grid.GetLength(0); r++)
            {
                var row = new string[7];
                for (int c = 0; c < 7; c++) row[c] = _schedule.Grid[r, c] ?? "";
                grid.Rows.Add(row);
            }
            content.Controls.Add(grid);
        }
    }
}
