using MediatR;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.GetAll
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, List<GetMovieResponse>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<List<GetMovieResponse>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

            if (movies is null)
                throw new MoviesNotFoundException("Movies not found in database");

            return movies.DomainToResponseModel();
        }
    }
}
