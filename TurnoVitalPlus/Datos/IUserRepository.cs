using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Datos
{
    public interface IUserRepository
    {
        User? Authenticate(string curp, string password);
        int CreateUser(User user, string password);
        User? GetById(int id);
        User? GetByCurp(string curp);
    }
}
