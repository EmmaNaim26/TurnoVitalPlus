using TurnoVitalPlus.Modelo;

namespace TurnoVitalPlus.Datos
{
    public interface IScheduleRepository
    {
        Schedule GetByUserId(int userId);
        void ApplyShiftRequest(int userId, ShiftRequest request);
    }
}
