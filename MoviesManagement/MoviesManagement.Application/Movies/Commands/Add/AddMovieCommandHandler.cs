using FluentValidation;
using MediatR;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Movies.Commands.Add
{
    public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, Unit>
    {
        MovieValidator<AddMovieCommand> _validator;
        IMovieRepository _movieRepository;

        public AddMovieCommandHandler(IMovieRepository movieRepository)
        {
            _validator = new();
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (validationResult.IsValid)
                throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ToString());

            var result = await _movieRepository.CreateAsync(request.ToMovieDomainModel());

            if (result.HasValue is false)
                throw new MovieCannotBeAddedException("Some error occured while adding the movie");

            return Unit.Value;
        }
    }
}
