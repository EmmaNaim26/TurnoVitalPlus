using System;

namespace TurnoVitalPlus.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = "Nombre Completo";
        public string Curp { get; set; } = "CURPXXXX000000";
        public DateTime BirthDate { get; set; } = new DateTime(2000, 1, 1);
        public string Matricula { get; set; } = "000000";
        public string Email { get; set; } = "correo@ejemplo.com";

        public static User Mock() => new User
        {
            FullName = "Alex Rivera",
            Curp = "RIVA000101HDFXXX01",
            BirthDate = new DateTime(2000, 01, 01),
            Matricula = "A123456",
            Email = "alex@hospital.mx"
        };
    }
}
