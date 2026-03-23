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
                VALUES (@cl,@ba,@se,@fe,@ho,'PROGRAMADA')";

                SqlCommand cmd = new SqlCommand(q, conn);

                cmd.Parameters.AddWithValue("@cl", c.IdCliente);
                cmd.Parameters.AddWithValue("@ba", c.IdBarbero);
                cmd.Parameters.AddWithValue("@se", c.IdServicio);
                cmd.Parameters.AddWithValue("@fe", c.Fecha);
                cmd.Parameters.AddWithValue("@ho", c.Hora);

                cmd.ExecuteNonQuery();
            }
        }

        // 🔥 LISTAR CON NOMBRES (AQUÍ ESTABA EL ERROR)
        public List<Cita> Listar()
        {
            List<Cita> lista = new List<Cita>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"
                SELECT c.*, 
                       uc.nombre + ' ' + uc.apellido AS cliente,
                       ub.nombre + ' ' + ub.apellido AS barbero,
                       s.nombre AS servicio
                FROM citas c
                INNER JOIN clientes cl ON c.id_cliente = cl.id_cliente
                INNER JOIN usuarios uc ON cl.id_usuario = uc.id_usuario
                INNER JOIN barberos b ON c.id_barbero = b.id_barbero
                INNER JOIN usuarios ub ON b.id_usuario = ub.id_usuario
                INNER JOIN servicios s ON c.id_servicio = s.id_servicio";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cita c = Map(reader);

                    c.NombreCliente = reader["cliente"].ToString();
                    c.NombreBarbero = reader["barbero"].ToString();
                    c.NombreServicio = reader["servicio"].ToString();

                    lista.Add(c);
                }
            }

            return lista;
        }

        public List<Cita> ObtenerPorBarberoYFecha(int idBarbero, DateTime fecha)
        {
            List<Cita> lista = new List<Cita>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string query = @"SELECT * FROM citas 
                                 WHERE id_barbero=@id 
                                 AND CAST(fecha AS DATE)=@fecha";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", idBarbero);
                cmd.Parameters.AddWithValue("@fecha", fecha.Date);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(Map(reader));
                }
            }

            return lista;
        }

        public Cita ObtenerPorId(int id)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM citas WHERE id_cita=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    return Map(reader);
            }

            return null;
        }

        public void Cancelar(int id)
        {
            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE citas SET estado='CANCELADA' WHERE id_cita=@id", conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        private Cita Map(SqlDataReader reader)
        {
            return new Cita
            {
                IdCita = (int)reader["id_cita"],
                IdCliente = (int)reader["id_cliente"],
                IdBarbero = (int)reader["id_barbero"],
                IdServicio = (int)reader["id_servicio"],
                Fecha = (DateTime)reader["fecha"],
                Hora = (TimeSpan)reader["hora"],
                Estado = reader["estado"].ToString()
            };
        }
    }
}