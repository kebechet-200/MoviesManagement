namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public const string Code = "User Already Exists";
        public UserAlreadyExistsException(string message) : base(message) { }
    }
}
