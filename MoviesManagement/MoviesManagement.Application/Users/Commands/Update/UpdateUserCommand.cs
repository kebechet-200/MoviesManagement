using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Users.Commands.Update
{
    public class UpdateUserCommand : BaseUserModel, IRequest<Unit> { }
}
