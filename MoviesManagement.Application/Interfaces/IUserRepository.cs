using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<IQueryable<User>> GetAllAsync();
        Task<Guid?> AddAsync(User user);
        Task<Guid?> UpdateAsync(User user);
        Task<Guid?> DeleteAsync(Guid guid);

        Task<bool> ExistsAsync(string username, CancellationToken cancellationToken);
        Task<Guid?> ValidateAsync(User user, CancellationToken cancellationToken);
    }
}
