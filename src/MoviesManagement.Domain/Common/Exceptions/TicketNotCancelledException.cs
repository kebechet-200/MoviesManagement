namespace MoviesManagement.Domain.Common.Exceptions
{
    public class TicketNotCancelledException : Exception
    {
        public const string Code = nameof(TicketNotCancelledException);

        public TicketNotCancelledException() : base() { }

        public TicketNotCancelledException(string message) : base(message) { }
    }
}
