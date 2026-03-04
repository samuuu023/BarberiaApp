using Microsoft.AspNetCore.Mvc;
using Barberia.Logica;
using Barberia.Entidades;

namespace Barberia.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        UsuarioBL bl = new UsuarioBL();

        [HttpPost]
        public IActionResult Crear(Usuario u)
        {
            bl.CrearUsuario(u);
            return Ok("Usuario creado");
        }
    }
}
