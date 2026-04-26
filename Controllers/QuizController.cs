using CodeLingo.Backend.Models;
using CodeLingo.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeLingo.Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuizController : BaseApiController
    {
        private readonly QuizService _quizService;

        public QuizController(QuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost("generate")]
        public IActionResult Generate([FromQuery] int materialId, [FromBody] string text)
        {
            int userId = GetCurrentUserId();

            if (userId == 0)
            {
                return Unauthorized("Invalid token");
            }

            var result = _quizService.GenerateAndSave(userId, materialId, text);

            if (result == "User not found" || result == "Learning material not found")
            {
                return NotFound(result);
            }

            if (result == "Free users can only generate 1 quiz per day. Upgrade to premium for unlimited quizzes.")
            {
                return StatusCode(403, result);
            }

            if (result == "AI quiz generation failed" || result == "AI quiz validation failed")
            {
                return StatusCode(500, result);
            }

            return Created("", result);
        }

        [HttpGet("{id}")]
        public IActionResult GetQuiz(int id)
        {
            int userId = GetCurrentUserId();

            var quiz = _quizService.GetQuiz(userId, id);

            if (quiz == null)
            {
                return NotFound("Quiz not found");
            }

            return Ok(quiz);
        }

        [HttpPost("submit")]
        public IActionResult Submit([FromBody] SubmitQuizRequest request)
        {
            int userId = GetCurrentUserId();

            var result = _quizService.SubmitQuiz(userId, request);

            if (result == "Quiz not found")
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}