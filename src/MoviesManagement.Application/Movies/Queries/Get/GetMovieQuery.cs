using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieQuery : IRequest<Movie> 
    {
        public Guid Id { get; init; }
    }
}
