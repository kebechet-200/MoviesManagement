namespace MoviesManagement.Domain.Common.Exceptions
{
    public class YouAlreadyBoughtTicketException : Exception
    {
        public const string Code = "You already bought ticket.";

        public YouAlreadyBoughtTicketException(string message) : base(message) { }
    }
}
