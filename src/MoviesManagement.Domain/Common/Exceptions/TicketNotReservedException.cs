namespace MoviesManagement.Domain.Common.Exceptions
{
    public class TicketNotReservedException : Exception
    {
        public const string Code = nameof(TicketNotReservedException);

        public TicketNotReservedException() : base() { }

        public TicketNotReservedException(string message) : base(message) { }
    }
}
