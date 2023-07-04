using MediatR;

namespace MoviesManagement.Application.Movies.Commands.Delete
{
    public class DeleteMovieCommand : IRequest<Unit> 
    {
        public Guid Id { get; init; }
    }
}
