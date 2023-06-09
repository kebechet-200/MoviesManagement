﻿using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Common.Models
{
    public class BaseMovieModel
    {
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; init; }
        public bool IsActive { get; init; }
        public bool IsExpired { get; init; }
    }

    public static class MovieExtensions
    {
        public static Movie ToMovieDomainModel(this BaseMovieModel movie)
        {
            return new Movie
            {
                Name = movie.Name,
                Image = movie.Image,
                Description = movie.Description,
                StartDate = movie.StartDate,
                IsActive = movie.IsActive,
                IsExpired = movie.IsExpired
            };
        }
    }
}
