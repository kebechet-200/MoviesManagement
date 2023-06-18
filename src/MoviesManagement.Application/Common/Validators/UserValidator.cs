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
                    .WithMessage("Username should not be empty")
                .Length(8, 15)
                    .WithMessage("Username length should be between 8 and 15");

            RuleFor(x => x.Password)
                    .NotEmpty()
                        .WithMessage("Password should not be empty")
                    .Length(8, 15)
                        .WithMessage("Password length should be between 8 and 15");
        }
    }
}
