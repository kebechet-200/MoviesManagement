using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tickets.Queries.Get
{
    public record GetTicketQuery(Ticket ticket) : IRequest<Ticket>;
}
