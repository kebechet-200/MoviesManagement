namespace MoviesManagement.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
