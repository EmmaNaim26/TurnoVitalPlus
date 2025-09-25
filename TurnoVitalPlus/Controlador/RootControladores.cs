// Controlador raíz: maneja sesión, navegación y mantiene el estado del usuario.
using System;
using TurnoVitalPlus.Models;
using TurnoVitalPlus.Views;

namespace TurnoVitalPlus.Controllers
{
    public class RootController
    {
        private readonly RepositoryRegistry _repos;
        private MainForm? _host;              // Contenedor (ventana única)
        public User? CurrentUser { get; private set; }

        public RootController(RepositoryRegistry repos) => _repos = repos;

        public void Bind(MainForm host) => _host = host;

        // Navega cargando un UserControl en el panel central del MainForm.
        private void Navigate(ControlView view)
        {
            _host?.ShowView(view);
        }

        // Carga la vista de Login como estado inicial.
        public void Start()
        {
            Navigate(new LoginView(this));
        }

        // Intenta autenticar y, si es correcto, muestra el Dashboard.
        public void TryLogin(string username, string password)
        {
            var user = _repos.Users.Authenticate(username, password);
            if (user == null)
            {
                _host?.ShowToast("Usuario o contraseña incorrectos.");
                return;
            }
            CurrentUser = user;
            Navigate(new DashboardView(this, _repos.Schedules.GetByUserId(user.Id)));
        }

        // Acciones de navegación disparadas desde el menú lateral.
        public void GoHome() => Navigate(new DashboardView(this, _repos.Schedules.GetByUserId(CurrentUser!.Id)));
        public void GoSchedule() => Navigate(new ScheduleView(this, _repos.Schedules.GetByUserId(CurrentUser!.Id)));
        public void GoSpaces() => Navigate(new SpacesView(this, _repos.Spaces, CurrentUser!.Id));
        public void GoRequests() => Navigate(new RequestView(this));

        // Aplica una solicitud de turno en el modelo y refresca las vistas correspondientes.
        public void ApplyShiftRequest(ShiftRequest req)
        {
            if (CurrentUser == null) return;
            _repos.Schedules.ApplyShiftRequest(CurrentUser.Id, req);
            _host?.ShowToast("Solicitud registrada (simulada).");
            GoHome(); // Regresa al Dashboard para ver el cambio reflejado
        }
    }
}
