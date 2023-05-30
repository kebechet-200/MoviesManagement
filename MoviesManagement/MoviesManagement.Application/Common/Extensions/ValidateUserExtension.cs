using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Application.Common.Extensions
{
    internal static class ValidateUserExtension
    {
        private static UserValidator _validator;
        static ValidateUserExtension()
        {
            _validator = new();
        }

        internal static async Task ValidateQuery(BaseUserModel request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation cancelled");

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                string exception = validationResult.Errors
                    .FirstOrDefault()?.ErrorMessage
                    ?? string.Empty;

                throw new UserValidationException(exception);
            }
        }
    }
}
