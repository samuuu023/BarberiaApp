using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia.Entidades
{
    public class Barbero
    {
        public int IdBarbero { get; set; }
        public int IdUsuario { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
    }
}
