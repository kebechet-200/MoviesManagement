using MoviesManagement.Domain.Common;

namespace MoviesManagement.Domain.POCO
{
    public class User : BaseEntity
    {
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public IReadOnlyList<Ticket> Tickets { get; } = new List<Ticket>();
    }
}
