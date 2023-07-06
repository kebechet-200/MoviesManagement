using FluentValidation;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Common.Validators
{
    public class UserValidator : AbstractValidator<BaseUserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                    .WithMessage(ErrorMessages.UsernameShouldNotBeEmpty)
                .Length(8, 15)
                    .WithMessage(ErrorMessages.LessOrMoreUsernameLength);

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage(ErrorMessages.PasswordShouldNotBeEmpty)
                .Length(8, 15)
                    .WithMessage(ErrorMessages.LessOrMorePasswordLength);
        }
    }
}
