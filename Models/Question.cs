namespace CodeLingo.Backend.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string QuestionText { get; set; } = string.Empty;

        public string DifficultyLevel { get; set; } = "Easy";

        public Quiz? Quiz { get; set; }

        public List<Option> Options { get; set; } = new List<Option>();
    }
}