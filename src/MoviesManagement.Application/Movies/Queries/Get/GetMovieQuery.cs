using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieQuery : IRequest<GetMovieResponse> 
    {
        public Guid Id { get; init; }
    }
}
