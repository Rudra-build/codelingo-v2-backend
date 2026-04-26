using CodeLingo.Backend.Data;
using Stripe.Checkout;

namespace CodeLingo.Backend.Services
{
    public class SubscriptionService
    {
        private readonly AppDbContext _context;

        public SubscriptionService(AppDbContext context)
        {
            _context = context;
        }

        public object GetStatus(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            return new
            {
                IsPremium = user?.IsPremium ?? false,
                Level = user?.Level ?? 1,
                CurrentStreak = user?.CurrentStreak ?? 0
            };
        }

        public string UpgradeToPremium(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return "User not found";
            }

            user.IsPremium = true;
            user.PremiumStartedAt = DateTime.Now;

            _context.SaveChanges();

            return "User upgraded to premium";
        }

        public string DowngradeToFree(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return "User not found";
            }

            user.IsPremium = false;
            user.StripeSubscriptionId = null;

            _context.SaveChanges();

            return "User downgraded to free";
        }
    }
}