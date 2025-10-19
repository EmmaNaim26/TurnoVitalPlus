using System;

namespace TurnoVitalPlus.Modelo
{
    public class ShiftRequest
    {
        public string Code { get; set; } = "";
        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }
}
