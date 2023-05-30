using FluentValidation;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Movies.Commands.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Application.Common.Validators
{
    public class MovieValidator<T> : AbstractValidator<T> where T : BaseMovieCommand
    {
        public MovieValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
