using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, GetMovieResponseModel>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<GetMovieResponseModel> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new MovieIdIsEmptyException(ErrorMessages.MovieIdIsEmpty);

            var movie = await _movieRepository.GetAsync(request.Id, cancellationToken).ConfigureAwait(false);

            if (movie is null)
                throw new MoviesNotFoundException(ErrorMessages.MovieNotFound);

            return movie.MovieDomainToResultModel();
        }
    }
}
