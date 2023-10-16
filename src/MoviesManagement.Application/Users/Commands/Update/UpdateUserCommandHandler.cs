using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Users.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await ValidateUserExtension.ValidateQuery(request, cancellationToken);

            var userExists = await _userRepository.ExistsAsync(request.Username, cancellationToken).ConfigureAwait(false);

            if (userExists is false)
                throw new UserDoesNotExistException($"User does not exist in the database.");

            var updatedUser =  await _userRepository.UpdateAsync(request.CreateUserModel(), cancellationToken).ConfigureAwait(false);

            if (updatedUser == Guid.Empty)
                throw new UserNotUpdatedException("There was some problem while updating user, try again later");

            return Unit.Value;
        }
    }
}
