using CodeLingo.Backend.Models;
using CodeLingo.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseApiController
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var result = _userService.Register(request);

            if (result == "User already exists")
            {
                return BadRequest(result);
            }

            return Created("", result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var result = _userService.Login(request);

            if (result == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(result);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            int userId = GetCurrentUserId();

            var result = _userService.GetProfile(userId);

            if (result == null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }
    }
}