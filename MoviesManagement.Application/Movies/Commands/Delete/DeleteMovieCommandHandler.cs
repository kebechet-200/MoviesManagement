using MediatR;
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
                throw new MoviesNotFoundException($"Movie id is empty");

            var result = await _movieRepository.DeleteAsync(request.id);

            if (result.HasValue is false)
                throw new MovieCannotBeUpdatedException("The movie can not be deleted");

            return Unit.Value;
        }
    }
}
