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

        public object CreateCheckoutSession(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return "User not found";
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Quantity = 1,
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "gbp",
                            UnitAmount = 499,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "CodeLingo Premium"
                            }
                        }
                    }
                },

                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            user.StripeSubscriptionId = session.Id;
            _context.SaveChanges();

            return new
            {
                CheckoutUrl = session.Url,
                SessionId = session.Id
            };
        }

        public string ConfirmPayment(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return "User not found";
            }

            if (string.IsNullOrWhiteSpace(user.StripeSubscriptionId))
            {
                return "No Stripe session found";
            }

            var service = new SessionService();
            var session = service.Get(user.StripeSubscriptionId);

            if (session.PaymentStatus != "paid")
            {
                return "Payment not completed";
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