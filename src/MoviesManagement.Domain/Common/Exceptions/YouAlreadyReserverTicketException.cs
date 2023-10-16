namespace MoviesManagement.Domain.Common.Exceptions
{
    public class YouAlreadyReservedTicketException : Exception
    {
        public const string Code = nameof(YouAlreadyReservedTicketException);

        public YouAlreadyReservedTicketException(string message) : base(message) { }
    }
    
}
