
using MoviesManagement.Domain.Common;
using MoviesManagement.Domain.Common.Enum;

namespace MoviesManagement.Domain.POCO
{
    public class Ticket : BaseEntity
    {
        public Movie Movie { get; init; } = new();
        public User User { get; init; } = new();
        public TicketEnum State { get; init; }
    }
}
