using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proyecto.Controlador
{
    public class Controlador
    {
        // Puedes poner aquí toda la lógica que quieras reutilizar
        // Ejemplo: manejo de datos, validaciones, etc.

        public void MostrarMensaje(string texto)
        {
            MessageBox.Show(texto, "Mensaje del controlador", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ValidarTexto(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public List<string> ObtenerDatosEjemplo()
        {
            return new List<string> { "Dato 1", "Dato 2", "Dato 3" };
        }
    }
}
