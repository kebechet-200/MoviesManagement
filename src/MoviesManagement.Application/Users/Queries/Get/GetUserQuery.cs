using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Users.Queries.Get
{
    public record GetUserQuery(Guid id) : IRequest<User>;
}
