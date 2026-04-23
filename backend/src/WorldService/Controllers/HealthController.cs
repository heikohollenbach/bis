using Microsoft.AspNetCore.Mvc;

namespace BIS.WorldService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", service = "WorldService", timestamp = DateTime.UtcNow });
    }
}
