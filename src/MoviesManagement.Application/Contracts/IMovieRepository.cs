using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Contracts;

public interface IMovieRepository
{
    Task<Guid> CreateAsync(Movie movie, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Movie movie, CancellationToken cancellationToken);
    Task<Guid> DeleteAsync(Guid guid, CancellationToken cancellationToken);

    Task<IQueryable<Movie>> GetAllAsync(CancellationToken cancellationToken);
    Task<Movie> GetAsync(Guid guid, CancellationToken cancellationToken);
}
