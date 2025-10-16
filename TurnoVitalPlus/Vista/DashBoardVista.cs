// Views/DashboardView.cs
// Vista del Menú principal totalmente responsive: dos columnas (izq: datos usuario/hospital; der: calendario + turno)


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
            DoubleBuffered = true;  // evita flicker al redimensionar
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // === Barra lateral fija a la izquierda ===
            var sidebar = new Sidebar();
            sidebar.OnInicio += _root.GoHome;
            sidebar.OnHorario += _root.GoSchedule;
            sidebar.OnEspacios += _root.GoSpaces;
            sidebar.OnSolicitar += _root.GoRequests;
            Controls.Add(sidebar);

            // === Superficie derecha: dos columnas fluidas ===
            var surface = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(12),
                BackColor = Color.White
            };
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64f)); // izquierda
            surface.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36f)); // derecha
            Controls.Add(surface);
            surface.BringToFront(); // que quede por encima del sidebar

            // ====== COLUMNA IZQUIERDA: Datos Usuario (arriba) + Datos Hospital (abajo) ======
            var colLeft = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2
            };
            colLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 55f));
            colLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 45f));
            surface.Controls.Add(colLeft, 0, 0);

            // --- Card: Datos del Usuario ---
            var cardUsuario = MakeCard();
            cardUsuario.Dock = DockStyle.Fill;

            var headerUsuario = MakeH1("Datos del Usuario");
            headerUsuario.Dock = DockStyle.Top;
            cardUsuario.Controls.Add(headerUsuario);

            // Contenido: dos columnas (avatar fijo + info elástica)
            var userGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Margin = new Padding(0, 8, 0, 0)
            };
            userGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110)); // avatar fijo
            userGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));  // info
            cardUsuario.Controls.Add(userGrid);

            // Avatar circular simple
            var avatarPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            avatarPanel.Paint += (_, e) =>
            {
                var r = new Rectangle(20, 10, 70, 70);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var b = new SolidBrush(Color.FromArgb(160, 160, 160));
                e.Graphics.FillEllipse(b, r);
            };
            userGrid.Controls.Add(avatarPanel, 0, 0);

            var u = _root.CurrentUser!;
            var infoUsuario = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                Font = new Font("Segoe UI", 10),
                Text = $"Nombre Completo\n{u.FullName}\n\n" +
                       $"CURP\n{u.Curp}\n\n" +
                       $"Fecha de Nacimiento\n{u.BirthDate:dd/MM/yyyy}\n\n" +
                       $"Matricula\n{u.Matricula}\n\n" +
                       $"Correo electronico\n{u.Email}"
            };
            userGrid.Controls.Add(infoUsuario, 1, 0);

            colLeft.Controls.Add(cardUsuario, 0, 0);

            // --- Card: Datos del Hospital ---
            var cardHospital = MakeCard();
            cardHospital.Dock = DockStyle.Fill;

            var headerHospital = MakeH1("Datos del Hospital");
            headerHospital.Dock = DockStyle.Top;
            cardHospital.Controls.Add(headerHospital);

            var infoHospital = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = false,
                Font = new Font("Segoe UI", 10),
                Text = "Hospital General “Juan Blanco Alarcon”\n" +
                       "Av. Universidad 6142, Acapulco GRO\n" +
                       "Turno: Matutino/Vespertino/Nocturno\n" +
                       "Ingreso: Marzo 2022"
            };
            cardHospital.Controls.Add(infoHospital);

            colLeft.Controls.Add(cardHospital, 0, 1);

            // ====== COLUMNA DERECHA: Calendario (arriba) + Resumen turno (abajo) ======
            var colRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2
            };
            colRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            colRight.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            surface.Controls.Add(colRight, 1, 0);

            // --- Card: Calendario ---
            var cardCalendar = MakeCard();
            cardCalendar.Dock = DockStyle.Fill;

            var headerCal = new Label
            {
                Text = $"{DateTime.Now:MMMM}".ToUpper() + $"     {DateTime.Now:yyyy}",
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Top
            };
            cardCalendar.Controls.Add(headerCal);

            var calendar = new MonthCalendar
            {
                MaxSelectionCount = 1,
                ShowToday = false,
                Dock = DockStyle.Top,
                Margin = new Padding(8)
            };
            // contenedor para permitir que el calendario “se ancle” arriba y deje espacio libre abajo
            var calHolder = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            calHolder.Controls.Add(calendar);
            cardCalendar.Controls.Add(calHolder);

            colRight.Controls.Add(cardCalendar, 0, 0);

            // --- Card: Resumen de turno ---
            var cardTurno = MakeCard();
            cardTurno.Dock = DockStyle.Fill;

            var stackTurno = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 6
            };
            // filas automáticas (texto) y separadores
            stackTurno.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stackTurno.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stackTurno.RowStyles.Add(new RowStyle(SizeType.Absolute, 8));
            stackTurno.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stackTurno.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            stackTurno.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            cardTurno.Controls.Add(stackTurno);

            var lblTuTurno = new Label { Text = "Tu turno:", AutoSize = true, Font = SubtitleFont };
            var valTuTurno = new Label { Text = _schedule.CurrentTurn, AutoSize = true, Font = new Font("Segoe UI", 12) };

            var sep = new Panel { Height = 8, Dock = DockStyle.Top };

            var lblDescanso = new Label { Text = "Dias de descanso:", AutoSize = true, Font = SubtitleFont };
            var valDescanso = new Label { Text = _schedule.RestDaysLabel, AutoSize = true, Font = new Font("Segoe UI", 12) };

            stackTurno.Controls.Add(lblTuTurno, 0, 0);
            stackTurno.Controls.Add(valTuTurno, 0, 1);
            stackTurno.Controls.Add(sep, 0, 2);
            stackTurno.Controls.Add(lblDescanso, 0, 3);
            stackTurno.Controls.Add(valDescanso, 0, 4);

            colRight.Controls.Add(cardTurno, 0, 1);
        }
    }
}
