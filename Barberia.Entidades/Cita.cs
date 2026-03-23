using System;

namespace Barberia.Entidades
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdCliente { get; set; }
        public int IdBarbero { get; set; }
        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string Estado { get; set; }

        // 🔥 PARA MOSTRAR EN VISTA
        public string NombreCliente { get; set; }
        public string NombreBarbero { get; set; }
        public string NombreServicio { get; set; }
    }
}