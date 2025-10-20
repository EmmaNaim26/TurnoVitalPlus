namespace TurnoVitalPlus.Datos
{
    public class RepositoryRegistry
    {
        public IUserRepository Users { get; }
        public IScheduleRepository Schedules { get; }
        public ISpacesRepository Spaces { get; }

        private RepositoryRegistry(IUserRepository users, IScheduleRepository schedules, ISpacesRepository spaces)
        {
            Users = users;
            Schedules = schedules;
            Spaces = spaces;
        }

        public static RepositoryRegistry Build(IConnectionFactory factory)
        {
            return new RepositoryRegistry(
                new MySqlUserRepository(factory),
                new MySqlScheduleRepository(factory),
                new MySqlSpacesRepository(factory)
            );
        }
    }
}
