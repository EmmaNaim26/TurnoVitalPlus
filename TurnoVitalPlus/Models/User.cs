namespace TurnoVitalPlus.Modelo
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Curp { get; set; } = "";
        public string Hospital { get; set; } = "";
        public string Position { get; set; } = "";
    }
}
