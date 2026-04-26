namespace CodeLingo.Backend.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int LearningMaterialId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string DifficultyLevel { get; set; } = "Easy";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }

        public LearningMaterial? LearningMaterial { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();

        public List<QuizResult> QuizResults { get; set; } = new List<QuizResult>();
    }
}