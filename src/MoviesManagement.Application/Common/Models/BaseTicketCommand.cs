using MoviesManagement.Application.Tickets.Commands.Buy;
using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Common.Models
{
    public class BaseTicketCommand
    {
        public Guid UserId { get; init; } = default!;
        public Guid MovieId { get; init; } = default!;
        public TicketEnum State { get; init; } = default!;
    }
    public static class TicketExtension
    {
        public static Ticket CommandToDomain(this BaseTicketCommand model) =>
            new Ticket
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                State = model.State
            };
    }
}
