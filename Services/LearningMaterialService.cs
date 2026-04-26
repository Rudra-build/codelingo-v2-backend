using CodeLingo.Backend.Models;
using CodeLingo.Backend.Repositories;

namespace CodeLingo.Backend.Services
{
    public class LearningMaterialService
    {
        private readonly LearningMaterialRepository _repository;

        public LearningMaterialService(LearningMaterialRepository repository)
        {
            _repository = repository;
        }

        public string Create(int userId, CreateLearningMaterialRequest request)
        {
            var material = new LearningMaterial
            {
                UserId = userId,
                Title = request.Title,
                Content = request.Content
            };

            _repository.Add(material);

            return "Learning material saved";
        }
    }
}