namespace CodeLingo.Backend.Models
{
    public class LearningMaterial
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }

        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}