using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Users.Queries.GenerateToken
{
    public class GenerateTokenQuery : BaseUserModel, IRequest<string> { };
}
