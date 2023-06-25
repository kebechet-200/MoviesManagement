using FluentValidation;
using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Movies.Commands.Create
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Unit>
    {
        private readonly MovieValidator<CreateMovieCommand> _validator;
        private readonly IMovieRepository _movieRepository;

        public CreateMovieCommandHandler(IMovieRepository movieRepository, MovieValidator<CreateMovieCommand> validator)
        {
            _validator = validator;
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request).ConfigureAwait(false);

            if (validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ToString());

            var result = await _movieRepository.CreateAsync(request.ToMovieDomainModel()).ConfigureAwait(false);

            if (result == Guid.Empty)
                throw new MovieCannotBeAddedException(ErrorMessages.MovieCannotBeAdded);

            return Unit.Value;
        }
    }
}
