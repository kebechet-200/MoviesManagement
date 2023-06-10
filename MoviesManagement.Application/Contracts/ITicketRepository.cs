using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Contracts;

public interface ITicketRepository
{
    Task BuyTicketAsync(Ticket ticket);
    Task ReserveTicketAsync(Ticket ticket);
    Task CancelTicketAsync(Ticket ticket);

    Task<Ticket> GetTicketAsync(Guid id);
    Task<IQueryable<Ticket>> GetAllTicketAsync();
}
