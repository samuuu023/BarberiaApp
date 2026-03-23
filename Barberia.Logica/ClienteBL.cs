using System.Collections.Generic;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class ClienteBL
    {
        ClienteDAL dal = new ClienteDAL();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public List<Cliente> Listar()
        {
            return dal.ObtenerTodos();
        }

        public Cliente Obtener(int id)
        {
            return dal.ObtenerPorId(id);
        }

        public void Crear(Cliente c)
        {
            Usuario u = new Usuario
            {
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Correo = c.Correo,
                Password = c.Password,
                Rol = "CLIENTE"
            };

            int idUsuario = usuarioDAL.InsertarYRetornarId(u);

            c.IdUsuario = idUsuario;

            dal.Insertar(c);
        }

        public void Editar(Cliente c)
        {
            Usuario u = new Usuario
            {
                IdUsuario = c.IdUsuario,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Correo = c.Correo,
                Password = c.Password,
                Rol = "CLIENTE"
            };

            usuarioDAL.Editar(u);
            dal.Editar(c);
        }

        public void Eliminar(int idCliente)
        {
            int idUsuario = dal.ObtenerIdUsuario(idCliente);

            dal.Eliminar(idCliente);
            usuarioDAL.Eliminar(idUsuario);
        }
    }
}