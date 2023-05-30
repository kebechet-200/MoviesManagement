using MediatR;

namespace MoviesManagement.Application.Users.Commands.Delete
{
    public record DeleteUserCommand(Guid id) : IRequest<Unit>
}
