using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok("CodeLingo Backend is running");
        }
    }
}