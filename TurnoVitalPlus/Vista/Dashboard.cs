using System;
using System.Windows.Forms;
using TurnoVitalPlus.Controlador;
using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Vista
{
    public partial class Dashboard : UserControl
    {
        private readonly RootController _controller;
        private readonly User _currentUser;

        public Dashboard(RootController controller, User user)
        {
            _controller = controller;
            _currentUser = user;
            InitializeComponent();

            // Ejemplo: mostrar nombre de usuario en un label si tienes uno
            if (lblUserName != null)
                lblUserName.Text = $"Bienvenido, {_currentUser.FullName}";
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            _controller.GoToSchedule();
        }

        private void btnSpaces_Click(object sender, EventArgs e)
        {
            _controller.GoToSpaces();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            _controller.GoToRequest();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _controller.Logout();
        }
    }
}
