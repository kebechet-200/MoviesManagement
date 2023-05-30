using MoviesManagement.Application.Movies.Commands.Add;
using MoviesManagement.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesManagement.Application.Common.Models
{
    public class BaseMovieCommand
    {
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime StartDate { get; init; }
        public bool IsActive { get; init; }
        public bool IsExpired { get; init; }
    }

    public static class MovieCommandExtensions
    {
        public static Movie ToMovieDomainModel(this BaseMovieCommand movie)
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
