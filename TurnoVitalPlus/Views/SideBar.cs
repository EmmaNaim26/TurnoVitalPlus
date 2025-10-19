using System;
using System.Drawing;
using System.Windows.Forms;

namespace TurnoVitalPlus.Vista
{
    public class Sidebar : Panel
    {
        public event Action? OnInicio;
        public event Action? OnHorario;
        public event Action? OnEspacios;
        public event Action? OnSolicitar;
        public event Action? OnGuia;
        public event Action? OnSoporte;
        public event Action? OnAyuda;

        public Sidebar()
        {
            Width = 220;
            Dock = DockStyle.Left;
            BackColor = Color.FromArgb(245, 245, 245);
            Padding = new Padding(12);

            var lblUser = new Label { Text = "Usuario", Font = new Font("Segoe UI", 14, FontStyle.Bold), Dock = DockStyle.Top, Height = 36 };
            Controls.Add(lblUser);

            Controls.Add(CreateNav("Inicio", () => OnInicio?.Invoke()));
            Controls.Add(CreateNav("Horario", () => OnHorario?.Invoke()));
            Controls.Add(CreateNav("Espacios", () => OnEspacios?.Invoke()));
            Controls.Add(CreateNav("Solicitar turno", () => OnSolicitar?.Invoke()));

            var btnCrear = new Button { Text = "Crear Horario Laboral", Height = 44, Dock = DockStyle.Bottom, BackColor = Color.FromArgb(70, 200, 120) };
            Controls.Add(btnCrear);

            var lnkGuia = new LinkLabel { Text = "Guía de uso", Dock = DockStyle.Bottom, Padding = new Padding(4) };
            lnkGuia.LinkClicked += (_, __) => OnGuia?.Invoke();
            Controls.Add(lnkGuia);

            var lnkSoporte = new LinkLabel { Text = "Soporte Técnico", Dock = DockStyle.Bottom, Padding = new Padding(4) };
            lnkSoporte.LinkClicked += (_, __) => OnSoporte?.Invoke();
            Controls.Add(lnkSoporte);

            var lnkAyuda = new LinkLabel { Text = "Ayuda", Dock = DockStyle.Bottom, Padding = new Padding(4) };
            lnkAyuda.LinkClicked += (_, __) => OnAyuda?.Invoke();
            Controls.Add(lnkAyuda);
        }

        private Control CreateNav(string text, Action onClick)
        {
            var lbl = new Label { Text = text, Font = new Font("Segoe UI", 12), Margin = new Padding(6), AutoSize = true };
            lbl.Click += (_, __) => onClick();
            return lbl;
        }
    }
}
