using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;

namespace Barberia.IUMVC.Controllers
{
    public class LoginController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string correo, string password)
        {
            Usuario u = usuarioBL.Login(correo, password);

            if (u != null)
            {
                HttpContext.Session.SetString("usuario", u.Nombre);
                HttpContext.Session.SetString("rol", u.Rol);
                HttpContext.Session.SetInt32("id_usuario", u.IdUsuario);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}