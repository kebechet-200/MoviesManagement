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
        private readonly MovieValidator<UpdateMovieCommand> _validator;
        private readonly IMovieRepository _movieRepository;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository, MovieValidator<UpdateMovieCommand> validator)
        {
            _validator = validator;
            _movieRepository = movieRepository;
        }

        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request).ConfigureAwait(false);

            if (validationResult.IsValid is false)
                throw new ValidationException(validationResult.Errors.FirstOrDefault()?.ToString());

            var result = await _movieRepository.UpdateAsync(request.ToMovieDomainModel()).ConfigureAwait(false);

            if (result == Guid.Empty)
                throw new MovieCannotBeUpdatedException("The movie can not be updated");

            return Unit.Value;
        }
    }
}
