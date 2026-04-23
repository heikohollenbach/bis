using Microsoft.AspNetCore.Mvc;

namespace BIS.ChatService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { status = "healthy", service = "ChatService", timestamp = DateTime.UtcNow });
    }
}
