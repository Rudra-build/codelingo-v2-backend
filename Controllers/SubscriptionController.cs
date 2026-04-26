using CodeLingo.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SubscriptionController : BaseApiController
    {
        private readonly SubscriptionService _service;

        public SubscriptionController(SubscriptionService service)
        {
            _service = service;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            int userId = GetCurrentUserId();
            return Ok(_service.GetStatus(userId));
        }

        [HttpPost("upgrade")]
        public IActionResult Upgrade()
        {
            int userId = GetCurrentUserId();

            var result = _service.UpgradeToPremium(userId);

            if (result == "User not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("downgrade")]
        public IActionResult Downgrade()
        {
            int userId = GetCurrentUserId();

            var result = _service.DowngradeToFree(userId);

            if (result == "User not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}