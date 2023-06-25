using FluentValidation;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Common.Validators
{
    public class MovieValidator<T> : AbstractValidator<T> where T : BaseMovieModel
    {
        public MovieValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage(ErrorMessages.MovieNameShouldNotBeEmpty)
                .MaximumLength(50)
                    .WithMessage(ErrorMessages.MovieNameLengthMustBeShorterThan50);

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage(ErrorMessages.MovieDescriptionShouldNotBeEmpty)
                .MaximumLength(255)
                    .WithMessage(ErrorMessages.MovieDescriptionLengthMustBeShorterThan255);
        }
    }
}
