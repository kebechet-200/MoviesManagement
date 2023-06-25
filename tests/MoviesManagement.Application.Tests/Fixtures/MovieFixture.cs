using Moq;
using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Domain.POCO;

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
                CreateMovieCommandHandler? service = serviceProvider.GetService<CreateMovieCommandHandler>()!;
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
            _movieRepository
                .Setup(x => x.CreateAsync(_successMovie))
                .ReturnsAsync(Guid.NewGuid());
        }

        private Movie _successMovie = new Movie 
        { 
            Name = "success",
            Description= "test",
            IsActive = true,
            IsExpired = false,
            Image = "some-image.png",
            StartDate = DateTime.Now
        };
    }
}
