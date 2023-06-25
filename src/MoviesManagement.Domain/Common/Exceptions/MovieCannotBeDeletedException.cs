namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieCannotBeDeletedException : Exception
    {
        public const string Code = "Movie can not be deleted";

        public MovieCannotBeDeletedException(string message) : base(message) { }
    }
}
