using Moq;
using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Domain.POCO;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Tests.Fixtures
{
    public class MovieFixture
    {
        private readonly Mock<IMovieRepository> _movieRepository = new Mock<IMovieRepository>();
        private ServiceCollection ServiceCollection { get; } = new ServiceCollection();

        public CreateMovieCommandHandler CreateMovieCommandHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                CreateMovieCommandHandler service = serviceProvider.GetRequiredService<CreateMovieCommandHandler>();
                return service;
            }
        }

        public DeleteMovieCommandHandler DeleteMovieCommandHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                DeleteMovieCommandHandler? service = serviceProvider.GetService<DeleteMovieCommandHandler>()!;
                return service;
            }
        }

        public UpdateMovieCommandHandler UpdateMovieCommandHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                UpdateMovieCommandHandler? service = serviceProvider.GetService<UpdateMovieCommandHandler>()!;
                return service;
            }
        }

        public MovieFixture()
        {
            var successGuid = new Guid("{CF0A8C1C-F2D0-41A1-A12C-53D9BE513A1C}");
            var failedGuid = new Guid("{CF1A8C1C-F2D0-41A1-A12C-53D9BE513A1C}");
            #region Movie command handler mocks

            // Create movie.
            _movieRepository
                .Setup(x => x.CreateAsync(It.Is<Movie>(movie => movie.Name == _successMovie.Name)))
                .ReturnsAsync(successGuid);

            _movieRepository
                .Setup(x => x.CreateAsync(It.Is<Movie>(movie => movie.Name == _failedMovie.Name)))
                .ReturnsAsync(Guid.Empty);

            // Update movie.
            _movieRepository
                .Setup(x => x.UpdateAsync(It.Is<Movie>(movie => movie.Name == _successMovie.Name)))
                .ReturnsAsync(successGuid);

            _movieRepository
                .Setup(x => x.UpdateAsync(It.Is<Movie>(movie => movie.Name == _failedMovie.Name)))
                .ReturnsAsync(Guid.Empty);

            // Delete movie.
            _movieRepository
                .Setup(x => x.DeleteAsync(It.Is<Guid>(guid => guid == successGuid)))
                .ReturnsAsync(successGuid);

            _movieRepository
                .Setup(x => x.DeleteAsync(It.Is<Guid>(x => x == failedGuid)))
                .ReturnsAsync(Guid.Empty);

            #endregion

            #region Add transient services
            AddServices();
            #endregion
        }

        private void AddServices()
        {
            // Add repository mock
            _ = ServiceCollection.AddTransient(_ => _movieRepository.Object);

            // Add handlers
            _ = ServiceCollection.AddTransient<CreateMovieCommandHandler>();
            _ = ServiceCollection.AddTransient<UpdateMovieCommandHandler>();
            _ = ServiceCollection.AddTransient<DeleteMovieCommandHandler>();

            // Add validator
            _ = ServiceCollection.AddTransient<MovieValidator<CreateMovieCommand>>();
            _ = ServiceCollection.AddTransient<MovieValidator<UpdateMovieCommand>>();
        }

        private CreateMovieCommand _successMovie = new CreateMovieCommand
        {
            Name = "success"
        };

        private Movie _failedMovie = new Movie
        {
            Name = "failed"
        };
    }
}
