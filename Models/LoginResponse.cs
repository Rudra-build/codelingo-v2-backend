namespace CodeLingo.Backend.Models
{
    public class LoginResponse
    {
        public string Message { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int Level { get; set; }

        public bool IsPremium { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}