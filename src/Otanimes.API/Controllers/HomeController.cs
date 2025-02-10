using Microsoft.AspNetCore.Mvc;

namespace Otanimes.API.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Home() => Ok();
}