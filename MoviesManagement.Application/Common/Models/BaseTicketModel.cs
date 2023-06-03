using MoviesManagement.Application.Tickets.Commands.Buy;
using MoviesManagement.Domain.Common.Enum;

namespace MoviesManagement.Application.Common.Models
{
    public class BaseTicketModel
    {
        public Guid UserId { get; init; } = default!;
        public Guid MovieId { get; init; } = default!;
        public TicketEnum State { get; init; } = default!;
    }
}
