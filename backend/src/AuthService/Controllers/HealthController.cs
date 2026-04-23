using Microsoft.AspNetCore.Mvc;

namespace BIS.AuthService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", service = "AuthService", timestamp = DateTime.UtcNow });
    }
}
