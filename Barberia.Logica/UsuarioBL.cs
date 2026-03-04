using Barberia.AccesoDatos;
using Barberia.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barberia.Logica
{
    public class UsuarioBL
    {
        UsuarioDAL dal = new UsuarioDAL();

        public void CrearUsuario(Usuario u)
        {
            if (string.IsNullOrWhiteSpace(u.Correo))
                throw new Exception("Correo requerido");

            dal.Insertar(u);
        }
    }
}
