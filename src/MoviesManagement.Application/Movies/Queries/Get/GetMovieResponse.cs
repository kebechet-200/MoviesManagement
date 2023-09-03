using MoviesManagement.Domain.POCO;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieResponse : BaseMovieCommand { }

    public static class GetMovieExtension
    {
        public static GetMovieResponse DomainToResponseModel(this Movie movie) =>
            new()
            {
                Description = movie.Description,
                Image = movie.Image,
                IsActive = movie.IsActive,
                IsExpired = movie.IsExpired,
                Name = movie.Name,
                StartDate = movie.StartDate
            };

        public static List<GetMovieResponse> DomainToResponseModel(this IQueryable<Movie> response) =>
            response
            .Select(x => x.DomainToResponseModel())
            .ToList();
    }
}
