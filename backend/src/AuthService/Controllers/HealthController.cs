using Microsoft.AspNetCore.Mvc;

namespace BIS.AuthService.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", service = "AuthService", timestamp = DateTime.UtcNow });
    }
}
