namespace TurnoVitalPlus.Modelo
{
    public class Schedule
    {
        public string Zone { get; set; } = "";
        public string ShiftLabel { get; set; } = "";
        public string CurrentTurn { get; set; } = "";
        public string RestDaysLabel { get; set; } = "";
        public string PeriodLabel { get; set; } = "";
        public string Notes { get; set; } = "";
        public string[,] Grid { get; set; } = new string[4, 7];
    }
}
