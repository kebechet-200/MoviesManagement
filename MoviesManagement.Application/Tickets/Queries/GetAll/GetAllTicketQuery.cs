using MediatR;
using MoviesManagement.Domain.POCO;

namespace MoviesManagement.Application.Tickets.Queries.GetAll
{
    public record GetAllTicketQuery : IRequest<IQueryable<Ticket>>;
}
