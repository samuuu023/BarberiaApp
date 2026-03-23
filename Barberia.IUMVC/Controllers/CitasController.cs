using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;
using System.Linq;
using Barberia.IUMVC.Filters;
using System;

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
                TempData["Success"] = "Cita cancelada";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public JsonResult HorasDisponibles(int idBarbero, string fecha)
        {
            DateTime f = DateTime.Parse(fecha);

            var horas = citaBL.ObtenerHorasDisponibles(idBarbero, f);

            if (horas.Count == 0)
                return Json(new List<string> { "NO DISPONIBLE" });

            return Json(horas);
        }
    }
}