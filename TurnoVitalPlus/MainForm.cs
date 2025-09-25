// Ventana única de la aplicación. Contiene un panel donde se inyectan las vistas.
// También aloja un "toast" simple para mensajes y responsabiliza el redimensionamiento fluido.
using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;

namespace TurnoVitalPlus
{
    public class MainForm : Form
    {
        private readonly RootController _root;
        private readonly Panel _host;     // Contenedor central de vistas
        private readonly Label _toast;    // Mensajes flotantes

        public MainForm(RootController root)
        {
            _root = root;

            // Configura ventana principal
            Text = "Turno Vital+";
            MinimumSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            // Panel host con Dock=Fill para ocupar todo el área
            _host = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            Controls.Add(_host);

            // Toast para retroalimentación no intrusiva
            _toast = new Label
            {
                Visible = false,
                AutoSize = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Padding = new Padding(12, 8, 12, 8)
            };
            Controls.Add(_toast);

            // Evento de carga: el controlador arranca la app (muestra Login)
            Load += (_, __) => _root.Start();

            // Reposiciona el toast cuando la ventana cambia de tamaño
            Resize += (_, __) => PositionToast();
        }

        // Inyecta un UserControl al contenedor, asegurando una sola vista visible.
        public void ShowView(Control view)
        {
            _host.SuspendLayout();
            _host.Controls.Clear();
            view.Dock = DockStyle.Fill;
            _host.Controls.Add(view);
            _host.ResumeLayout();
        }

        // Muestra un mensaje temporal
        public void ShowToast(string message, int ms = 1800)
        {
            _toast.Text = message;
            _toast.Visible = true;
            PositionToast();

            var timer = new Timer { Interval = ms };
            timer.Tick += (_, __) => { _toast.Visible = false; timer.Stop(); timer.Dispose(); };
            timer.Start();
        }

        private void PositionToast()
        {
            if (!_toast.Visible) return;
            _toast.Left = (ClientSize.Width - _toast.Width) / 2;
            _toast.Top = 16;
            _toast.BringToFront();
        }
    }
}
