using System;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;

namespace TurnoVitalPlus.Vista
{
    public partial class LoginView : UserControl
    {
        private readonly RootController _controller;

        public LoginView(RootController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _controller.TryLogin(txtCurp.Text.Trim(), txtPassword.Text.Trim());
        }
    }
}
