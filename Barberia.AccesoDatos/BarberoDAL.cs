using System;
using System.Collections.Generic;
using Barberia.Entidades;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class BarberoDAL
    {
        ConexionBD conexion = new ConexionBD();

        public List<Barbero> ObtenerTodos()
        {
            List<Barbero> lista = new List<Barbero>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"SELECT b.id_barbero, b.id_usuario, b.telefono, b.estado,
                                u.nombre, u.apellido, u.correo
                                FROM barberos b
                                INNER JOIN usuarios u ON b.id_usuario = u.id_usuario";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Barbero
                    {
                        IdBarbero = (int)reader["id_barbero"],
                        IdUsuario = (int)reader["id_usuario"],
                        Telefono = reader["telefono"].ToString(),
                        Estado = (bool)reader["estado"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Correo = reader["correo"].ToString()
                    });
                }
            }
            return lista;
        }

        public Barbero ObtenerPorId(int id)
        {
            Barbero b = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"SELECT b.id_barbero, b.id_usuario, b.telefono,
                                u.nombre, u.apellido, u.correo, u.password
                                FROM barberos b
                                INNER JOIN usuarios u ON b.id_usuario = u.id_usuario
                                WHERE b.id_barbero=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    b = new Barbero
                    {
                        IdBarbero = (int)r["id_barbero"],
                        IdUsuario = (int)r["id_usuario"],
                        Telefono = r["telefono"].ToString(),
                        Nombre = r["nombre"].ToString(),
                        Apellido = r["apellido"].ToString(),
                        Correo = r["correo"].ToString(),
                        Password = r["password"].ToString()
                    };
                }
            }
            return b;
        }

        public void Insertar(Barbero b)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "INSERT INTO barberos(id_usuario,telefono) VALUES(@id,@tel)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", b.IdUsuario);
                cmd.Parameters.AddWithValue("@tel", b.Telefono);

                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Barbero b)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "UPDATE barberos SET telefono=@tel WHERE id_barbero=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tel", b.Telefono);
                cmd.Parameters.AddWithValue("@id", b.IdBarbero);

                cmd.ExecuteNonQuery();
            }
        }

        public int ObtenerIdUsuario(int idBarbero)
        {
            int idUsuario = 0;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "SELECT id_usuario FROM barberos WHERE id_barbero=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", idBarbero);

                idUsuario = (int)cmd.ExecuteScalar();
            }

            return idUsuario;
        }

        public void Eliminar(int idBarbero)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "DELETE FROM barberos WHERE id_barbero=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", idBarbero);
                cmd.ExecuteNonQuery();
            }
        }
    }
}