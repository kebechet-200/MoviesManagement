namespace MoviesManagement.Domain.Common.Exceptions
{
    public class CantCancelTicketException : Exception
    {
        public const string Code = "You can't cancel the ticket";

        public CantCancelTicketException(string message) : base(message) { }
    }
}
