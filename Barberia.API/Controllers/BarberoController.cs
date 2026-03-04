using Barberia.Logica;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/barberos")]
public class BarberoController : ControllerBase
{
    BarberoBL bl = new();

    [HttpGet]
    public IActionResult Listar()
        => Ok(bl.Listar());
}