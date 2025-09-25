// Contratos mínimos de repositorio para futura persistencia.
// Hoy devuelven datos simulados (in-memory) que alimentan el modelo.
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using TurnoVitalPlus.Models;

namespace TurnoVitalPlus
{
    public interface IUserRepository
    {
        User? Authenticate(string username, string password);
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

    // Implementaciones temporales en memoria para prueba visual
    internal class InMemoryUserRepository : IUserRepository
    {
        public User? Authenticate(string username, string password)
        {
            if (username.Equals("usuario", StringComparison.OrdinalIgnoreCase) && password == "1234")
            {
                return User.Mock();
            }
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
            // Inserta una marca simple para visualizar el impacto del turno solicitado
            sched.Notes = $"Última solicitud aplicada: {request.Code} - {DateTime.Now:t}";
        }
    }

    internal class InMemorySpacesRepository : ISpacesRepository
    {
        public IEnumerable<string> GetAvailableSpaces() =>
            new List<string>(); // "Sin espacios disponibles" como en el boceto

        public IEnumerable<string> GetAvailableRestDays(Guid userId) =>
            new List<string> { }; // "Ya has asignado tus días de descanso"
    }

    public class RepositoryRegistry
    {
        // Aquí podrías cambiar a implementaciones reales que usen IConnectionFactory
        public IUserRepository Users { get; init; }
        public IScheduleRepository Schedules { get; init; }
        public ISpacesRepository Spaces { get; init; }

        private RepositoryRegistry(IUserRepository u, IScheduleRepository s, ISpacesRepository sp)
        { Users = u; Schedules = s; Spaces = sp; }

        public static RepositoryRegistry Build(IConnectionFactory factory)
        {
            // Sustituye por repos reales que usen 'factory' cuando conectes BD
            return new RepositoryRegistry(new InMemoryUserRepository(),
                                          new InMemoryScheduleRepository(),
                                          new InMemorySpacesRepository());
        }
    }
}
