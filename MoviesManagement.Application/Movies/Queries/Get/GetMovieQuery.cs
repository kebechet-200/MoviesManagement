using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public record GetMovieQuery(Guid Guid) : IRequest<Movie> { }
}
