using FluentValidation;
using MoviesManagement.Application.Common.Models;

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
