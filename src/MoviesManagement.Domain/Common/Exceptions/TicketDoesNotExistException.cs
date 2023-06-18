namespace MoviesManagement.Domain.Common.Exceptions
{
    public class TicketDoesNotExistException : Exception
    {
        public const string Code = "Ticket does not exist.";

        public TicketDoesNotExistException(string message) : base(message) { }
    }
}
