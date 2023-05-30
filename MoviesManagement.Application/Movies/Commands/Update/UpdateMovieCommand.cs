using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Movies.Commands.Update
{
    public class UpdateMovieCommand : BaseMovieCommand, IRequest<Unit> { }
}
