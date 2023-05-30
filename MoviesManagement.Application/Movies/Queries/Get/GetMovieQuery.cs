using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public record GetMovieQuery(Guid id) : IRequest<Movie> { }
}
