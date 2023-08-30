using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Application.Movies.Queries.GetAll;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Common.Extensions
{
    internal static class ModelConvertingExtension
    {
        internal static GetMovieResponseModel MovieDomainToResultModel(this Movie movie) =>
            new()
            {
                Description = movie.Description,
                Id = movie.Id,
                Image = movie.Image,
                IsActive = movie.IsActive,
                IsExpired = movie.IsExpired,
                Name = movie.Name,
                StartDate = movie.StartDate,
            };

        internal static List<GetMovieResponseModel> MoviesDomainToResultModel(this IQueryable<Movie> movies) =>
            movies.Select(movie => new GetMovieResponseModel
            {
                Description = movie.Description,
                Id = movie.Id,
                Image = movie.Image,
                IsActive = movie.IsActive,
                IsExpired = movie.IsExpired,
                Name = movie.Name,
                StartDate = movie.StartDate
            })
            .ToList();

    }
}
