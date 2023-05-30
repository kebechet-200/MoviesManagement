

using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<IQueryable<Movie>> GetAllAsync();
        Task<Movie> GetAsync(Guid guid);
        Task<Guid?> CreateAsync(Movie movie);
        Task<Guid?> UpdateAsync(Movie movie);
        Task<Guid?> DeleteAsync(Guid guid);
    }
}
