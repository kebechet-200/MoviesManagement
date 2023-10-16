using MediatR;
using MoviesManagement.Application.Common.Extensions;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Application.Tickets.Common.Exceptions;
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
                TicketExceptions.Throw.NotFound(nameof(request.UserId));

            if (request.MovieId == Guid.Empty)
                TicketExceptions.Throw.NotFound(nameof(request.MovieId));

            if (request.State is not TicketEnum.Cancel)
                TicketExceptions.Throw.InvalidState(request.State);

            var user = await _userRepository.GetAsync(request.UserId, cancellationToken).ConfigureAwait(false);
            var movie = await _movieRepository.GetAsync(request.MovieId, cancellationToken).ConfigureAwait(false);

            if (user is null)
                throw new UserDoesNotExistException($"User with an id {request.UserId} does not exist in the database");

            if (movie is null)
                throw new MoviesNotFoundException($"Movie with an id {request.MovieId} does not exist in the database");

            if (movie.IsActive is false)
                TicketExceptions.Throw.MovieInactive();

            bool isLessThanHourFromStart = DateTime.UtcNow > movie.StartDate.AddHours(-1);

            if (isLessThanHourFromStart)
                TicketExceptions.Throw.MovieStartsSoon();

            var movieTickets = user.Tickets
                .Where(x => x.UserId == user.Id)
                .Where(x => x.MovieId == movie.Id);

            if (movieTickets.Any(x => x.State is not TicketEnum.Cancel))
                TicketExceptions.Throw.NoActiveTicket();

            var result = await _ticketRepository.CancelTicketAsync(request.CommandToDomain(), cancellationToken).ConfigureAwait(false);

            if (result == Guid.Empty)
                TicketExceptions.Throw.CannotCancel();

            return Unit.Value;
        }
    }
}
