// Reproduce la Imagen 2: menú lateral + tarjetas "Datos del Usuario", "Datos del Hospital", calendario y panel derecho con turno actual.
using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;
using TurnoVitalPlus.Models;

namespace TurnoVitalPlus.Views
{
    public class DashboardView : ControlView
    {
        private readonly RootController _root;
        private readonly Schedule _schedule;

        public DashboardView(RootController root, Schedule schedule)
        {
            _root = root;
            _schedule = schedule;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // Sidebar (mismo en todas las vistas posteriores)
            var sidebar = new Sidebar();
            sidebar.OnInicio += _root.GoHome;
            sidebar.OnHorario += _root.GoSchedule;
            sidebar.OnEspacios += _root.GoSpaces;
            sidebar.OnSolicitar += _root.GoRequests;
            Controls.Add(sidebar);

            // Contenido principal a la derecha
            var surface = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2 };
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            surface.Padding = new Padding(10);
            Controls.Add(surface);
            surface.BringToFront();

            // Columna izquierda: Datos del Usuario + Datos del Hospital
            var datosUsuario = MakeCard();
            datosUsuario.Controls.Add(MakeH1("Datos del Usuario"));
            // Avatar + datos
            var avatar = new Panel { Width = 90, Height = 90, BackColor = Color.White, Margin = new Padding(0, 6, 12, 0) };
            avatar.Paint += (_, e) => { e.Graphics.FillEllipse(Brushes.Gray, 10, 10, 70, 70); }; // ícono simple
            var user = _root.CurrentUser!;
            var info = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = $"Nombre Completo\n{user.FullName}\n\nCURP\n{user.Curp}\n\nFecha de Nacimiento\n{user.BirthDate:dd/MM/yyyy}\n\nMatricula\n{user.Matricula}\n\nCorreo electronico\n{user.Email}"
            };
            var row = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, WrapContents = false };
            row.Controls.Add(avatar); row.Controls.Add(info);
            datosUsuario.Controls.Add(row);
            row.Top = 50;

            var datosHospital = MakeCard();
            datosHospital.Controls.Add(MakeH1("Datos del Hospital"));
            var hosp = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Text = "Hospital General “Juan Blanco Alarcon”\nAv. Universidad 6142, Acapulco GRO\nTurno: Matutino/Vespertino/Nocturno\nIngreso: Marzo 2022"
            };
            hosp.Top = 50;
            datosHospital.Controls.Add(hosp);

            var leftColumn = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
            leftColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            leftColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            leftColumn.Controls.Add(datosUsuario, 0, 0);
            leftColumn.Controls.Add(datosHospital, 0, 1);
            surface.Controls.Add(leftColumn, 0, 0);

            // Columna derecha: Calendario + datos del turno
            var rightColumn = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
            rightColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            rightColumn.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            surface.Controls.Add(rightColumn, 1, 0);

            var cardCalendar = MakeCard();
            var monthTitle = new Label
            {
                Text = $"{DateTime.Now:MMMM}".ToUpper() + $"     {DateTime.Now:yyyy}",
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold)
            };
            var calendar = new MonthCalendar
            {
                MaxSelectionCount = 1,
                ShowToday = false,
                Dock = DockStyle.Top
            };
            cardCalendar.Controls.Add(monthTitle);
            monthTitle.Top = 6; monthTitle.Left = 10;
            cardCalendar.Controls.Add(calendar);
            calendar.Top = 36; calendar.Left = 10;
            rightColumn.Controls.Add(cardCalendar, 0, 0);

            var cardTurno = MakeCard();
            var lbl1 = new Label { AutoSize = true, Font = SubtitleFont, Text = "Tu turno:" };
            var val1 = new Label { AutoSize = true, Font = new Font("Segoe UI", 12), Text = _schedule.CurrentTurn };
            var lbl2 = new Label { AutoSize = true, Font = SubtitleFont, Text = "Dias de descanso:" };
            var val2 = new Label { AutoSize = true, Font = new Font("Segoe UI", 12), Text = _schedule.RestDaysLabel };
            var lbl3 = new Label { AutoSize = true, Font = SubtitleFont, Text = "Periodo Laboral:" };
            var val3 = new Label { AutoSize = true, Font = new Font("Segoe UI", 12), Text = _schedule.PeriodLabel };
            var stack = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true };
            stack.Controls.Add(lbl1); stack.Controls.Add(val1);
            stack.Controls.Add(new Label { Height = 8, AutoSize = false, Width = 1 });
            stack.Controls.Add(lbl2); stack.Controls.Add(val2);
            stack.Controls.Add(new Label { Height = 8, AutoSize = false, Width = 1 });
            stack.Controls.Add(lbl3); stack.Controls.Add(val3);
            cardTurno.Controls.Add(stack);
            rightColumn.Controls.Add(cardTurno, 0, 1);
        }
    }
}
