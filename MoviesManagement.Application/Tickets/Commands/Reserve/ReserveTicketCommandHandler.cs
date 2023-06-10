using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Tickets.Commands.Reserve
{
    public class ReserveTicketCommandHandler : IRequestHandler<ReserveTicketCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ITicketRepository _ticketRepository;

        public ReserveTicketCommandHandler(IUserRepository userRepository, IMovieRepository movieRepository, ITicketRepository ticketRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<Unit> Handle(ReserveTicketCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                throw new TicketNotFoundException("Userid is empty");

            if (request.MovieId == Guid.Empty)
                throw new TicketNotFoundException("MovieId is empty");

            if (request.State is not TicketEnum.Reserve)
                throw new InvalidStateException("Ticket status is not Reserve");

            var user = await _userRepository.GetAsync(request.UserId);
            var movie = await _movieRepository.GetAsync(request.MovieId);

            if (user is null)
                throw new UserDoesNotExistException($"User with an id {request.UserId} does not exist in the database");

            if (movie is null)
                throw new MoviesNotFoundException($"Movie with an id {request.MovieId} does not exist in the database");

            if (movie.IsActive)
                throw new Exception(); // TODO : Implement exception

            bool isMovieStartedAlready = movie.IsExpired || DateTime.UtcNow > movie.StartDate;

            if (isMovieStartedAlready)
                throw new MovieAlreadyStartedException("The movie that you are trying to buy already started.");

            bool isLessThanHourFromStart = DateTime.UtcNow > movie.StartDate.AddHours(-1);

            if (isLessThanHourFromStart)
                throw new MovieStartsLessThanAnHourException("The movie that you are trying to reserve starts less than an hour.");

            var movieTickets = user.Tickets
                .Where(x => x.UserId == user.Id)
                .Where(x => x.MovieId == movie.Id);

            if (movieTickets.Any(x => x.State == TicketEnum.Buy))
                throw new YouAlreadyBoughtTicketException("You already bought the ticket");

            if (movieTickets.Any(x => x.State == TicketEnum.Reserve))
                throw new YouAlreadyReserverTicketException("You already reserve the ticket of this movie");

            await _ticketRepository.ReserveTicketAsync(request.TicketCommandToDomain()).ConfigureAwait(false);

            return Unit.Value;
        }
    }
}
