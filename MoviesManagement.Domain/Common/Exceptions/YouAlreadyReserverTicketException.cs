namespace MoviesManagement.Domain.Common.Exceptions
{
    public class YouAlreadyReserverTicketException : Exception
    {
        public const string Code = nameof(YouAlreadyReserverTicketException);

        public YouAlreadyReserverTicketException(string message) : base(message) { }
    }
    
}
