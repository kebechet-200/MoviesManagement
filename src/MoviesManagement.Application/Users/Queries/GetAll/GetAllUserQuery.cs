using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Users.Queries.GetAll
{
    public record GetAllUserQuery() : IRequest<IQueryable<User>>;
}
