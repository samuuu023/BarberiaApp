using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class ClienteBL
    {
        ClienteDAL dal = new();

        public List<Cliente> Listar()
            => dal.ObtenerTodos();
    }
}
