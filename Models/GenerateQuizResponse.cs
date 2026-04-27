namespace CodeLingo.Backend.Models
{
    public class GenerateQuizResponse
    {
        public string Message { get; set; } = string.Empty;
        public int QuizId { get; set; }
        public int Level { get; set; }
    }
}