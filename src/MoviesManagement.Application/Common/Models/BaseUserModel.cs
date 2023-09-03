namespace MoviesManagement.Application.Common.Models
{
    public class BaseUserCommand
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
    }
}
