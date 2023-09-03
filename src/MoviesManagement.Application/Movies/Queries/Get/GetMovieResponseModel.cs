namespace MoviesManagement.Application.Movies.Queries.Get
{
    public class GetMovieResponseModel
    {
        public Guid Id { get; init; } = default;
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; init; }
        public bool IsActive { get; init; }
        public bool IsExpired { get; init; }
    }
}
