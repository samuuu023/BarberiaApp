using System.Collections.Generic;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class BarberoBL
    {
        BarberoDAL dal = new BarberoDAL();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public List<Barbero> Listar()
        {
            return dal.ObtenerTodos();
        }

        public Barbero Obtener(int id)
        {
            return dal.ObtenerPorId(id);
        }

        public void CrearBarbero(Barbero b)
        {
            Usuario u = new Usuario
            {
                Nombre = b.Nombre,
                Apellido = b.Apellido,
                Correo = b.Correo,
                Password = b.Password,
                Rol = "BARBERO"
            };

            int idUsuario = usuarioDAL.InsertarYRetornarId(u);

            b.IdUsuario = idUsuario;

            dal.Insertar(b);
        }

        public void Editar(Barbero b)
        {
            Usuario u = new Usuario
            {
                IdUsuario = b.IdUsuario,
                Nombre = b.Nombre,
                Apellido = b.Apellido,
                Correo = b.Correo,
                Password = b.Password,
                Rol = "BARBERO"
            };

            usuarioDAL.Editar(u);
            dal.Editar(b);
        }

        public void Eliminar(int idBarbero)
        {
            int idUsuario = dal.ObtenerIdUsuario(idBarbero);

            dal.Eliminar(idBarbero);
            usuarioDAL.Eliminar(idUsuario);
        }
    }
}