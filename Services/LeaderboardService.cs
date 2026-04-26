using CodeLingo.Backend.Data;
using System.Linq;

namespace CodeLingo.Backend.Services
{
    public class LeaderboardService
    {
        private readonly AppDbContext _context;

        public LeaderboardService(AppDbContext context)
        {
            _context = context;
        }

        public object GetTopUsers()
        {
            var users = _context.Users
                .OrderByDescending(u => u.Level)
                .ThenByDescending(u => u.TotalQuizzesCompleted)
                .Take(10)
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.Level,
                    u.CurrentStreak,
                    u.TotalQuizzesCompleted
                })
                .ToList();

            return users;
        }
    }
}