namespace CodeLingo.Backend.Models
{
    public class QuizResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public List<OptionDto> Options { get; set; }
    }

    public class OptionDto
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
    }
}