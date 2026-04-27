using CodeLingo.Backend.Models;
using CodeLingo.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LearningMaterialController : BaseApiController
    {
        private readonly LearningMaterialService _service;

        public LearningMaterialController(LearningMaterialService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateLearningMaterialRequest request)
        {
            int userId = GetCurrentUserId();

            if (userId == 0)
            {
                return Unauthorized("Invalid token");
            }

            var result = _service.Create(userId, request);

            return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
        }

        [HttpGet("me")]
        public IActionResult GetMyMaterials()
        {
            int userId = GetCurrentUserId();

            if (userId == 0)
            {
                return Unauthorized("Invalid token");
            }

            var result = _service.GetMyMaterials(userId);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int userId = GetCurrentUserId();

            if (userId == 0)
            {
                return Unauthorized("Invalid token");
            }

            bool deleted = _service.Delete(userId, id);

            if (!deleted)
            {
                return NotFound("Learning material not found");
            }

            return Ok("Learning material deleted");
        }
    }
}