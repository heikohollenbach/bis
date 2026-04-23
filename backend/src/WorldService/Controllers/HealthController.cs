using Microsoft.AspNetCore.Mvc;

namespace BIS.WorldService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", service = "WorldService", timestamp = DateTime.UtcNow });
    }
}
