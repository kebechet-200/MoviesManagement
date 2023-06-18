namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserDoesNotExistException : Exception
    {
        public const string Code = "User does not exist.";

        public UserDoesNotExistException(string message) : base(message) { }
    }
}
