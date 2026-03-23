using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;
using System.Linq;
using Barberia.IUMVC.Filters;
using System;
using System.Collections.Generic;

namespace Barberia.IUMVC.Controllers
{
    [AuthorizeRole("ADMIN", "CLIENTE", "BARBERO")]
    public class CitasController : Controller
    {
        CitaBL citaBL = new();
        ClienteBL clienteBL = new();
        BarberoBL barberoBL = new();
        ServicioBL servicioBL = new();

        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("rol");
            var idUsuario = HttpContext.Session.GetInt32("id_usuario");

            var lista = citaBL.Listar();

            // 🔥 FILTRO POR ROL
            if (rol == "CLIENTE")
            {
                var cliente = clienteBL.Listar()
                    .FirstOrDefault(x => x.IdUsuario == idUsuario);

                lista = cliente != null
                    ? lista.Where(x => x.IdCliente == cliente.IdCliente).ToList()
                    : new List<Cita>();
            }

            if (rol == "BARBERO")
            {
                var barbero = barberoBL.Listar()
                    .FirstOrDefault(x => x.IdUsuario == idUsuario);

                lista = barbero != null
                    ? lista.Where(x => x.IdBarbero == barbero.IdBarbero).ToList()
                    : new List<Cita>();
            }

            // 🔥 LLENAR NOMBRES
            var clientes = clienteBL.Listar();
            var barberos = barberoBL.Listar();
            var servicios = servicioBL.Listar();

            foreach (var c in lista)
            {
                var cliente = clientes.FirstOrDefault(x => x.IdCliente == c.IdCliente);
                var barbero = barberos.FirstOrDefault(x => x.IdBarbero == c.IdBarbero);
                var servicio = servicios.FirstOrDefault(x => x.IdServicio == c.IdServicio);

                c.NombreCliente = cliente != null ? cliente.Nombre : "N/A";
                c.NombreBarbero = barbero != null ? barbero.Nombre : "N/A";
                c.NombreServicio = servicio != null ? servicio.Nombre : "N/A";
            }

            return View(lista);
        }

        public IActionResult Crear()
        {
            ViewBag.Barberos = barberoBL.Listar();
            ViewBag.Servicios = servicioBL.Listar();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cita c)
        {
            var idUsuario = HttpContext.Session.GetInt32("id_usuario");

            var cliente = clienteBL.Listar()
                .FirstOrDefault(x => x.IdUsuario == idUsuario);

            if (cliente == null)
                return RedirectToAction("Index");

            c.IdCliente = cliente.IdCliente;
            c.Estado = "PROGRAMADA";

            try
            {
                citaBL.Crear(c);
                TempData["Success"] = "Cita creada correctamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Crear");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Cancelar(int id)
        {
            try
            {
                citaBL.Cancelar(id);
                TempData["Success"] = "Cita cancelada correctamente";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public JsonResult HorasDisponibles(int idBarbero, string fecha)
        {
            DateTime fechaConvertida = DateTime.Parse(fecha);

            var horas = citaBL.ObtenerHorasDisponibles(idBarbero, fechaConvertida);

            if (horas.Count == 0)
                return Json(new List<string> { "NO DISPONIBLE" });

            return Json(horas);
        }
    }
}