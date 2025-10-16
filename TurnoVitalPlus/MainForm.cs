using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;

namespace TurnoVitalPlus
{
    public class MainForm : Form
    {
        private readonly RootController _root;
        private readonly Panel _host;
        private readonly Label _toast;

        public MainForm(RootController root)
        {
            _root = root;
            Text = "Turno Vital+";
            MinimumSize = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            _host = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            Controls.Add(_host);

            _toast = new Label
            {
                Visible = false,
                AutoSize = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.White,
                Padding = new Padding(12, 8, 12, 8)
            };
            Controls.Add(_toast);

            Load += (_, __) => _root.Start();
            Resize += (_, __) => PositionToast();
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

            // <-- Usa el Timer de WinForms para evitar ambigüedad con System.Threading.Timer
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
            _toast.Top = 16;
            _toast.BringToFront();
        }
    }
}
