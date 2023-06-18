using Moq;
using MoviesManagement.Application.Contracts;

namespace MoviesManagement.Application.Tests
{
    public class ApplicationMovieFixture
    {
        private readonly Mock<IMovieRepository> _movieRepository;
        public ApplicationMovieFixture()
        {
            _movieRepository = new Mock<IMovieRepository>();

            _movieRepository.Setup(x => )
        }
    }
}
