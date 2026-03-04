using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class CitaBL
    {
        CitaDAL dal = new();

        public void Crear(Cita c)
        {
            if (c.Fecha < DateTime.Today)
                throw new Exception("No puedes agendar en fechas pasadas");

            dal.Insertar(c);
        }

        public List<Cita> ObtenerPorBarbero(int id)
            => dal.ObtenerPorBarbero(id);
    }
}
