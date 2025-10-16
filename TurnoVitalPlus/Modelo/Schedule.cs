// Representa el horario laboral y textos del panel derecho del Menú principal y vista Horario.
namespace TurnoVitalPlus.Models
{
    public class Schedule
    {
        // Matriz compacta para poblar la cuadrícula visual (etiquetas por bloque)
        public string[,] Grid { get; set; } = new string[4, 7]; // 4 bloques x 7 días
        public string Zone { get; set; } = "Recepción del Área de Urgencias";
        public string ShiftLabel { get; set; } = "Matutino 7-19hrs";
        public string CurrentTurn { get; set; } = "Matutino";
        public string RestDaysLabel { get; set; } = "Jueves y Sábados";
        public string PeriodLabel { get; set; } = "Agosto-Diciembre 2025";
        public string Notes { get; set; } = "";

        public static Schedule Mock()
        {
            var s = new Schedule();
            // Llena la cuadrícula como en el boceto (LABOR/RECESO/CHEQUEO)
            s.Grid = new string[4, 7]
            {
                {"LABOR","LABOR","LABOR","LABOR","LABOR","", "LABOR"},
                {"RECESO","LABOR","RECESO","DÍA DE DESCANSO\nCON DISPOSICIÓN\nANTE EMERGENCIA","RECESO","","LABOR"},
                {"LABOR","LABOR","LABOR","LABOR","LABOR","",""},
                {"CHEQUEO","CHEQUEO","","","CHEQUEO","","CHEQUEO"},
            };
            return s;
        }
    }
}
