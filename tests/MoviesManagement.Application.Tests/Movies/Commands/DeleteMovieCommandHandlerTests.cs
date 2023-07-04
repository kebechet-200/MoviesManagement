using FluentAssertions;
using MediatR;
using MoviesManagement.Application.Common;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Tests.Fixtures;
using MoviesManagement.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoviesManagement.Application.Tests.Movies.Commands
{
    public class DeleteMovieCommandHandlerTests : IClassFixture<MovieFixture>
    {
        private readonly MovieFixture fixture;

        public DeleteMovieCommandHandlerTests(MovieFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Handle_WhenMovieIdIsEmpty_ShouldReturnError()
        {
            Exception exception = default!;
            DeleteMovieCommandHandler handler = fixture.DeleteMovieCommandHandler;
            try
            {
                await handler.Handle(_movieWithEmptyId, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull().And.BeOfType<MovieIdIsEmptyException>();
            exception.Message.Should().Be(ErrorMessages.MovieIdIsEmpty);
        }

        [Fact]
        public async Task Handle_WhenMovieRepositoryReturnsEmptyGuid_ShouldReturnError()
        {
            Exception exception = default!;
            DeleteMovieCommandHandler handler = fixture.DeleteMovieCommandHandler;
            try
            {
                await handler.Handle(_movieWithFailedGuid, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().NotBeNull().And.BeOfType<MovieCannotBeDeletedException>();
            exception.Message.Should().Be(ErrorMessages.MovieCannotBeDeleted);
        }

        [Fact]
        public async Task Handle_WhenMovieRepositoryReturnsSuccess_ShouldReturnUnit()
        {
            Exception exception = default!;
            DeleteMovieCommandHandler handler = fixture.DeleteMovieCommandHandler;
            Unit result;
            try
            {
                result = await handler.Handle(_movieWithSuccessGuid, CancellationToken.None);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            exception.Should().BeNull();
            result.Should().Be(Unit.Value);
        }

        private DeleteMovieCommand _movieWithEmptyId = new DeleteMovieCommand { Id = Guid.Empty };
        private DeleteMovieCommand _movieWithFailedGuid = new DeleteMovieCommand { Id = new Guid("{CF1A8C1C-F2D0-41A1-A12C-53D9BE513A1C}") };
        private DeleteMovieCommand _movieWithSuccessGuid = new DeleteMovieCommand { Id = new Guid("{CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C}") };
    }
}
