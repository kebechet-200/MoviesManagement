using MediatR;
using MoviesManagement.Application.Interfaces;
using MoviesManagement.Domain.Common.Enum;
using MoviesManagement.Domain.Common.Exceptions;

namespace MoviesManagement.Application.Tickets.Commands.Create
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Unit>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IUserRepository _userRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository,
                                          IMovieRepository movieRepository,
                                          IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _movieRepository = movieRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
                throw new TicketNotFoundException("Userid is empty");

            if (request.MovieId == Guid.Empty)
                throw new TicketNotFoundException("MovieId is empty");

            var user = await _userRepository.GetAsync(request.UserId);
            var movie = await _movieRepository.GetAsync(request.MovieId);

            if (user is null)
                throw new UserDoesNotExistException($"User with an id {request.UserId} does not exist in the database");

            if (movie is null)
                throw new MoviesNotFoundException($"Movie with an id {request.MovieId} does not exist in the database");

            _ = request.State switch
            {
                TicketEnum.Bought => BuyTicket(user.Id, movie.Id),
                TicketEnum.Reserved => ReserverTicketTicket(user.Id, movie.Id),
                TicketEnum.Cancelled => CancelTicket(user.Id, movie.Id),
                _ => throw new InvalidStateException("Ticket State is not valid"),
            };

            return Unit.Value;
        }

        // TODO : Implement that
        private object BuyTicket(Guid userId, Guid movieId)
        {
            throw new NotImplementedException();
        }

        private object ReserverTicketTicket(Guid userId, Guid movieId)
        {
            throw new NotImplementedException();
        }

        private object CancelTicket(Guid userId, Guid movieId)
        {
            throw new NotImplementedException();
        }
    }
}
