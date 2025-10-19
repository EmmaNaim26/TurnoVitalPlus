namespace TurnoVitalPlus.Modelo
{
    public class Schedule
    {
        public string[,] Grid { get; set; } = new string[4, 7];
        public string Zone { get; set; } = "Recepción del Área de Urgencias";
        public string ShiftLabel { get; set; } = "Matutino 7-19hrs";
        public string CurrentTurn { get; set; } = "Matutino";
        public string RestDaysLabel { get; set; } = "Jueves y Sábados";
        public string PeriodLabel { get; set; } = "Agosto-Diciembre 2025";
        public string Notes { get; set; } = "";
    }
}
