using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public class RequestView : UserControl
    {
        private readonly RootController _root;
        private readonly TurnoVitalPlus.Datos.IScheduleRepository _repo;

        public RequestView(RootController root, TurnoVitalPlus.Datos.IScheduleRepository repo)
        {
            _root = root;
            _repo = repo;
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

            var title = new Label { Text = "Solicitar turno / descanso", Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true };
            content.Controls.Add(title);

            var txtDetails = new TextBox { Top = 60, Left = 16, Width = 600, Height = 120, Multiline = true };
            content.Controls.Add(txtDetails);

            var btnSend = new Button { Text = "Enviar solicitud", Top = 190, Left = 16 };
            btnSend.Click += (_, __) =>
            {
                if (_root.CurrentUser is null) { MessageBox.Show("No hay usuario autenticado."); return; }
                var req = new ShiftRequest { Code = "REQ_" + DateTime.Now.Ticks, RequestedAt = DateTime.Now, Details = txtDetails.Text.Trim() };
                _repo.ApplyShiftRequest(_root.CurrentUser.Id, req);
                MessageBox.Show("Solicitud enviada.");
                _root.Start();
            };
            content.Controls.Add(btnSend);
        }
    }
}
