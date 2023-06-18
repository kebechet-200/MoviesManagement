using MediatR;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, Movie>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
                throw new MoviesNotFoundException($"Movie id is empty");

            var movie = await _movieRepository.GetAsync(request.id).ConfigureAwait(false);

            if (movie is null)
                throw new MoviesNotFoundException($"The movie with an id of {request.id} not found");

            return movie;
        }
    }
}
