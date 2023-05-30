using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Interfaces;
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

            var userExists = await _userRepository.Exists(request.Username, cancellationToken);

            if (userExists)
                throw new UserAlreadyExistsException($"User with name {request.Username} already exsits");

            var user = CreateUserExtension.CreateUserModel(request);

            var updatedUser =  await _userRepository.Update(user);

            if (updatedUser is false)
                throw new UserNotUpdatedException("There was some problem while updating user, try again later");

            return Unit.Value;
        }
    }
}
