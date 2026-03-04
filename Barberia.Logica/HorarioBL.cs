using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class HorarioBL
    {
        HorarioDAL dal = new();

        public List<Horario> ObtenerPorBarbero(int id)
            => dal.ObtenerPorBarbero(id);
    }
}
