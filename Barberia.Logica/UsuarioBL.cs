using Barberia.AccesoDatos;
using Barberia.Entidades;
using System;
using System.Collections.Generic;

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

        // LOGIN
        public Usuario Login(string correo, string password)
        {
            List<Usuario> lista = dal.ObtenerTodos();

            foreach (Usuario u in lista)
            {
                if (u.Correo == correo && u.Password == password)
                {
                    return u;
                }
            }

            return null;
        }

        // LISTAR USUARIOS
        public List<Usuario> ObtenerTodos()
        {
            return dal.ObtenerTodos();
        }

        // ELIMINAR USUARIO
        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }

        // EDITAR USUARIO
        public void Editar(Usuario u)
        {
            dal.Editar(u);
        }
    }
}