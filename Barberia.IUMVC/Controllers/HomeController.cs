using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Barberia.IUMVC.Models;

namespace Barberia.IUMVC.Controllers
{
    public class HomeController : Controller
    {
        // 🔒 PROTEGER TODO EL CONTROLLER
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = HttpContext.Session.GetString("usuario");

            if (usuario == null)
            {
                context.Result = RedirectToAction("Index", "Login");
            }

            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}