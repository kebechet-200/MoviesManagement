using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Tickets.Commands.Cancel
{
    public class CancelTicketCommand : BaseTicketModel, IRequest<Unit> { }
}
