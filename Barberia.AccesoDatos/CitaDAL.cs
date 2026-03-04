using System;
using System.Collections.Generic;
using Barberia.Entidades;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class CitaDAL
    {
        ConexionBD conexion = new ConexionBD();

        public void Insertar(Cita c)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string q = @"INSERT INTO citas
                (id_cliente,id_barbero,id_servicio,fecha,hora,estado)
                VALUES (@cl,@ba,@se,@fe,@ho,@es)";

                SqlCommand cmd = new SqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@cl", c.IdCliente);
                cmd.Parameters.AddWithValue("@ba", c.IdBarbero);
                cmd.Parameters.AddWithValue("@se", c.IdServicio);
                cmd.Parameters.AddWithValue("@fe", c.Fecha);
                cmd.Parameters.AddWithValue("@ho", c.Hora);
                cmd.Parameters.AddWithValue("@es", c.Estado);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Cita> ObtenerPorBarbero(int idBarbero)
        {
            List<Cita> lista = new List<Cita>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM citas WHERE id_barbero=@id", conn);
                cmd.Parameters.AddWithValue("@id", idBarbero);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cita c = new Cita();
                    c.IdCita = (int)reader["id_cita"];
                    c.IdCliente = (int)reader["id_cliente"];
                    c.IdBarbero = (int)reader["id_barbero"];
                    c.IdServicio = (int)reader["id_servicio"];
                    c.Fecha = (DateTime)reader["fecha"];
                    c.Hora = (TimeSpan)reader["hora"];
                    c.Estado = reader["estado"].ToString();

                    lista.Add(c);
                }
            }
            return lista;
        }
    }
}