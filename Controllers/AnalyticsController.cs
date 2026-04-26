using CodeLingo.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AnalyticsController : BaseApiController
    {
        private readonly AnalyticsService _service;

        public AnalyticsController(AnalyticsService service)
        {
            _service = service;
        }

        [HttpGet("me")]
        public IActionResult GetMyAnalytics()
        {
            int userId = GetCurrentUserId();

            var result = _service.GetUserAnalytics(userId);

            if (result.ToString() == "User not found")
            {
                return NotFound(result);
            }

            if (result.ToString() == "Analytics is available only for premium users.")
            {
                return StatusCode(403, result);
            }

            return Ok(result);
        }
    }
}