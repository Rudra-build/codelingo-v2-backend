namespace CodeLingo.Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public int Level { get; set; } = 1;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<LearningMaterial> LearningMaterials { get; set; } = new List<LearningMaterial>();

        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();

        public List<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
        public int CurrentStreak { get; set; } = 0;

        public int LongestStreak { get; set; } = 0;

        public DateTime? LastQuizCompletedDate { get; set; }

        public int TotalQuizzesCompleted { get; set; } = 0;

        public bool IsPremium { get; set; } = false;

        public string? StripeCustomerId { get; set; }

        public string? StripeSubscriptionId { get; set; }

        public DateTime? PremiumStartedAt { get; set; }

        public DateTime? LastFreeQuizGeneratedDate { get; set; }
    }
}