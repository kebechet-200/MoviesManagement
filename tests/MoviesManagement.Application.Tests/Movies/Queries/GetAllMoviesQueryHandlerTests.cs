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
            //TODO : that's wrong commands should have it's own models
            List<Movie> result = default!;

            try
            {
                var movies = await handler.Handle(new(), CancellationToken.None);
                result = movies.ToList();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
            result[0].Description.Should().Be("testdescription");
            result[0].Image.Should().Be("testimage");
            result[0].Name.Should().Be("success");
            result[1].Description.Should().Be("test");
            result[1].Image.Should().Be("test");
            result[1].Name.Should().Be("test");
        }
    }
}
