namespace TurnoVitalPlus.Datos
{
    public class RepositoryRegistry
    {
        public IUserRepository Users { get; init; }
        public IScheduleRepository Schedules { get; init; }
        public ISpacesRepository Spaces { get; init; }

        public RepositoryRegistry(IUserRepository u, IScheduleRepository s, ISpacesRepository sp)
        {
            Users = u; Schedules = s; Spaces = sp;
        }

        public static RepositoryRegistry Build(IConnectionFactory factory)
        {
            // Devuelve repositorios MySQL por defecto (ya integrados)
            return new RepositoryRegistry(
                new MySqlUserRepository(factory),
                new MySqlScheduleRepository(factory),
                new MySqlSpacesRepository(factory)
            );
        }
    }
}
