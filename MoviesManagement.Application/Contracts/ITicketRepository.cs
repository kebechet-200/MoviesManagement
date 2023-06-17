using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Contracts;

public interface ITicketRepository
{
    Task BuyTicketAsync(Ticket ticket);
    Task ReserveTicketAsync(Ticket ticket);
    Task CancelTicketAsync(Ticket ticket);

    Task<Ticket> GetTicketAsync(Guid movieId, Guid userId);
    Task<IQueryable<Ticket>> GetAllTicketAsync();
}
