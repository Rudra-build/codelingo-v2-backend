using CodeLingo.Backend.Data;
using CodeLingo.Backend.Models;
using CodeLingo.Backend.Repositories;
using System.Text.Json;

namespace CodeLingo.Backend.Services
{
    public class QuizService
    {
        private readonly OpenAIService _openAIService;
        private readonly AppDbContext _context;
        private readonly QuizRepository _quizRepository;

        public QuizService(OpenAIService openAIService, AppDbContext context, QuizRepository quizRepository)
        {
            _openAIService = openAIService;
            _context = context;
            _quizRepository = quizRepository;
        }

        public string GenerateAndSave(int userId, int materialId, string content)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return "User not found";
            }

            var material = _context.LearningMaterials
                .FirstOrDefault(m => m.Id == materialId && m.UserId == userId);

            if (material == null)
            {
                return "Learning material not found";
            }

            DateTime today = DateTime.Today;

            if (!user.IsPremium)
            {
                if (user.LastFreeQuizGeneratedDate != null &&
                    user.LastFreeQuizGeneratedDate.Value.Date == today)
                {
                    return "Free users can only generate 1 quiz per day. Upgrade to premium for unlimited quizzes.";
                }

                user.LastFreeQuizGeneratedDate = today;
            }

            int level = user.Level;

            var aiResponse = _openAIService.GenerateQuiz(content, level);

            var questions = JsonSerializer.Deserialize<List<QuizQuestionDto>>(aiResponse);

            if (questions == null)
            {
                return "AI quiz validation failed";
            }

            var quiz = new Quiz
            {
                UserId = userId,
                LearningMaterialId = materialId,
                Title = "Generated Quiz",
                DifficultyLevel = $"Level {level}"
            };

            _context.Quizzes.Add(quiz);
            _context.SaveChanges();

            foreach (var q in questions)
            {
                var question = new Question
                {
                    QuizId = quiz.Id,
                    QuestionText = q.question
                };

                _context.Questions.Add(question);
                _context.SaveChanges();

                for (int i = 0; i < q.options.Count; i++)
                {
                    var option = new Option
                    {
                        QuestionId = question.Id,
                        OptionText = q.options[i],
                        IsCorrect = i == q.correctIndex
                    };

                    _context.Options.Add(option);
                }

                _context.SaveChanges();
            }

            _context.SaveChanges();

            return $"Quiz saved at Level {level}";
        }

        public string SubmitQuiz(int userId, SubmitQuizRequest request)
        {
            var quiz = _context.Quizzes.FirstOrDefault(q => q.Id == request.QuizId && q.UserId == userId);

            if (quiz == null)
            {
                return "Quiz not found";
            }

            int score = 0;

            foreach (var ans in request.Answers)
            {
                var correctOption = _context.Options
                    .FirstOrDefault(o =>
                        o.QuestionId == ans.QuestionId &&
                        o.Question.QuizId == request.QuizId &&
                        o.IsCorrect);

                if (correctOption != null && correctOption.Id == ans.SelectedOptionId)
                {
                    score++;
                }
            }

            int total = request.Answers.Count;

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null && total > 0)
            {
                double percentage = (double)score / total;

                if (percentage >= 0.8)
                {
                    user.Level++;
                }

                DateTime today = DateTime.Today;

                if (user.LastQuizCompletedDate == null)
                {
                    user.CurrentStreak = 1;
                }
                else if (user.LastQuizCompletedDate.Value.Date == today.AddDays(-1))
                {
                    user.CurrentStreak++;
                }
                else if (user.LastQuizCompletedDate.Value.Date == today)
                {
                    // Same day: keep streak same
                }
                else
                {
                    user.CurrentStreak = 1;
                }

                if (user.CurrentStreak > user.LongestStreak)
                {
                    user.LongestStreak = user.CurrentStreak;
                }

                user.LastQuizCompletedDate = today;
                user.TotalQuizzesCompleted++;
            }

            var result = new QuizResult
            {
                UserId = userId,
                QuizId = request.QuizId,
                Score = score,
                TotalQuestions = total
            };

            _context.QuizResults.Add(result);
            _context.SaveChanges();

            return $"Score: {score}/{total} | Level: {user?.Level} | Streak: {user?.CurrentStreak}";
        }

        public QuizResponseDto? GetQuiz(int userId, int quizId)
        {
            return _quizRepository.GetQuiz(userId, quizId);
        }


        private bool IsValidAiQuiz(List<QuizQuestionDto>? questions)
        {
            if (questions == null || questions.Count != 5)
            {
                return false;
            }

            foreach (var question in questions)
            {
                if (string.IsNullOrWhiteSpace(question.question))
                {
                    return false;
                }

                if (question.options == null || question.options.Count != 4)
                {
                    return false;
                }

                foreach (var option in question.options)
                {
                    if (string.IsNullOrWhiteSpace(option))
                    {
                        return false;
                    }
                }

                if (question.correctIndex < 0 || question.correctIndex > 3)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class QuizQuestionDto
    {
        public string question { get; set; } = string.Empty;

        public List<string> options { get; set; } = new List<string>();

        public int correctIndex { get; set; }
    }
}