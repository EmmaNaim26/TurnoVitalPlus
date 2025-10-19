using System.Collections.Generic;

namespace TurnoVitalPlus.Datos
{
    public interface ISpacesRepository
    {
        IEnumerable<string> GetAvailableSpaces();
        IEnumerable<string> GetAvailableRestDays(int userId);
    }
}
