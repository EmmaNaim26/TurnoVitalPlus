using System;
using TurnoVitalPlus.Datos;
using TurnoVitalPlus.Modelo;
using TurnoVitalPlus.Vista;

namespace TurnoVitalPlus.Controlador
{
    public class RootController
    {
        private readonly RepositoryRegistry _repos;
        private readonly MainForm _host;
        public User? CurrentUser { get; private set; }

        public RootController(RepositoryRegistry repos, MainForm host)
        {
            _repos = repos;
            _host = host;
        }

        public void Start()
        {
            _host.ShowView(new LoginView(this));
        }

        public void TryLogin(string curp, string password)
        {
            var user = _repos.Users.Authenticate(curp, password);
            if (user is null)
            {
                _host.ShowToast("Usuario o contraseña incorrectos");
                return;
            }

            CurrentUser = user;
            // ✅ Ahora Dashboard recibe RootController + Usuario actual
            var dashboard = new Dashboard(this, user);
            _host.ShowView(dashboard);
        }

        public void Logout()
        {
            CurrentUser = null;
            _host.ShowView(new LoginView(this));
        }

        public void GoToSchedule()
        {
            if (CurrentUser is null) return;
            var schedule = _repos.Schedules.GetByUserId(CurrentUser.Id);
            _host.ShowView(new ScheduleView(this, schedule));
        }

        public void GoToSpaces()
        {
            if (CurrentUser is null) return;
            _host.ShowView(new SpacesView(this, _repos.Spaces, CurrentUser.Id));
        }

        public void GoToRequest()
        {
            _host.ShowView(new RequestView(this, _repos.Schedules));
        }

        public void ApplyRequest(ShiftRequest req)
        {
            if (CurrentUser is null) return;
            _repos.Schedules.ApplyShiftRequest(CurrentUser.Id, req);
            _host.ShowToast("Solicitud aplicada");
            var dashboard = new Dashboard(this, CurrentUser);
            _host.ShowView(dashboard);
        }
    }
}
