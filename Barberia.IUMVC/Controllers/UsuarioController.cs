using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;
using System.Linq;
using Barberia.IUMVC.Filters;

namespace Barberia.IUMVC.Controllers
{
    [AuthorizeRole("ADMIN")]
    public class UsuarioController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL();

        public IActionResult Index(string buscar)
        {
            var usuarios = usuarioBL.ObtenerTodos();

            if (!string.IsNullOrEmpty(buscar))
            {
                usuarios = usuarios
                    .Where(x => x.Nombre.Contains(buscar))
                    .ToList();
            }

            return View(usuarios);
        }

        public IActionResult Ver(int id)
        {
            var usuario = usuarioBL.ObtenerTodos()
                .FirstOrDefault(x => x.IdUsuario == id);

            return View(usuario);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Usuario u)
        {
            if (!ModelState.IsValid)
                return View(u);

            usuarioBL.CrearUsuario(u);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            usuarioBL.Eliminar(id);
            return RedirectToAction("Index");
        }

        public IActionResult Editar(int id)
        {
            var usuario = usuarioBL.ObtenerTodos()
                .FirstOrDefault(x => x.IdUsuario == id);

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Usuario u)
        {
            if (!ModelState.IsValid)
                return View(u);

            usuarioBL.Editar(u);
            return RedirectToAction("Index");
        }
    }
}