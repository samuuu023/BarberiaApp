using System;
using System.Collections.Generic;
using Barberia.Entidades;
using System.Data.SqlClient;

namespace Barberia.AccesoDatos
{
    public class HorarioDAL
    {
        ConexionBD conexion = new ConexionBD();

        public List<Horario> ObtenerPorBarbero(int idBarbero)
        {
            List<Horario> lista = new List<Horario>();

            using (SqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                string q = "SELECT * FROM horarios WHERE id_barbero=@id";
                SqlCommand cmd = new SqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@id", idBarbero);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Horario h = new Horario();
                    h.IdHorario = (int)reader["id_horario"];
                    h.IdBarbero = (int)reader["id_barbero"];
                    h.DiaSemana = reader["dia_semana"].ToString();
                    h.HoraInicio = (TimeSpan)reader["hora_inicio"];
                    h.HoraFin = (TimeSpan)reader["hora_fin"];
                    h.Estado = (bool)reader["estado"];

                    lista.Add(h);
                }
            }
            return lista;
        }
    }
}