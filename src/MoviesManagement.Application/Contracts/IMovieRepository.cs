using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Contracts;

public interface IMovieRepository
{
    Task<Guid> CreateAsync(Movie movie);
    Task<Guid> UpdateAsync(Movie movie);
    Task<Guid> DeleteAsync(Guid guid);

    Task<IQueryable<Movie>> GetAllAsync();
    Task<Movie> GetAsync(Guid guid);
}
