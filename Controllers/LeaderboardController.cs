using CodeLingo.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly LeaderboardService _service;

        public LeaderboardController(LeaderboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetLeaderboard()
        {
            var result = _service.GetTopUsers();
            return Ok(result);
        }
    }
}