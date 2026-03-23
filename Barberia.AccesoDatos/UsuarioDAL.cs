using Barberia.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class UsuarioDAL
    {
        ConexionBD conexion = new ConexionBD();

        // 🔹 INSERT
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

        // 🔹 INSERT Y RETORNAR ID
        public int InsertarYRetornarId(Usuario u)
        {
            int id = 0;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"INSERT INTO usuarios
                (nombre,apellido,correo,password,rol)
                VALUES (@n,@a,@c,@p,@r);
                SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@n", u.Nombre);
                cmd.Parameters.AddWithValue("@a", u.Apellido);
                cmd.Parameters.AddWithValue("@c", u.Correo);
                cmd.Parameters.AddWithValue("@p", u.Password);
                cmd.Parameters.AddWithValue("@r", u.Rol);

                id = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return id;
        }

        // 🔹 LOGIN
        public Usuario Login(string correo, string password)
        {
            Usuario u = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "SELECT * FROM usuarios WHERE correo=@c AND password=@p";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c", correo);
                cmd.Parameters.AddWithValue("@p", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    u = new Usuario
                    {
                        IdUsuario = (int)reader["id_usuario"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Correo = reader["correo"].ToString(),
                        Password = reader["password"].ToString(),
                        Rol = reader["rol"].ToString()
                    };
                }
            }

            return u;
        }

        // 🔹 OBTENER POR CORREO
        public Usuario ObtenerPorCorreo(string correo)
        {
            Usuario u = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "SELECT * FROM usuarios WHERE correo=@c";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@c", correo);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    u = new Usuario
                    {
                        IdUsuario = (int)reader["id_usuario"],
                        Nombre = reader["nombre"].ToString(),
                        Correo = reader["correo"].ToString()
                    };
                }
            }

            return u;
        }

        // 🔹 GUARDAR TOKEN
        public void GuardarToken(int id, string token, DateTime expira)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"UPDATE usuarios 
                                 SET token_recuperacion=@t, token_expira=@e
                                 WHERE id_usuario=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@t", token);
                cmd.Parameters.AddWithValue("@e", expira);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 OBTENER POR TOKEN
        public Usuario ObtenerPorToken(string token)
        {
            Usuario u = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"SELECT * FROM usuarios 
                                 WHERE token_recuperacion=@t 
                                 AND token_expira > GETDATE()";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@t", token);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    u = new Usuario
                    {
                        IdUsuario = (int)reader["id_usuario"],
                        Correo = reader["correo"].ToString()
                    };
                }
            }

            return u;
        }

        // 🔹 CAMBIAR PASSWORD
        public void CambiarPassword(int id, string nuevaPass)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"UPDATE usuarios 
                                 SET password=@p,
                                     token_recuperacion=NULL,
                                     token_expira=NULL
                                 WHERE id_usuario=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@p", nuevaPass);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔥 LISTAR TODOS
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

        // 🔥 ELIMINAR
        public void Eliminar(int id)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "DELETE FROM usuarios WHERE id_usuario=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔥 EDITAR
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