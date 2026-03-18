using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Barberia.Logica;
using Barberia.Entidades;
using System.Linq;

namespace Barberia.IUMVC.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL();

        // 🔒 PROTEGER + VALIDAR ROL
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = HttpContext.Session.GetString("usuario");
            var rol = HttpContext.Session.GetString("rol");

            if (usuario == null)
            {
                context.Result = RedirectToAction("Index", "Login");
                return;
            }

            // 🔥 SOLO ADMIN PUEDE ENTRAR
            if (rol != "ADMIN")
            {
                context.Result = RedirectToAction("Index", "Home");
                return;
            }

            base.OnActionExecuting(context);
        }

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