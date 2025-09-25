// Representa una solicitud de turno/descanso extraordinario desde la vista "Solicitar turno".
using System;

namespace TurnoVitalPlus.Models
{
    public class ShiftRequest
    {
        public string Code { get; set; } = "";
        public DateTime RequestedAt { get; set; } = DateTime.Now;
    }
}
