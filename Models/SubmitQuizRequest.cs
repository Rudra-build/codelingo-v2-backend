namespace CodeLingo.Backend.Models
{
    public class SubmitQuizRequest
    {
        public int QuizId { get; set; }

        public List<AnswerDto> Answers { get; set; } = new List<AnswerDto>();
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }

        public int SelectedOptionId { get; set; }
    }
}