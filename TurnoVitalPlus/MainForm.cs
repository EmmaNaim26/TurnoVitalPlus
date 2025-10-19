using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;

namespace TurnoVitalPlus
{
    public class MainForm : Form
    {
        private readonly Panel _host;
        private readonly Label _toast;
        public RootController Controller { get; private set; } = null!;

        public MainForm()
        {
            Text = "Turno Vital+";
            MinimumSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            // host panel donde se cargan las vistas (una sola ventana)
            _host = new Panel { Dock = DockStyle.Fill, BackColor = Color.WhiteSmoke };
            Controls.Add(_host);

            _toast = new Label
            {
                Visible = false,
                AutoSize = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Padding = new Padding(10, 6, 10, 6)
            };
            Controls.Add(_toast);

            Load += MainForm_Load;
            Resize += (_, __) => PositionToast();

            // inicializamos controlador y repositorios aquí (integración BD)
            var connString = Infrastructure.AppSettings.ConnectionString;
            var factory = new Datos.MySqlConnectionFactory(connString);
            var repoRegistry = Datos.RepositoryRegistry.Build(factory);
            Controller = new RootController(repoRegistry, this);
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            Controller.Start();
        }

        public void ShowView(Control view)
        {
            _host.SuspendLayout();
            _host.Controls.Clear();
            view.Dock = DockStyle.Fill;
            _host.Controls.Add(view);
            _host.ResumeLayout();
        }

        public void ShowToast(string message, int ms = 1800)
        {
            _toast.Text = message;
            _toast.Visible = true;
            PositionToast();
            var timer = new System.Windows.Forms.Timer { Interval = ms };
            timer.Tick += (_, __) =>
            {
                _toast.Visible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void PositionToast()
        {
            if (!_toast.Visible) return;
            _toast.Left = (ClientSize.Width - _toast.Width) / 2;
            _toast.Top = 12;
            _toast.BringToFront();
        }
    }
}
