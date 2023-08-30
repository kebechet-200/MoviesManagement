using MediatR;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.GetAll
{
    public class GetAllMoviesQuery : IRequest<List<GetMovieResponseModel>> { }
}
