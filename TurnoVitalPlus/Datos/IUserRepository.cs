using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Datos
{
    public interface IUserRepository
    {
        User? Authenticate(string curp, string password);
        User? GetById(int id);
    }
}
