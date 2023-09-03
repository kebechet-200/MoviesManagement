using FluentAssertions;
using MoviesManagement.Application.Movies.Queries.Get;
using MoviesManagement.Application.Movies.Queries.GetAll;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Domain.POCO;
using Xunit;

namespace MoviesManagement.Application.Tests.Movies.Queries
{
    public class GetAllMoviesQueryHandlerTests : IClassFixture<MovieFixture>
    {
        private readonly MovieFixture _fixture;

        public GetAllMoviesQueryHandlerTests(MovieFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Handle_WhenMovieRepositoryReturnsSuccess_ShouldReturnMovie()
        {
            Exception exception = default!;
            GetAllMoviesQueryHandler handler = _fixture.GetAllMoviesQueryHandler;
            List<GetMovieResponse> movies = default!;
            try
            {
                movies = await handler.Handle(new(), CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
            movies[0].Description.Should().Be("testdescription");
            movies[0].Image.Should().Be("testimage");
            movies[0].Name.Should().Be("success");
            movies[1].Description.Should().Be("test");
            movies[1].Image.Should().Be("test");
            movies[1].Name.Should().Be("test");
        }
    }
}
