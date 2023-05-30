namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieIsNotAvailableException : Exception
    {
        public const string Code = "Movie is not available.";

        public MovieIsNotAvailableException(string message) : base(message) { }
    }
}
