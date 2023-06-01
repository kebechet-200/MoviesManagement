using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Interfaces
{
    public interface ITicketRepository
    {
        Task CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(Ticket ticket);

        Task<Ticket> GetTicketAsync(Guid id);
        Task<IQueryable<Ticket>> GetAllTicketAsync();
    }
}
