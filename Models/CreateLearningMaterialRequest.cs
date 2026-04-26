namespace CodeLingo.Backend.Models
{
    public class CreateLearningMaterialRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}