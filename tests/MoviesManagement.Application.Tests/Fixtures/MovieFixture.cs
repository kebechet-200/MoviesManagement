using Moq;
using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Application.Common.Validators;
using MoviesManagement.Domain.POCO;
using MoviesManagement.Application.Movies.Queries.Get;

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

        public GetMovieQueryHandler GetMovieQueryHandler
        {
            get
            {
                ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider();
                GetMovieQueryHandler? service = serviceProvider.GetService<GetMovieQueryHandler>()!;
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
                .Setup(x => x.CreateAsync(It.Is<Movie>(movie => movie.Name == _successMovieCommand.Name), default))
                .ReturnsAsync(successGuid);

            _movieRepository
                .Setup(x => x.CreateAsync(It.Is<Movie>(movie => movie.Name == _failedMovieCommand.Name), default))
                .ReturnsAsync(Guid.Empty);

            // Update movie.
            _movieRepository
                .Setup(x => x.UpdateAsync(It.Is<Movie>(movie => movie.Name == _successMovieCommand.Name), default))
                .ReturnsAsync(successGuid);

            _movieRepository
                .Setup(x => x.UpdateAsync(It.Is<Movie>(movie => movie.Name == _failedMovieCommand.Name), default))
                .ReturnsAsync(Guid.Empty);

            // Delete movie.
            _movieRepository
                .Setup(x => x.DeleteAsync(It.Is<Guid>(guid => guid == successGuid), default))
                .ReturnsAsync(successGuid);

            _movieRepository
                .Setup(x => x.DeleteAsync(It.Is<Guid>(x => x == failedGuid), default))
                .ReturnsAsync(Guid.Empty);

            _movieRepository
                .Setup(x => x.GetAsync(It.Is<Guid>(x => x == failedGuid), default))
                .ReturnsAsync((Movie)default!);

            _movieRepository
                .Setup(x => x.GetAsync(It.Is<Guid>(guid => guid == successGuid), default))
                .ReturnsAsync(_successMovie);

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
            _ = ServiceCollection.AddTransient<GetMovieQueryHandler>();

            // Add validator
            _ = ServiceCollection.AddTransient<MovieValidator<CreateMovieCommand>>();
            _ = ServiceCollection.AddTransient<MovieValidator<UpdateMovieCommand>>();
        }

        private CreateMovieCommand _successMovieCommand = new CreateMovieCommand
        {
            Name = "success"
        };

        private CreateMovieCommand _failedMovieCommand = new CreateMovieCommand
        {
            Name = "failed"
        };

        private Movie _successMovie = new Movie
        {
            Description = "testdescription",
            Image = "testimage",
            IsActive = true,
            IsExpired = false,
            Name = "success",
            StartDate = DateTime.UtcNow.AddHours(1)
        };
    }
}
