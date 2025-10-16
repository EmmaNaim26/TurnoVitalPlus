// Controlador raíz: maneja sesión, navegación y mantiene el estado del usuario.
using System;
using TurnoVitalPlus.Views;
using TurnoVitalPlus.Models;
using AppUser = TurnoVitalPlus.Models.User;   // <-- alias para evitar ambigüedad con Microsoft.VisualBasic.ApplicationServices.User

namespace TurnoVitalPlus.Controllers
{
    public class RootController
    {
        private readonly RepositoryRegistry _repos;
        private MainForm? _host;              // Contenedor (ventana única)
        public AppUser? CurrentUser { get; private set; }   // <-- nullable

        public RootController(RepositoryRegistry repos) => _repos = repos;

        public void Bind(MainForm host) => _host = host;

        private void Navigate(ControlView view) => _host?.ShowView(view);

        public void Start() => Navigate(new LoginView(this));

        public void TryLogin(string username, string password)
        {
            var user = _repos.Users.Authenticate(username, password);
            if (user is null)
            {
                _host?.ShowToast("Usuario o contraseña incorrectos.");
                return;
            }
            CurrentUser = user;
            Navigate(new DashboardView(this, _repos.Schedules.GetByUserId(user.Id)));
        }

        public void GoHome()
        {
            if (CurrentUser is null) return;
            Navigate(new DashboardView(this, _repos.Schedules.GetByUserId(CurrentUser.Id)));
        }

        public void GoSchedule()
        {
            if (CurrentUser is null) return;
            Navigate(new ScheduleView(this, _repos.Schedules.GetByUserId(CurrentUser.Id)));
        }

        public void GoSpaces()
        {
            if (CurrentUser is null) return;
            Navigate(new SpacesView(this, _repos.Spaces, CurrentUser.Id));
        }

        public void GoRequests() => Navigate(new RequestView(this));

        public void ApplyShiftRequest(ShiftRequest req)
        {
            if (CurrentUser is null) return;
            _repos.Schedules.ApplyShiftRequest(CurrentUser.Id, req);
            _host?.ShowToast("Solicitud registrada (simulada).");
            GoHome();
        }
    }
}
