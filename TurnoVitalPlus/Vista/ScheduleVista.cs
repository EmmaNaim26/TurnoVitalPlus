// Reproduce la Imagen 3 con una cuadrícula de horario y etiquetas inferiores de zona y turno.
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;
using TurnoVitalPlus.Models;

namespace TurnoVitalPlus.Views
{
    public class ScheduleView : ControlView
    {
        private readonly RootController _root;
        private readonly Schedule _schedule;

        public ScheduleView(RootController root, Schedule schedule)
        {
            _root = root;
            _schedule = schedule;
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

            var surface = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12) };
            Controls.Add(surface);
            surface.BringToFront();

            // Borde verde exterior como el boceto
            var bordered = new Panel { Dock = DockStyle.Fill, Padding = new Padding(12), BackColor = Color.FromArgb(84, 200, 104) };
            var inner = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(8) };
            surface.Controls.Add(bordered);
            bordered.Controls.Add(inner);

            var title = new Label { Text = "Horario Laboral", AutoSize = true, Font = TitleFont };
            inner.Controls.Add(title);

            // Tabla de horario: DataGridView configurado para verse como plantilla
            var grid = new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                RowHeadersVisible = true,
                ColumnHeadersHeight = 36,
                Dock = DockStyle.None,
                Width = 700,
                Height = 260,
                Location = new Point(10, 50),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Columnas por día
            grid.Columns.Add("Lunes", "LUNES");
            grid.Columns.Add("Martes", "MARTES");
            grid.Columns.Add("Miercoles", "MIERCOLES");
            grid.Columns.Add("Jueves", "JUEVES");
            grid.Columns.Add("Viernes", "VIERNES");
            grid.Columns.Add("Sabado", "SABADO");
            grid.Columns.Add("Domingo", "DOMINGO");

            // Filas por bloques de tiempo con encabezado HRS
            grid.Rows.Add(_schedule.Grid[0, 0], _schedule.Grid[0, 1], _schedule.Grid[0, 2], _schedule.Grid[0, 3], _schedule.Grid[0, 4], _schedule.Grid[0, 5], _schedule.Grid[0, 6]);
            grid.Rows[0].HeaderCell.Value = "7-12";
            grid.Rows.Add(_schedule.Grid[1, 0], _schedule.Grid[1, 1], _schedule.Grid[1, 2], _schedule.Grid[1, 3], _schedule.Grid[1, 4], _schedule.Grid[1, 5], _schedule.Grid[1, 6]);
            grid.Rows[1].HeaderCell.Value = "12-13";
            grid.Rows.Add(_schedule.Grid[2, 0], _schedule.Grid[2, 1], _schedule.Grid[2, 2], _schedule.Grid[2, 3], _schedule.Grid[2, 4], _schedule.Grid[2, 5], _schedule.Grid[2, 6]);
            grid.Rows[2].HeaderCell.Value = "13-18";
            grid.Rows.Add(_schedule.Grid[3, 0], _schedule.Grid[3, 1], _schedule.Grid[3, 2], _schedule.Grid[3, 3], _schedule.Grid[3, 4], _schedule.Grid[3, 5], _schedule.Grid[3, 6]);
            grid.Rows[3].HeaderCell.Value = "18-19";

            // Ajustes visuales
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            grid.RowTemplate.Height = 60;

            inner.Controls.Add(grid);

            // Etiquetas inferiores (zona y turno) como en el boceto
            var lblZona = new Label { AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), Text = "Zona donde labora:" };
            var valZona = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Text = _schedule.Zone };
            var lblTurno = new Label { AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), Text = "Turno Laboral:" };
            var valTurno = new Label { AutoSize = true, Font = new Font("Segoe UI", 10), Text = _schedule.ShiftLabel };

            var info = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, Location = new Point(20, 330) };
            var r1 = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
            r1.Controls.Add(lblZona); r1.Controls.Add(new Label { Width = 8 }); r1.Controls.Add(valZona);
            var r2 = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true };
            r2.Controls.Add(lblTurno); r2.Controls.Add(new Label { Width = 8 }); r2.Controls.Add(valTurno);
            info.Controls.Add(r1); info.Controls.Add(r2);
            inner.Controls.Add(info);
        }
    }
}
