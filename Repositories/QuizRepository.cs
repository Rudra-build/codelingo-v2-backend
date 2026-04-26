using CodeLingo.Backend.Data;
using CodeLingo.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeLingo.Backend.Repositories
{
    public class QuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public QuizResponseDto? GetQuiz(int userId, int quizId)
        {
            var quiz = _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Options)
                .FirstOrDefault(q => q.Id == quizId && q.UserId == userId);

            if (quiz == null) return null;

            return new QuizResponseDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Questions = quiz.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    Options = q.Options.Select(o => new OptionDto
                    {
                        Id = o.Id,
                        OptionText = o.OptionText
                    }).ToList()
                }).ToList()
            };
        }
    }
}