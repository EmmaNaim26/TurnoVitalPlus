using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public class RegisterView : ControlVista
    {
        private readonly RootController _root;
        private TextBox _txtNombre = null!;
        private TextBox _txtCurp = null!;
        private TextBox _txtHospital = null!;
        private TextBox _txtPass = null!;
        private TextBox _txtConfirm = null!;

        public RegisterView(RootController root)
        {
            _root = root;
            Dock = DockStyle.Fill;
            BuildUi();
        }

        private void BuildUi()
        {
            var center = new Panel { Dock = DockStyle.Fill, Padding = new Padding(40) };
            var card = MakeCard();
            card.Width = 520;
            center.Controls.Add(card);
            Controls.Add(center);

            card.Controls.Add(new Label { Text = "Registro de Usuario", Font = TitleFont, AutoSize = true });

            int y = 60;
            card.Controls.Add(new Label { Text = "Nombre completo", Top = y, Left = 8, AutoSize = true });
            _txtNombre = new TextBox { Top = y + 20, Left = 8, Width = 480 };
            card.Controls.Add(_txtNombre);

            y += 60;
            card.Controls.Add(new Label { Text = "CURP", Top = y, Left = 8, AutoSize = true });
            _txtCurp = new TextBox { Top = y + 20, Left = 8, Width = 200 };
            card.Controls.Add(_txtCurp);

            card.Controls.Add(new Label { Text = "Hospital", Top = y, Left = 220, AutoSize = true });
            _txtHospital = new TextBox { Top = y + 20, Left = 220, Width = 268 };
            card.Controls.Add(_txtHospital);

            y += 80;
            card.Controls.Add(new Label { Text = "Contraseña", Top = y, Left = 8, AutoSize = true });
            _txtPass = new TextBox { Top = y + 20, Left = 8, Width = 240, UseSystemPasswordChar = true };
            card.Controls.Add(_txtPass);

            card.Controls.Add(new Label { Text = "Confirmar contraseña", Top = y, Left = 260, AutoSize = true });
            _txtConfirm = new TextBox { Top = y + 20, Left = 260, Width = 228, UseSystemPasswordChar = true };
            card.Controls.Add(_txtConfirm);

            var btn = new Button { Text = "Registrar", Top = y + 70, Left = 8, Width = 120, BackColor = Color.FromArgb(39, 174, 96) };
            btn.Click += (_, __) => Register();
            card.Controls.Add(btn);

            var btnCancel = new Button { Text = "Volver", Top = y + 70, Left = 140, Width = 120 };
            btnCancel.Click += (_, __) => _root.Logout();
            card.Controls.Add(btnCancel);
        }

        private void Register()
        {
            if (string.IsNullOrWhiteSpace(_txtNombre.Text) || string.IsNullOrWhiteSpace(_txtCurp.Text) || string.IsNullOrWhiteSpace(_txtPass.Text))
            {
                MessageBox.Show("Completa los campos requeridos.");
                return;
            }
            if (_txtPass.Text != _txtConfirm.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            var user = new User
            {
                FullName = _txtNombre.Text.Trim(),
                Curp = _txtCurp.Text.Trim(),
                Hospital = _txtHospital.Text.Trim(),
                Position = "pendiente"
            };

            var id = _root.ControllerRegistry.Users.CreateUser(user, _txtPass.Text); // We'll expose registry; below we adapt
            MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
            _root.Logout();
        }
    }
}
