// Barra lateral reutilizable para Menú principal, Horario, Espacios y Solicitar turno.
// Contiene vínculos de "Guía de uso", "Soporte Técnico" y "Ayuda".
using System;
using System.Drawing;
using System.Windows.Forms;
using TurnoVitalPlus.Controllers;

namespace TurnoVitalPlus.Views
{
    public class Sidebar : Panel
    {
        public event Action? OnInicio;
        public event Action? OnHorario;
        public event Action? OnEspacios;
        public event Action? OnSolicitar;

        public Sidebar()
        {
            Dock = DockStyle.Left;
            Width = 240;
            BackColor = Color.White;
            Padding = new Padding(16);

            // Encabezado con icono y "Usuario"
            var header = new FlowLayoutPanel { FlowDirection = FlowDirection.LeftToRight, AutoSize = true, WrapContents = false };
            var avatar = new Label { Text = "👤", AutoSize = true, Font = new Font("Segoe UI Emoji", 20, FontStyle.Regular) };
            var titulo = new Label { Text = "Usuario", AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), Margin = new Padding(10, 6, 0, 0) };
            header.Controls.Add(avatar); header.Controls.Add(titulo);

            // Botón "Crear Horario Laboral"
            var crear = new Button
            {
                Text = "Crear Horario\nLaboral",
                Width = 200,
                Height = 60,
                BackColor = Color.FromArgb(99, 205, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 10, 0, 20)
            };
            crear.FlatAppearance.BorderSize = 0;

            // Menú
            var lista = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false };
            ButtonItem(lista, "Inicio", () => OnInicio?.Invoke(), true);
            ButtonItem(lista, "Horario", () => OnHorario?.Invoke());
            ButtonItem(lista, "Espacios", () => OnEspacios?.Invoke());
            ButtonItem(lista, "Solicitar turno", () => OnSolicitar?.Invoke());

            // Enlaces
            var links = new FlowLayoutPanel { FlowDirection = FlowDirection.TopDown, AutoSize = true, WrapContents = false, Margin = new Padding(0, 20, 0, 0) };
            Link(links, "Guia de uso", HelpLinksController.OpenGuide);
            Link(links, "Soporte Tecnico", HelpLinksController.OpenSupport);
            Link(links, "Ayuda", HelpLinksController.OpenHelp);

            // Layout principal
            var column = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, AutoScroll = true, WrapContents = false };
            column.Controls.Add(header);
            column.Controls.Add(new Label { AutoSize = true, Height = 8 });
            column.Controls.Add(lista);
            column.Controls.Add(crear);
            column.Controls.Add(links);

            Controls.Add(column);
        }

        private void ButtonItem(FlowLayoutPanel parent, string text, Action click, bool selected = false)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = selected ? Color.FromArgb(39, 174, 96) : Color.Black,
                Font = new Font("Segoe UI", 12, selected ? FontStyle.Bold : FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Width = 200
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (_, __) => click();
            parent.Controls.Add(btn);
        }

        private void Link(FlowLayoutPanel parent, string text, Action click)
        {
            var link = new LinkLabel { Text = text, AutoSize = true };
            link.LinkClicked += (_, __) => click();
            parent.Controls.Add(link);
        }
    }
}
