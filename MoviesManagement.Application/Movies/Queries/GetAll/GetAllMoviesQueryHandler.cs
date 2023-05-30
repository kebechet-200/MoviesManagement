using MediatR;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.GetAll
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IQueryable<Movie>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Task<IQueryable<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = _movieRepository.GetAllAsync();

            if (movies is null)
                throw new MoviesNotFoundException("Movies not found in database");

            return movies;
        }
    }
}
