using MediatR;
using MoviesManagement.Application.Common.Models;

namespace MoviesManagement.Application.Tickets.Commands.Buy
{
    public class BuyTicketCommand : BaseTicketCommand, IRequest<Unit> { }
}
