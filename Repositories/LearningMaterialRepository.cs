using CodeLingo.Backend.Data;
using CodeLingo.Backend.Models;

namespace CodeLingo.Backend.Repositories
{
    public class LearningMaterialRepository
    {
        private readonly AppDbContext _context;

        public LearningMaterialRepository(AppDbContext context)
        {
            _context = context;
        }

        public LearningMaterial Add(LearningMaterial material)
        {
            _context.LearningMaterials.Add(material);
            _context.SaveChanges();

            return material;
        }

        public List<LearningMaterial> GetByUserId(int userId)
        {
            return _context.LearningMaterials
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
        }

        public LearningMaterial? GetByIdAndUserId(int id, int userId)
        {
            return _context.LearningMaterials
                .FirstOrDefault(m => m.Id == id && m.UserId == userId);
        }

        public bool Delete(LearningMaterial material)
        {
            _context.LearningMaterials.Remove(material);
            _context.SaveChanges();

            return true;
        }
    }
}