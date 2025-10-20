using System;
using System.Windows.Forms;
using System.ComponentModel;
using TurnoVitalPlus.Controlador;

namespace TurnoVitalPlus.Vista
{
    public class MainForm : Form
    {
        private RootController? _root;
        private Panel _hostPanel = null!;
        private IContainer components = null!;

        public MainForm()
        {
            InitializeComponent();
        }

        // Método público para inyectar el controlador raíz
        public void SetRootController(RootController root)
        {
            _root = root;
        }

        // Muestra una vista (Control) dentro del panel host
        public void ShowView(Control view)
        {
            if (_hostPanel == null)
            {
                MessageBox.Show("Host panel no está inicializado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _hostPanel.SuspendLayout();
            _hostPanel.Controls.Clear();
            view.Dock = DockStyle.Fill;
            _hostPanel.Controls.Add(view);
            _hostPanel.ResumeLayout();
        }

        // Mensajes rápidos
        public void ShowToast(string message)
        {
            MessageBox.Show(message);
        }

        // Inicializa componentes (implementación simple para evitar dependencia del .Designer)
        private void InitializeComponent()
        {
            components = new Container();
            this.Text = "TurnoVital+";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new System.Drawing.Size(1100, 720);

            // Panel host que contiene las vistas dinámicas
            _hostPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Name = "hostPanel"
            };

            this.Controls.Add(_hostPanel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
