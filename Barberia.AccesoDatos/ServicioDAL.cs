using System;
using System.Collections.Generic;
using Barberia.Entidades;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class ServicioDAL
    {
        ConexionBD conexion = new ConexionBD();

        public List<Servicio> ObtenerTodos()
        {
            List<Servicio> lista = new List<Servicio>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM servicios", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Servicio s = new Servicio();
                    s.IdServicio = (int)reader["id_servicio"];
                    s.Nombre = reader["nombre"].ToString();
                    s.Descripcion = reader["descripcion"].ToString();
                    s.Precio = (decimal)reader["precio"];
                    s.Estado = (bool)reader["estado"];

                    lista.Add(s);
                }
            }
            return lista;
        }
    }
}