using MediatR;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tickets.Queries.Get
{
    public class GetTicketQueryHandler : IRequestHandler<GetTicketQuery, Ticket>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            if (request.MovieId == Guid.Empty)
                throw new MovieIdIsEmptyException($"Movie id is empty");

            if (request.UserId == Guid.Empty)
                throw new UserIdIsEmptyException($"User id is empty");

            var ticket = await _ticketRepository.GetTicketAsync(request.MovieId, request.UserId);

            if (ticket is null)
                throw new TicketNotFoundException($"Ticket not found on user id {request.UserId} and movie {request.MovieId}");

            return ticket;
        }
    }
}
