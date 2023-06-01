using MediatR;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Users.Queries.Get
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
                throw new User($"user id is empty");

            var user = await _userRepository.GetAsync(request.id);

            if (user is null)
                throw new MoviesNotFoundException($"The user with an id of {request.id} not found");

            return user;
        }
    }
}
