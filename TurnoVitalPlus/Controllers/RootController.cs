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

        public void Start() => _host.ShowView(new LoginView(this));

        public void TryLogin(string curp, string password)
        {
            var user = _repos.Users.Authenticate(curp, password);
            if (user is null)
            {
                _host.ShowToast("Usuario o contraseña incorrectos");
                return;
            }
            CurrentUser = user;
            var schedule = _repos.Schedules.GetByUserId(user.Id);
            _host.ShowView(new DashboardView(this, schedule));
        }

        public void OpenRegister()
        {
            _host.ShowView(new RegisterView(this));
        }

        public void Logout()
        {
            CurrentUser = null;
            _host.ShowView(new LoginView(this));
        }

        public void GoToSchedule() => _host.ShowView(new ScheduleView(this, _repos.Schedules.GetByUserId(CurrentUser?.Id ?? 0)));
        public void GoToSpaces() => _host.ShowView(new SpacesView(this, _repos.Spaces, CurrentUser?.Id ?? 0));
        public void GoToRequest() => _host.ShowView(new RequestView(this, _repos.Schedules));
        public void ApplyRequest(ShiftRequest req)
        {
            if (CurrentUser is null) return;
            _repos.Schedules.ApplyShiftRequest(CurrentUser.Id, req);
            _host.ShowToast("Solicitud aplicada");
            _host.ShowView(new DashboardView(this, _repos.Schedules.GetByUserId(CurrentUser.Id)));
        }
    }
}
