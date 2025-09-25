// Vista de inicio de sesión. Reproduce la Imagen 1 con campos Usuario/Contraseña y botón "Iniciar Sesion".
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;

namespace TurnoVitalPlus.Views
{
    public class LoginView : ControlView
    {
        private readonly RootController _root;

        public LoginView(RootController root) => _root = root;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            BackColor = Color.White;

            // Título superior centrado
            var titulo = new Label
            {
                Text = "Te damos la bienvenida a Turno Vital+",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true
            };

            // Tarjeta con inputs
            var card = MakeCard();
            card.Size = new Size(500, 200);
            var lblUsuario = new Label { Text = "Usuario:", AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            var txtUsuario = new TextBox { Width = 300, Font = new Font("Segoe UI", 11) };
            var lblPass = new Label { Text = "Contraseña", AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold), Top = 70 };
            var txtPass = new TextBox { Width = 300, Font = new Font("Segoe UI", 11), UseSystemPasswordChar = true, Top = 95 };
            var btn = MakePrimary("Iniciar Sesion");
            btn.Top = 140;

            // Acciones
            btn.Click += (_, __) => _root.TryLogin(txtUsuario.Text.Trim(), txtPass.Text);

            // Posicionamiento absoluto simple dentro de la card (responde por anchura via Anchor)
            lblUsuario.Left = 20; lblUsuario.Top = 20;
            txtUsuario.Left = 20; txtUsuario.Top = 45; txtUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPass.Left = 20;
            txtPass.Left = 20; txtPass.Anchor = txtUsuario.Anchor;
            btn.Left = 20;

            card.Controls.Add(lblUsuario);
            card.Controls.Add(txtUsuario);
            card.Controls.Add(lblPass);
            card.Controls.Add(txtPass);
            card.Controls.Add(btn);

            // Contenedor central flexible
            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 3 };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            layout.Controls.Add(new Panel()); // espacio superior
            var center = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false };
            center.Controls.Add(titulo);
            center.Controls.Add(new Panel { Height = 20, Width = 10 });
            center.Controls.Add(card);
            var centerHolder = new Panel { Dock = DockStyle.Fill };
            centerHolder.Controls.Add(center);
            center.Left = (centerHolder.Width - center.Width) / 2;
            center.Top = 0;
            centerHolder.Resize += (_, __) =>
            {
                center.Left = (centerHolder.Width - center.Width) / 2;
                center.Top = 0;
            };
            layout.Controls.Add(centerHolder, 0, 1);
            layout.Controls.Add(new Panel()); // espacio inferior

            Controls.Add(layout);
        }
    }
}
