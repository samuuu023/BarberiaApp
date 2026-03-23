using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;
using System.Linq;
using Barberia.IUMVC.Filters;

namespace Barberia.IUMVC.Controllers
{
    [AuthorizeRole("ADMIN", "BARBERO")]
    public class BarberoController : Controller
    {
        BarberoBL bl = new BarberoBL();

        public IActionResult Index(string buscar)
        {
            var lista = bl.Listar();

            if (!string.IsNullOrEmpty(buscar))
            {
                lista = lista
                    .Where(x => x.Nombre.Contains(buscar))
                    .ToList();
            }

            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Barbero b)
        {
            bl.CrearBarbero(b);
            TempData["Success"] = "Barbero creado correctamente";
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            return View(bl.Obtener(id));
        }

        [HttpPost]
        public IActionResult Editar(Barbero b)
        {
            bl.Editar(b);
            TempData["Success"] = "Barbero actualizado";
            return RedirectToAction("Index");
        }

        public IActionResult Ver(int id)
        {
            return View(bl.Obtener(id));
        }

        public IActionResult Eliminar(int id)
        {
            bl.Eliminar(id);
            TempData["Success"] = "Barbero eliminado correctamente";
            return RedirectToAction("Index");
        }
    }
}