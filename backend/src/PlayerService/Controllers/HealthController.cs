using Microsoft.AspNetCore.Mvc;

namespace BIS.PlayerService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", service = "PlayerService", timestamp = DateTime.UtcNow });
    }
}
