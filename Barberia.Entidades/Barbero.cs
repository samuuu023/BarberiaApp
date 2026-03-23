using System;

namespace Barberia.Entidades
{
    public class Barbero
    {
        public int IdBarbero { get; set; }
        public int IdUsuario { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }

        // 🔥 DATOS DEL USUARIO
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}