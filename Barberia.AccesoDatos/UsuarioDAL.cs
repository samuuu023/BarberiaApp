using Barberia.Entidades;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class UsuarioDAL
    {
        ConexionBD conexion = new ConexionBD();

        public void Insertar(Usuario u)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"INSERT INTO usuarios
                (nombre,apellido,correo,password,rol)
                VALUES (@n,@a,@c,@p,@r)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", u.Nombre);
                cmd.Parameters.AddWithValue("@a", u.Apellido);
                cmd.Parameters.AddWithValue("@c", u.Correo);
                cmd.Parameters.AddWithValue("@p", u.Password);
                cmd.Parameters.AddWithValue("@r", u.Rol);

                cmd.ExecuteNonQuery();
            }
        }
    }
}