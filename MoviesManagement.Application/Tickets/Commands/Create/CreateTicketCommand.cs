using MediatR;
using MoviesManagement.Application.Common.Models;
using MoviesManagement.Domain.Common.Enum;

namespace MoviesManagement.Application.Tickets.Commands.Create
{
    public class CreateTicketCommand : BaseTicketModel, IRequest<Unit> { }
}
