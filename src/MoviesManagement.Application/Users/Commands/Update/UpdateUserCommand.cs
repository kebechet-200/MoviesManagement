using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Users.Commands.Update
{
    public class UpdateUserCommand : BaseUserCommand, IRequest<Unit> { }
}
