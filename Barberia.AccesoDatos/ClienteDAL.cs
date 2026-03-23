using System;
using System.Collections.Generic;
using Barberia.Entidades;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class ClienteDAL
    {
        ConexionBD conexion = new ConexionBD();

        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"SELECT c.*, u.nombre, u.apellido, u.correo
                                 FROM clientes c
                                 INNER JOIN usuarios u ON c.id_usuario = u.id_usuario";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        IdCliente = (int)reader["id_cliente"],
                        IdUsuario = (int)reader["id_usuario"],
                        Telefono = reader["telefono"].ToString(),
                        FechaNacimiento = (DateTime)reader["fecha_nacimiento"],
                        Estado = (bool)reader["estado"],

                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Correo = reader["correo"].ToString()
                    });
                }
            }
            return lista;
        }

        public Cliente ObtenerPorId(int id)
        {
            Cliente c = null;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"SELECT c.*, u.nombre, u.apellido, u.correo, u.password
                                 FROM clientes c
                                 INNER JOIN usuarios u ON c.id_usuario = u.id_usuario
                                 WHERE c.id_cliente=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                var r = cmd.ExecuteReader();

                if (r.Read())
                {
                    c = new Cliente
                    {
                        IdCliente = (int)r["id_cliente"],
                        IdUsuario = (int)r["id_usuario"],
                        Telefono = r["telefono"].ToString(),
                        FechaNacimiento = (DateTime)r["fecha_nacimiento"],

                        Nombre = r["nombre"].ToString(),
                        Apellido = r["apellido"].ToString(),
                        Correo = r["correo"].ToString(),
                        Password = r["password"].ToString()
                    };
                }
            }
            return c;
        }

        public void Insertar(Cliente c)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"INSERT INTO clientes(id_usuario,telefono,fecha_nacimiento)
                                 VALUES(@id,@tel,@fecha)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", c.IdUsuario);
                cmd.Parameters.AddWithValue("@tel", c.Telefono);
                cmd.Parameters.AddWithValue("@fecha", c.FechaNacimiento);

                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Cliente c)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"UPDATE clientes
                                 SET telefono=@tel,
                                     fecha_nacimiento=@fecha
                                 WHERE id_cliente=@id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@tel", c.Telefono);
                cmd.Parameters.AddWithValue("@fecha", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@id", c.IdCliente);

                cmd.ExecuteNonQuery();
            }
        }

        public int ObtenerIdUsuario(int idCliente)
        {
            int idUsuario = 0;

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "SELECT id_usuario FROM clientes WHERE id_cliente=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", idCliente);

                idUsuario = (int)cmd.ExecuteScalar();
            }

            return idUsuario;
        }

        public void Eliminar(int id)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = "DELETE FROM clientes WHERE id_cliente=@id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}