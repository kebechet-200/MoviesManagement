using MediatR;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Movies.Commands.Add
{
    public class AddMovieCommand : BaseMovieCommand, IRequest<Unit> { }
}
