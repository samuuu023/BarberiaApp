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

        public int CrearUsuarioYRetornarId(Usuario u)
        {
            return dal.InsertarYRetornarId(u);
        }

        public Usuario Login(string correo, string password)
        {
            return dal.Login(correo, password);
        }

        public Usuario ObtenerPorCorreo(string correo)
        {
            return dal.ObtenerPorCorreo(correo);
        }

        public void GuardarToken(int id, string token, DateTime expira)
        {
            dal.GuardarToken(id, token, expira);
        }

        public Usuario ObtenerPorToken(string token)
        {
            return dal.ObtenerPorToken(token);
        }

        public void CambiarPassword(int id, string nuevaPass)
        {
            dal.CambiarPassword(id, nuevaPass);
        }

        public List<Usuario> ObtenerTodos()
        {
            return dal.ObtenerTodos();
        }

        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }

        public void Editar(Usuario u)
        {
            dal.Editar(u);
        }
    }
}