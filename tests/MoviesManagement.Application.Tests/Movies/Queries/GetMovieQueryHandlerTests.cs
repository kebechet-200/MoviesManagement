using FluentAssertions;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Domain.Common.Exceptions;
using Xunit;

namespace MoviesManagement.Application.Tests.Movies.Queries
{
    public class GetMovieQueryHandlerTests : IClassFixture<MovieFixture>
    {
        private readonly MovieFixture _fixture;

        public GetMovieQueryHandlerTests(MovieFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        public async Task Handle_WhenMovieIdIsEmpty_ShouldReturnError()
        {
            Exception exception = default!;
            GetMovieQueryHandler handler = _fixture.GetMovieQueryHandler;
            try
            {
                _ = await handler.Handle(_movieWithEmptyId, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull().And.BeOfType<MovieIdIsEmptyException>();
            exception.Message.Should().Be(ErrorMessages.MovieIdIsEmpty);
        }

        [Fact]
        public async Task Handle_WhenMovieRepositoryReturnsNull_ShouldReturnError()
        {
            Exception exception = default!;
            GetMovieQueryHandler handler = _fixture.GetMovieQueryHandler;

            try
            {
                _ = await handler.Handle(_movieWithFailedGuid, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull().And.BeOfType<MoviesNotFoundException>();
            exception.Message.Should().Be(ErrorMessages.MovieNotFound);
        }

        [Fact]
        public async Task Handle_WhenMovieRepositoryReturnsSuccess_ShouldReturnMovie()
        {
            Exception exception = default!;
            GetMovieQueryHandler handler = _fixture.GetMovieQueryHandler;
            //TODO : that's wrong commands should have it's own models
            GetMovieResponse result = default!;

            try
            {
                result = await handler.Handle(_movieWithSuccessGuid, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
            result.Description.Should().Be("testdescription");
            result.Image.Should().Be("testimage");
            result.Name.Should().Be("success");
        }

        private GetMovieQuery _movieWithEmptyId = new GetMovieQuery { Id = Guid.Empty };
        private GetMovieQuery _movieWithSuccessGuid = new GetMovieQuery { Id = new ("{CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C}") };
        private GetMovieQuery _movieWithFailedGuid = new GetMovieQuery { Id = new("{CF1A8C1C-F2D0-41A1-A12C-53D9BE513A1C}") };
    }
}
