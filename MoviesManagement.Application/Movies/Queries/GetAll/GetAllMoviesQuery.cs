using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.GetAll
{
    public class GetAllMoviesQuery : IRequest<IQueryable<Movie>> { }
}
