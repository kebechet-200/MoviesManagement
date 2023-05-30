namespace MoviesManagement.Domain.Common.Exceptions
{
    public class MovieAlreadyStartedException : Exception
    {
        public const string Code = "Movie already started";

        public MovieAlreadyStartedException(string message) : base(message) { }
    }
}
