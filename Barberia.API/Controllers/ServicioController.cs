using Barberia.Logica;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/servicios")]
public class ServicioController : ControllerBase
{
    ServicioBL bl = new();

    [HttpGet]
    public IActionResult Listar()
        => Ok(bl.Listar());
}