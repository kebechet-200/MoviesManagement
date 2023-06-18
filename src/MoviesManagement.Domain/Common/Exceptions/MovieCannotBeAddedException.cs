namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieCannotBeAddedException : Exception
    {
        public const string Code = "Movie can not be added";

        public MovieCannotBeAddedException(string message) : base(message) { }
    }
}
