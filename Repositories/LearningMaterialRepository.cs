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
    }
}