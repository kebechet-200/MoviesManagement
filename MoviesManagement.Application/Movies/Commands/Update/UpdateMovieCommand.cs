using MediatR;
using MoviesManagement.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Application.Movies.Commands.Update
{
    public class UpdateMovieCommand : BaseMovieCommand, IRequest<Unit> { }
}
