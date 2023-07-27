namespace MoviesManagement.Domain.Common.Exceptions
{
    public class TicketNotBoughtException : Exception
    {
        public const string Code = nameof(TicketNotBoughtException);

        public TicketNotBoughtException() : base() { }

        public TicketNotBoughtException(string message) : base(message) { }
    }
}
