using CodeLingo.Backend.Data;
using CodeLingo.Backend.Models;

namespace CodeLingo.Backend.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }
    }
}