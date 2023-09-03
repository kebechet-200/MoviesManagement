using MediatR;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Contracts;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, GetMovieResponse>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<GetMovieResponse> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new MovieIdIsEmptyException(ErrorMessages.MovieIdIsEmpty);

            var movie = await _movieRepository.GetAsync(request.Id, cancellationToken).ConfigureAwait(false);

            if (movie is null)
                throw new MoviesNotFoundException(ErrorMessages.MovieNotFound);

            return movie.DomainToResponseModel();
        }
    }
}
