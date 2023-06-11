using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Tickets.Commands.Cancel
{
    public class CancelTicketCommandHandler : IRequestHandler<CancelTicketCommand, Unit>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public CancelTicketCommandHandler(ITicketRepository ticketRepository,
                                          IMovieRepository movieRepository,
                                          IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CancelTicketCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                throw new TicketNotFoundException("Userid is empty");

            if (request.MovieId == Guid.Empty)
                throw new TicketNotFoundException("MovieId is empty");

            if (request.State is not TicketEnum.Cancel)
                throw new InvalidStateException("Ticket status is not Cancel");

            var user = await _userRepository.GetAsync(request.UserId).ConfigureAwait(false);
            var movie = await _movieRepository.GetAsync(request.MovieId).ConfigureAwait(false);

            if (user is null)
                throw new UserDoesNotExistException($"User with an id {request.UserId} does not exist in the database");

            if (movie is null)
                throw new MoviesNotFoundException($"Movie with an id {request.MovieId} does not exist in the database");

            if (movie.IsActive is false)
                throw new MovieIsInactiveException("The ticket can't be cancelled because the movie is inactive");

            bool isLessThanHourFromStart = DateTime.UtcNow > movie.StartDate.AddHours(-1);

            if (isLessThanHourFromStart)
                throw new MovieStartsLessThanAnHourException("You can't cancel the movie starts less than an hour.");

            var movieTickets = user.Tickets
                .Where(x => x.UserId == user.Id)
                .Where(x => x.MovieId == movie.Id);

            if (movieTickets.Any(x => x.State is not TicketEnum.Cancel))
                throw new CantCancelTicketException("You don't have active tickets");

            await _ticketRepository.CancelTicketAsync(request.TicketCommandToDomain()).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
