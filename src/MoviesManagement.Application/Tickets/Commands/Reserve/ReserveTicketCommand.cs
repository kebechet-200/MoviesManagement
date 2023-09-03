using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Tickets.Commands.Reserve
{
    public class ReserveTicketCommand : BaseTicketCommand, IRequest<Unit> { }
}
