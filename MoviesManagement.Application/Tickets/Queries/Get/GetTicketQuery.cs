using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tickets.Queries.Get
{
    public sealed class GetTicketQuery : IRequest<Ticket>
    {
        public Guid UserId { get; init; } = default;
        public Guid MovieId { get; init; } = default;
    }
}
