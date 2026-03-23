using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;
using System.Linq;
using Barberia.IUMVC.Filters;

namespace Barberia.IUMVC.Controllers
{
    [AuthorizeRole("ADMIN")]
    public class ClienteController : Controller
    {
        ClienteBL bl = new ClienteBL();

        public IActionResult Index(string buscar)
        {
            var lista = bl.Listar();

            if (!string.IsNullOrEmpty(buscar))
            {
                lista = lista.Where(x =>
                    x.Nombre.Contains(buscar) ||
                    x.Apellido.Contains(buscar) ||
                    x.Correo.Contains(buscar)
                ).ToList();
            }

            return View(lista);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public IActionResult Crear(Cliente c)
        {
            bl.Crear(c);
            TempData["Success"] = "Cliente creado correctamente";
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id) => View(bl.Obtener(id));

        [HttpPost]
        public IActionResult Editar(Cliente c)
        {
            bl.Editar(c);
            TempData["Success"] = "Cliente actualizado";
            return RedirectToAction("Index");
        }

        public IActionResult Ver(int id) => View(bl.Obtener(id));

        public IActionResult Eliminar(int id)
        {
            bl.Eliminar(id);
            TempData["Success"] = "Cliente eliminado correctamente";
            return RedirectToAction("Index");
        }
    }
}