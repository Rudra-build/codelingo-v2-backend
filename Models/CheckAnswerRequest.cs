namespace CodeLingo.Backend.Models
{
    public class CheckAnswerRequest
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public int SelectedOptionId { get; set; }
    }

    public class CheckAnswerResponse
    {
        public bool IsCorrect { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}