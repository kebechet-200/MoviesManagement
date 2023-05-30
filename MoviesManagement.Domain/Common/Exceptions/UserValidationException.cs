namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserValidationException : Exception
    {
        public const string Code = "User validation exception";

        public UserValidationException(string message) : base(message) { }
    }
}
