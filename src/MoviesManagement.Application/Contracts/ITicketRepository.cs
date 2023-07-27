using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Contracts;

public interface ITicketRepository
{
    Task<Guid> BuyTicketAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<Guid> ReserveTicketAsync(Ticket ticket, CancellationToken cancellationToken);
    Task<Guid> CancelTicketAsync(Ticket ticket, CancellationToken cancellationToken);

    Task<Ticket> GetTicketAsync(Guid movieId, Guid userId, CancellationToken cancellationToken);
    Task<IQueryable<Ticket>> GetAllTicketAsync(CancellationToken cancellationToken);
}
