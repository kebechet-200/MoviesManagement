using FluentAssertions;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Application.Common;
using Xunit;
using FluentValidation;

namespace MoviesManagement.Application.Tests.Movies.Commands
{
    public class CreateMovieCommandHandlerTests /*IClassFixture<MovieFixture>*/
    {
        private readonly MovieFixture _movieFixture;

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
            Description = "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
    }
}
