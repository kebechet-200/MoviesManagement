namespace MoviesManagement.Domain.Common.Exceptions
{
    public class InvalidUserException : Exception
    {
        public const string Code = "Invalid user.";

        public InvalidUserException(string message) : base(message) { }
    }
}
