using System.Drawing;
using System.Windows.Forms;

namespace TurnoVitalPlus.Vista
{
    public class ControlVista : UserControl
    {
        protected Font TitleFont => new Font("Segoe UI", 20, FontStyle.Bold);
        protected Font SubtitleFont => new Font("Segoe UI", 12, FontStyle.Bold);
        protected Color PanelGray => Color.FromArgb(230, 230, 230);

        protected Panel MakeCard()
        {
            return new Panel
            {
                BackColor = PanelGray,
                Padding = new Padding(12),
                Margin = new Padding(8),
                Dock = DockStyle.Top,
                MinimumSize = new Size(0, 120)
            };
        }

        protected Label MakeH1(string text)
        {
            return new Label { Text = text, Font = TitleFont, AutoSize = true, Padding = new Padding(4) };
        }
    }
}
