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

            return Created("", result);
        }
    }
}