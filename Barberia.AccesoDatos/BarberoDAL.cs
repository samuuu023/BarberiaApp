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

                SqlCommand cmd = new SqlCommand("SELECT * FROM barberos", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Barbero b = new Barbero();
                    b.IdBarbero = (int)reader["id_barbero"];
                    b.IdUsuario = (int)reader["id_usuario"];
                    b.Telefono = reader["telefono"].ToString();
                    b.Estado = (bool)reader["estado"];

                    lista.Add(b);
                }
            }
            return lista;
        }
    }
}