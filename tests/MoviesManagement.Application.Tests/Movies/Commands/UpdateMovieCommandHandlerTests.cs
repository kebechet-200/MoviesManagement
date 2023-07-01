using FluentAssertions;
using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoviesManagement.Application.Tests.Movies.Commands
{
    public class UpdateMovieCommandHandlerTests : IClassFixture<MovieFixture>
    {
        public readonly MovieFixture _movieFixture;

        public UpdateMovieCommandHandlerTests(MovieFixture movieFixture)
        {
            _movieFixture = movieFixture;
        }

        [Fact]
        public async Task Handle_WhenMovieNameIsEmpty_ShouldThrowValidationException()
        {
            // Arrange
            Exception exception = default!;
            var handler = _movieFixture.UpdateMovieCommandHandler;

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
            var handler = _movieFixture.UpdateMovieCommandHandler;

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
            var handler = _movieFixture.UpdateMovieCommandHandler;

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
            var handler = _movieFixture.UpdateMovieCommandHandler;

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
            var handler = _movieFixture.UpdateMovieCommandHandler;

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
            exception.Should().NotBeNull().And.BeOfType<MovieCannotBeUpdatedException>();
            exception.Message.Should().Be(ErrorMessages.MovieCannotBeUpdated);
        }

        [Fact]
        public async Task Handle_WhenRepositoryReturnsSuccess_ShouldReturnUnitValue()
        {
            // Arrange
            Exception? exception = default;
            var handler = _movieFixture.UpdateMovieCommandHandler;
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

        private UpdateMovieCommand _movieWithEmptyName = new UpdateMovieCommand
        {
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private UpdateMovieCommand _movieWithMoreThan50CharacterName = new UpdateMovieCommand
        {
            Name = new string('t', 51),
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private UpdateMovieCommand _movieWithEmptyDescription = new UpdateMovieCommand
        {
            Name = "success",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private UpdateMovieCommand _movieWithMoreThan255CharacterDescription = new UpdateMovieCommand
        {
            Name = "success",
            Description = new string('t', 256),
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private UpdateMovieCommand _successMovie = new UpdateMovieCommand
        {
            Name = "success",
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
        private UpdateMovieCommand _failedMovie = new UpdateMovieCommand
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
