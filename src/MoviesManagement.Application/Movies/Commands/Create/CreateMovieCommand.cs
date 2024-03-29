﻿using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Movies.Commands.Create
{
    public class CreateMovieCommand : BaseMovieCommand, IRequest<Unit> { }
}
