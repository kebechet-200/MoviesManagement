using MediatR;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace MoviesManagement.Application.Movies.Commands.Update
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Unit>
    {
        MovieValidator<UpdateMovieCommand> _validator;
        IMovieRepository _movieRepository;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _validator = new(); // use di
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request).ConfigureAwait(false);

            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ToString());

            var result = await _movieRepository.UpdateAsync(request.ToMovieDomainModel()).ConfigureAwait(false);

            if (result.HasValue is false)
                throw new MovieCannotBeUpdatedException("The movie can not be updated");

            return Unit.Value;
        }
    }
}
