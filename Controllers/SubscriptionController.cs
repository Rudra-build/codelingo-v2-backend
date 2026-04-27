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

        [HttpPost("create-checkout")]
        public IActionResult CreateCheckout()
        {
            int userId = GetCurrentUserId();

            var result = _service.CreateCheckoutSession(userId);

            if (result.ToString() == "User not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost("confirm")]
        public IActionResult Confirm()
        {
            int userId = GetCurrentUserId();

            var result = _service.ConfirmPayment(userId);

            if (result == "User not found")
            {
                return NotFound(result);
            }

            if (result == "No Stripe session found" || result == "Payment not completed")
            {
                return BadRequest(result);
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