using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Exists(string username, CancellationToken cancellationToken);
        Task<Guid?> Validate(User user, CancellationToken cancellationToken);
        Task<bool> Add(User user);
        Task<bool> Update(User user);
    }
}
