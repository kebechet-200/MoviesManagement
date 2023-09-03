using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Common.Models
{
    public class BaseMovieCommand
    {
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; init; }
        public bool IsActive { get; init; }
        public bool IsExpired { get; init; }
    }

    public static class MovieExtensions
    {
        internal static Movie ToMovieDomainModel(this BaseMovieCommand movie)
        {
            return new Movie
            {
                Name = movie.Name,
                Image = movie.Image,
                Description = movie.Description,
                StartDate = movie.StartDate,
                IsActive = movie.IsActive,
                IsExpired = movie.IsExpired
            };
        }

        internal static User CreateUserModel(this BaseUserCommand request)
        {
            return new User
            {
                Username = request.Username,
                Password = EncryptPasswordExtension.Encrypt(request.Password)
            };
        }
    }
}
