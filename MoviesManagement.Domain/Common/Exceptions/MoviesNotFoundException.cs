namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MoviesNotFoundException : Exception
    {
        public const string Code = "Movies not found";

        public MoviesNotFoundException(string message) : base(message) { }
    }
}
