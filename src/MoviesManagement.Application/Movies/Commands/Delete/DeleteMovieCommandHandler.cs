using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, Unit>
    {
        private readonly IMovieRepository _movieRepository;

        public DeleteMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
                throw new MovieIdIsEmptyException(ErrorMessages.MovieIdIsEmpty);

            var result = await _movieRepository.DeleteAsync(request.id).ConfigureAwait(false);

            if (result == Guid.Empty)
                throw new MovieCannotBeDeletedException(ErrorMessages.MovieCannotBeDeleted);

            return Unit.Value;
        }
    }
}
