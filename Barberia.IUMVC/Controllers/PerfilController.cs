using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using System.Linq;
using Barberia.IUMVC.Filters;

namespace Barberia.IUMVC.Controllers
{
    [AuthorizeRole("CLIENTE")]
    public class PerfilController : Controller
    {
        CitaBL citaBL = new();
        ClienteBL clienteBL = new();

        public IActionResult Index()
        {
            var idUsuario = HttpContext.Session.GetInt32("id_usuario");

            var cliente = clienteBL.Listar()
                .FirstOrDefault(x => x.IdUsuario == idUsuario);

            var citas = citaBL.Listar()
                .Where(x => x.IdCliente == cliente.IdCliente)
                .ToList();

            return View(citas);
        }
    }
}