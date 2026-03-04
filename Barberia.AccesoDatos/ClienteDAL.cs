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

                SqlCommand cmd = new SqlCommand("SELECT * FROM clientes", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cliente c = new Cliente();
                    c.IdCliente = (int)reader["id_cliente"];
                    c.IdUsuario = (int)reader["id_usuario"];
                    c.Telefono = reader["telefono"].ToString();
                    c.FechaNacimiento = (DateTime)reader["fecha_nacimiento"];
                    c.Estado = (bool)reader["estado"];

                    lista.Add(c);
                }
            }
            return lista;
        }
    }
}