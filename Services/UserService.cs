using CodeLingo.Backend.Models;
using CodeLingo.Backend.Repositories;

namespace CodeLingo.Backend.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(UserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public string Register(RegisterRequest request)
        {
            var existingUser = _userRepository.GetUserByEmail(request.Email);

            if (existingUser != null)
            {
                return "User already exists";
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            _userRepository.AddUser(user);

            return "User registered successfully";
        }

        public LoginResponse? Login(LoginRequest request)
        {
            var user = _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return null;
            }

            string token = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                Message = "Login successful",
                Token = token,
                Name = user.Name,
                Email = user.Email,
                Level = user.Level,
                IsPremium = user.IsPremium
            };
        }

        public object? GetProfile(int userId)
        {
            var user = _userRepository.GetUserById(userId);

            if (user == null)
            {
                return null;
            }

            return new
            {
                user.Name,
                user.Email,
                user.Level,
                user.CurrentStreak,
                user.LongestStreak,
                user.TotalQuizzesCompleted,
                user.IsPremium
            };
        }
    }
}