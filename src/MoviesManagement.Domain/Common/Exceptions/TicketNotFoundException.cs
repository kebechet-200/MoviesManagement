namespace MoviesManagement.Domain.Common.Exceptions
{
    public class TicketNotFoundException : Exception
    {
        public const string Code = "The id of the user is not privided";

        public TicketNotFoundException(string message) : base(message) { }
    }
}
