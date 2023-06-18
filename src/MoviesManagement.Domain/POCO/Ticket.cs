
using MoviesManagement.Domain.Common;
using MoviesManagement.Domain.Common.Enum;

namespace MoviesManagement.Domain.POCO
{
    public class Ticket : BaseEntity
    {
        // Navigation properties.
        public Movie Movie { get; init; } = new();
        public User User { get; init; } = new();
        public TicketEnum State { get; init; }

        // Foreign keys.
        public Guid UserId { get; init; } = default;
        public Guid MovieId { get; init; } = default;
    }
}
