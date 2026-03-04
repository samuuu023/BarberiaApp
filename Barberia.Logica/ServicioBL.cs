using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class ServicioBL
    {
        ServicioDAL dal = new();

        public List<Servicio> Listar()
            => dal.ObtenerTodos();
    }
}
