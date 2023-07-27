using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Contracts;

public interface IUserRepository
{
    Task<Guid> AddAsync(User user, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<Guid> DeleteAsync(Guid guid, CancellationToken cancellationToken);

    Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IQueryable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<bool> ExistsAsync(string username, CancellationToken cancellationToken);
    Task<Guid> ValidateAsync(User user, CancellationToken cancellationToken);
}
