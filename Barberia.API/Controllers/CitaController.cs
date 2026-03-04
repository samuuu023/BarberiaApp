using Barberia.Entidades;
using Barberia.Logica;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/citas")]
public class CitaController : ControllerBase
{
    CitaBL bl = new();

    [HttpPost]
    public IActionResult Crear(Cita c)
    {
        bl.Crear(c);
        return Ok("Cita creada");
    }

    [HttpGet("{idBarbero}")]
    public IActionResult PorBarbero(int idBarbero)
        => Ok(bl.ObtenerPorBarbero(idBarbero));
}