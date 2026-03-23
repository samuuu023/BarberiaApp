using System;
using System.Collections.Generic;
using System.Linq;
using Barberia.AccesoDatos;
using Barberia.Entidades;

namespace Barberia.Logica
{
    public class CitaBL
    {
        CitaDAL dal = new();
        HorarioDAL horarioDAL = new();

        public void Crear(Cita c)
        {
            if (c.Fecha < DateTime.Today)
                throw new Exception("No puedes agendar en fechas pasadas");

            dal.Insertar(c);
        }

        public List<Cita> Listar()
        {
            return dal.Listar();
        }

        public void Cancelar(int id)
        {
            var cita = dal.ObtenerPorId(id);

            if (cita == null)
                throw new Exception("Cita no encontrada");

            DateTime fechaHora = cita.Fecha.Date + cita.Hora;

            if (DateTime.Now > fechaHora.AddHours(-2))
                throw new Exception("No puedes cancelar con menos de 2 horas");

            dal.Cancelar(id);
        }

        public List<string> ObtenerHorasDisponibles(int idBarbero, DateTime fecha)
        {
            var horarios = horarioDAL.ObtenerPorBarbero(idBarbero);
            var citas = dal.ObtenerPorBarberoYFecha(idBarbero, fecha);

            List<string> disponibles = new();

            string dia = fecha.DayOfWeek.ToString().ToLower();

            var horariosDelDia = horarios
                .Where(x => x.DiaSemana.ToLower() == dia && x.Estado)
                .ToList();

            if (horariosDelDia.Count == 0)
                return disponibles;

            foreach (var h in horariosDelDia)
            {
                var hora = h.HoraInicio;

                while (hora < h.HoraFin)
                {
                    bool ocupado = citas.Any(c =>
                        c.Hora == hora && c.Estado != "CANCELADA");

                    if (!ocupado)
                        disponibles.Add(hora.ToString(@"hh\:mm"));

                    hora = hora.Add(TimeSpan.FromMinutes(30));
                }
            }

            return disponibles;
        }
    }
}