using Barberia.Entidades;
using System.Collections.Generic;
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

        public List<Usuario> ObtenerTodos()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "SELECT * FROM usuarios";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario u = new Usuario();

                    u.IdUsuario = (int)reader["id_usuario"];
                    u.Nombre = reader["nombre"].ToString();
                    u.Apellido = reader["apellido"].ToString();
                    u.Correo = reader["correo"].ToString();
                    u.Password = reader["password"].ToString();
                    u.Rol = reader["rol"].ToString();

                    lista.Add(u);
                }
            }

            return lista;
        }

        // METODO ELIMINAR
        public void Eliminar(int id)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "DELETE FROM usuarios WHERE id_usuario = @id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
       
        }
        public void Editar(Usuario u)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"UPDATE usuarios
                        SET nombre=@n,
                            apellido=@a,
                            correo=@c,
                            password=@p,
                            rol=@r
                        WHERE id_usuario=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", u.Nombre);
                cmd.Parameters.AddWithValue("@a", u.Apellido);
                cmd.Parameters.AddWithValue("@c", u.Correo);
                cmd.Parameters.AddWithValue("@p", u.Password);
                cmd.Parameters.AddWithValue("@r", u.Rol);
                cmd.Parameters.AddWithValue("@id", u.IdUsuario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}