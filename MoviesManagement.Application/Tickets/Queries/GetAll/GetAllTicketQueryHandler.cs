using MediatR;
using MoviesManagement.Application.Contracts;
using MoviesManagement.Domain.Common.Exceptions;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tickets.Queries.GetAll
{
    public class GetAllTicketQueryHandler : IRequestHandler<GetAllTicketQuery, IQueryable<Ticket>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IQueryable<Ticket>> Handle(GetAllTicketQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAllTicketAsync().ConfigureAwait(false);

            if (tickets is null || !tickets.Any())
                throw new TicketNotFoundException("Tickets not found in the database");

            return tickets;
        }
    }
}
