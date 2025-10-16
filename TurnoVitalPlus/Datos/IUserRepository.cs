using System;
using System.Collections.Generic;
using TurnoVitalPlus.Models;
using AppUser = TurnoVitalPlus.Models.User;   // alias para evitar ambigüedad

namespace TurnoVitalPlus
{
    public interface IUserRepository
    {
        AppUser? Authenticate(string username, string password);
    }

    public interface IScheduleRepository
    {
        Schedule GetByUserId(Guid userId);
        void ApplyShiftRequest(Guid userId, ShiftRequest request);
    }

    public interface ISpacesRepository
    {
        IEnumerable<string> GetAvailableSpaces();
        IEnumerable<string> GetAvailableRestDays(Guid userId);
    }

    // Implementaciones en memoria (mock) para probar la UI
    internal class InMemoryUserRepository : IUserRepository
    {
        public AppUser? Authenticate(string username, string password)
        {
            if (username.Equals("usuario", StringComparison.OrdinalIgnoreCase) && password == "1234")
                return AppUser.Mock();
            return null;
        }
    }

    internal class InMemoryScheduleRepository : IScheduleRepository
    {
        private readonly Dictionary<Guid, Schedule> _store = new();

        public Schedule GetByUserId(Guid userId)
        {
            if (!_store.ContainsKey(userId)) _store[userId] = Schedule.Mock();
            return _store[userId];
        }

        public void ApplyShiftRequest(Guid userId, ShiftRequest request)
        {
            var sched = GetByUserId(userId);
            sched.Notes = $"Última solicitud aplicada: {request.Code} - {DateTime.Now:t}";
        }
    }

    internal class InMemorySpacesRepository : ISpacesRepository
    {
        public IEnumerable<string> GetAvailableSpaces() => new List<string>(); // "Sin espacios disponibles"
        public IEnumerable<string> GetAvailableRestDays(Guid userId) => new List<string>(); // "Ya asignaste tus días"
    }

    public class RepositoryRegistry
    {
        public IUserRepository Users { get; init; }
        public IScheduleRepository Schedules { get; init; }
        public ISpacesRepository Spaces { get; init; }

        private RepositoryRegistry(IUserRepository u, IScheduleRepository s, ISpacesRepository sp)
        { Users = u; Schedules = s; Spaces = sp; }

        public static RepositoryRegistry Build(IConnectionFactory factory)
        {
            // Cuando conectes a BD, cambia estas implementaciones por repos SQL reales
            return new RepositoryRegistry(new InMemoryUserRepository(),
                                          new InMemoryScheduleRepository(),
                                          new InMemorySpacesRepository());
        }
    }
}
