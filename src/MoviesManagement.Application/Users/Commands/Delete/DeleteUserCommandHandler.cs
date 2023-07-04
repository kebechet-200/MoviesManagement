using MediatR;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Users.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteMovieCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                throw new MoviesNotFoundException($"User id is empty");

            var result = await _userRepository.DeleteAsync(request.Id).ConfigureAwait(false);

            if (result.HasValue is false)
                throw new MovieCannotBeUpdatedException("The movie can not be updated");

            return Unit.Value;
        }
    }
}
