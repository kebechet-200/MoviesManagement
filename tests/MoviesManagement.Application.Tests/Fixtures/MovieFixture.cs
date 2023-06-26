﻿using Moq;
using Microsoft.Extensions.DependencyInjection;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Movies.Commands.Create;
using MoviesManagement.Application.Movies.Commands.Delete;
using MoviesManagement.Application.Movies.Commands.Update;
using MoviesManagement.Domain.POCO;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MoviesManagement.Application.Common.Validators;

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
            // Create movie.
            _movieRepository
                .Setup(x => x.CreateAsync(_successMovie))
                .ReturnsAsync(Guid.NewGuid());

            _movieRepository
                .Setup(x => x.CreateAsync(_failedMovie))
                .ReturnsAsync(Guid.Empty);

            // Update movie.
            _movieRepository
                .Setup(x => x.CreateAsync(_successMovie))
                .ReturnsAsync(Guid.NewGuid());

            _movieRepository
                .Setup(x => x.CreateAsync(_failedMovie))
                .ReturnsAsync(Guid.Empty);

            // Delete movie.
            var guid = Guid.NewGuid();
            _movieRepository
                .Setup(x => x.DeleteAsync(guid))
                .ReturnsAsync(guid);

            _movieRepository
                .Setup(x => x.DeleteAsync(Guid.NewGuid()))
                .ReturnsAsync(Guid.Empty);

            _ = ServiceCollection.AddTransient(_ => _movieRepository.Object);
            _ = ServiceCollection.AddTransient<CreateMovieCommandHandler>();
            _ = ServiceCollection.AddTransient<MovieValidator<CreateMovieCommand>>();
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

        private Movie _failedMovie = new Movie
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
