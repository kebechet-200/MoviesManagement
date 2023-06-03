using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tickets.Commands.Buy
{
    public class BuyTicketCommandHandler : IRequestHandler<BuyTicketCommand, Unit>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public BuyTicketCommandHandler(ITicketRepository ticketRepository,
                                          IMovieRepository movieRepository,
                                          IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(BuyTicketCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                throw new TicketNotFoundException("Userid is empty");

            if (request.MovieId == Guid.Empty)
                throw new TicketNotFoundException("MovieId is empty");

            if (request.State is not TicketEnum.Bought)
                throw new InvalidStateException("Ticket status is not Buy");

            var user = await _userRepository.GetAsync(request.UserId);
            var movie = await _movieRepository.GetAsync(request.MovieId);

            if (user is null)
                throw new UserDoesNotExistException($"User with an id {request.UserId} does not exist in the database");

            if (movie is null)
                throw new MoviesNotFoundException($"Movie with an id {request.MovieId} does not exist in the database");

            return await BuyTicket(user, movie, request);
        }

        private async Task<Unit> BuyTicket(User user, Movie movie, BuyTicketCommand request)
        {
            if (movie.StartDate < DateTime.UtcNow)
                throw new MovieAlreadyStartedException("The movie has already started.");

            var movieTickets = user.Tickets
                .Where(x => x.UserId == user.Id)
                .Where(x => x.MovieId == movie.Id);

            if (movieTickets.Any(x => x.State == TicketEnum.Bought))
                throw new YouAlreadyBoughtTicketException("You already bought the ticket");

            await _ticketRepository.BuyTicketAsync(request.TicketCommandToDomain());

            return Unit.Value;
        }
    }
}
