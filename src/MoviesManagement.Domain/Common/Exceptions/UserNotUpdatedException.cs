namespace MoviesManagement.Domain.Common.Exceptions
{
    public class UserNotUpdatedException : Exception
    {
        public const string Code = "User not updated";

        public UserNotUpdatedException(string message) : base(message) { }
    }
}
