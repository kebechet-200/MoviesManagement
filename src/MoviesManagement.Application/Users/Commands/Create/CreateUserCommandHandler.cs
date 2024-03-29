﻿using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await ValidateUserExtension.ValidateQuery(request, cancellationToken);

            var userExists = await _userRepository.ExistsAsync(request.Username, cancellationToken).ConfigureAwait(false);

            if (userExists)
                throw new UserAlreadyExistsException($"User with name {request.Username} already exsits");

            var userResponse = await _userRepository.AddAsync(request.CreateUserModel(), cancellationToken).ConfigureAwait(false);

            if (userResponse == Guid.Empty)
                throw new UserNotRegisteredException("There was some problem while adding the user, try again later");

            return Unit.Value;
        }
    }
}
