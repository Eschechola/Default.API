using Microsoft.AspNetCore.Mvc;

namespace Default.API.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult Home() => Ok();
}