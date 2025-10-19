using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;

namespace TurnoVitalPlus.Vista
{
    public class LoginView : ControlVista
    {
        private readonly RootController _root;
        private TextBox _txtCurp = null!;
        private TextBox _txtPass = null!;

        public LoginView(RootController root)
        {
            _root = root;
            Dock = DockStyle.Fill;
            BuildUi();
        }

        private void BuildUi()
        {
            var center = new Panel { Dock = DockStyle.Fill, Padding = new Padding(40) };
            var card = MakeCard();
            card.Width = 480;
            card.Anchor = AnchorStyles.Top;
            center.Controls.Add(card);
            Controls.Add(center);

            var title = new Label { Text = "Te damos la bienvenida a Turno Vital+", Font = TitleFont, AutoSize = true };
            card.Controls.Add(title);

            card.Controls.Add(new Label { Text = "Usuario (CURP):", Top = 70, Left = 8, AutoSize = true });
            _txtCurp = new TextBox { Top = 90, Left = 8, Width = 420 };
            card.Controls.Add(_txtCurp);

            card.Controls.Add(new Label { Text = "Contraseña:", Top = 132, Left = 8, AutoSize = true });
            _txtPass = new TextBox { Top = 152, Left = 8, Width = 420, UseSystemPasswordChar = true };
            card.Controls.Add(_txtPass);

            var btn = new Button { Text = "Iniciar Sesión", Top = 200, Left = 8, Width = 140, BackColor = Color.FromArgb(39, 174, 96) };
            btn.Click += (_, __) => _root.TryLogin(_txtCurp.Text.Trim(), _txtPass.Text);
            card.Controls.Add(btn);

            var lnk = new LinkLabel { Text = "¿No tienes cuenta? Regístrate", Top = 200, Left = 170, AutoSize = true };
            lnk.Click += (_, __) => _root.OpenRegister();
            card.Controls.Add(lnk);
        }
    }
}
