
using MoviesManagement.Domain.Common;

namespace MoviesManagement.Domain.POCO
{
    public class Movie : BaseEntity
    {
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; init; }
        public bool IsActive { get; init; }
        public bool IsExpired { get; init; }
    }
}
