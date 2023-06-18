namespace MoviesManagement.Domain.Common.Exceptions
{
    public class InvalidStateException : Exception
    {
        public const string Code = "Ticket State is invalid";

        public InvalidStateException(string message) : base(message) { } 
    }
}
