// Clase base para todas las vistas. Reúne utilidades comunes para estilo y layout responsivo.
using System.Drawing;
using System.Windows.Forms;

namespace TurnoVitalPlus.Views
{
    public class ControlView : UserControl
    {
        protected Color Accent => Color.FromArgb(39, 174, 96);        // Verde del botón "Iniciar Sesión"
        protected Color PanelGray => Color.FromArgb(230, 230, 230);   // Gris de tarjetas
        protected Font TitleFont => new Font("Segoe UI", 18, FontStyle.Bold);
        protected Font SubtitleFont => new Font("Segoe UI", 12, FontStyle.Bold);

        protected Panel MakeCard() => new Panel
        {
            BackColor = PanelGray,
            Padding = new Padding(18),
            Margin = new Padding(10),
            BorderStyle = BorderStyle.None
        };

        protected Label MakeH1(string text) => new Label
        {
            Text = text,
            AutoSize = true,
            Font = TitleFont,
            Margin = new Padding(0, 0, 0, 12)
        };

        protected Button MakePrimary(string text)
        {
            var b = new Button
            {
                Text = text,
                AutoSize = true,
                Padding = new Padding(18, 10, 18, 10),
                FlatStyle = FlatStyle.Flat,
                BackColor = Accent,
                ForeColor = Color.White
            };
            b.FlatAppearance.BorderSize = 0;
            return b;
        }
    }
}
