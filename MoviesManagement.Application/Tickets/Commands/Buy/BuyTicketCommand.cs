using MediatR;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Domain.Common.Enum;

namespace MoviesManagement.Application.Tickets.Commands.Buy
{
    public class BuyTicketCommand : BaseTicketModel, IRequest<Unit> { }
}
