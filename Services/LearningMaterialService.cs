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

        public LearningMaterial Create(int userId, CreateLearningMaterialRequest request)
        {
            var material = new LearningMaterial
            {
                UserId = userId,
                Title = request.Title,
                Content = request.Content
            };

            return _repository.Add(material);
        }

        public List<LearningMaterial> GetMyMaterials(int userId)
        {
            return _repository.GetByUserId(userId);
        }

        public bool Delete(int userId, int materialId)
        {
            var material = _repository.GetByIdAndUserId(materialId, userId);

            if (material == null)
            {
                return false;
            }

            return _repository.Delete(material);
        }
    }
}