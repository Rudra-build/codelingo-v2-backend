using CodeLingo.Backend.Data;
using System.Linq;

namespace CodeLingo.Backend.Services
{
    public class AnalyticsService
    {
        private readonly AppDbContext _context;

        public AnalyticsService(AppDbContext context)
        {
            _context = context;
        }

        public object GetUserAnalytics(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                    {
                        return "User not found";
                    }

                    if (!user.IsPremium)
                    {
                        return "Analytics is available only for premium users.";
                    }

            var results = _context.QuizResults
                .Where(r => r.UserId == userId)
                .ToList();

            int totalAttempts = results.Count;
            int totalScore = results.Sum(r => r.Score);
            int totalQuestions = results.Sum(r => r.TotalQuestions);

            double averagePercentage = totalQuestions == 0
                ? 0
                : Math.Round((double)totalScore / totalQuestions * 100, 2);

            return new
            {
                UserId = userId,
                Level = user?.Level,
                CurrentStreak = user?.CurrentStreak,
                LongestStreak = user?.LongestStreak,
                TotalQuizzesCompleted = user?.TotalQuizzesCompleted,
                TotalAttempts = totalAttempts,
                AveragePercentage = averagePercentage
            };
        }
    }
}