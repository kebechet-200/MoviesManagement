using FluentAssertions;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Application.Common;
using Xunit;
using FluentValidation;
using MoviesManagement.Domain.POCO;
using MoviesManagement.Domain.Common.Exceptions;
using MediatR;

namespace MoviesManagement.Application.Tests.Movies.Commands
{
    public class CreateMovieCommandHandlerTests : IClassFixture<MovieFixture>
    {
        public readonly MovieFixture _movieFixture;

        public CreateMovieCommandHandlerTests(MovieFixture movieFixture)
        {
            _movieFixture = movieFixture;
        }

        [Fact]
        public async Task Handle_WhenMovieNameIsEmpty_ShouldThrowValidationException()
        {
            // Arrange
            Exception exception = default!;
            var handler = _movieFixture.CreateMovieCommandHandler;

            // Act
            try
            {
                var result = await handler.Handle(_movieWithEmptyName, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull().And.BeOfType<ValidationException>();
            exception.Message.Should().Be(ErrorMessages.MovieNameShouldNotBeEmpty);
        }

        [Fact]
        public async Task Handle_WhenMovieNameIsMoreThan50Characters_ShouldThrowValidationException()
        {
            // Arrange
            Exception exception = default!;
            var handler = _movieFixture.CreateMovieCommandHandler;

            // Act
            try
            {
                var result = await handler.Handle(_movieWithMoreThan50CharacterName, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull().And.BeOfType<ValidationException>();
            exception.Message.Should().Be(ErrorMessages.MovieNameLengthMustBeShorterThan50);
        }

        [Fact]
        public async Task Handle_WhenMovieDescriptionIsEmpty_ShouldThrowValidationException()
        {
            // Arrange
            Exception exception = default!;
            var handler = _movieFixture.CreateMovieCommandHandler;

            // Act
            try
            {
                var result = await handler.Handle(_movieWithEmptyDescription, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull().And.BeOfType<ValidationException>();
            exception.Message.Should().Be(ErrorMessages.MovieDescriptionShouldNotBeEmpty);
        }

        [Fact]
        public async Task Handle_WhenMovieDescriptionIsMoreThan255Characters_ShouldThrowValidationException()
        {
            // Arrange
            Exception exception = default!;
            var handler = _movieFixture.CreateMovieCommandHandler;

            // Act
            try
            {
                var result = await handler.Handle(_movieWithMoreThan255CharacterDescription, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull().And.BeOfType<ValidationException>();
            exception.Message.Should().Be(ErrorMessages.MovieDescriptionLengthMustBeShorterThan255);
        }

        [Fact]
        public async Task Handle_WhenRepositoryReturnsFail_ShouldThrowMovieCannotBeAddedException()
        {
            // Arrange
            Exception exception = default!;
            var handler = _movieFixture.CreateMovieCommandHandler;

            // Act
            try
            {
                var result = await handler.Handle(_failedMovie, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().NotBeNull().And.BeOfType<MovieCannotBeAddedException>();
            exception.Message.Should().Be(ErrorMessages.MovieCannotBeAdded);
        }

        [Fact]
        public async Task Handle_WhenRepositoryReturnsSuccess_ShouldReturnUnitValue()
        {
            // Arrange
            Exception? exception = default;
            var handler = _movieFixture.CreateMovieCommandHandler;
            Unit result = default;

            // Act
            try
            {
                result = await handler.Handle(_successMovie, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //Assert
            exception.Should().BeNull();
            result.Should().Be(Unit.Value);
        }

        private CreateMovieCommand _movieWithEmptyName = new CreateMovieCommand
        {
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private CreateMovieCommand _movieWithMoreThan50CharacterName = new CreateMovieCommand
        {
            Name = new string('t', 51),
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private CreateMovieCommand _movieWithEmptyDescription = new CreateMovieCommand
        {
            Name = "success",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private CreateMovieCommand _movieWithMoreThan255CharacterDescription = new CreateMovieCommand
        {
            Name = "success",
            Description = new string('t', 256),
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private CreateMovieCommand _successMovie = new CreateMovieCommand
        {
            Name = "success",
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private CreateMovieCommand _failedMovie = new CreateMovieCommand
        {
            Name = "failed",
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
    }
}
