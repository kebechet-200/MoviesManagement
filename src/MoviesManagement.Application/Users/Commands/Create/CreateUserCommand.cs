using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Users.Commands.Create
{
    public class CreateUserCommand : BaseUserCommand, IRequest<Unit> { }
}
