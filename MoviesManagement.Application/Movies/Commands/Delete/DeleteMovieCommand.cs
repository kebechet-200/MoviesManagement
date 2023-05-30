using MediatR;

namespace MoviesManagement.Application.Movies.Commands.Delete
{
    public record DeleteMovieCommand(Guid id) : IRequest<Unit>;
}
